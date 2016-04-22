
using UnityEngine;
using System.Collections;

//------------------------------------------------------------
// NOTICE:
// ゲーム全体の管理を行う
//
//------------------------------------------------------------
// TIPS:
// 下記オブジェクトを取得できます
//
// * 各プレイヤーの情報
// * 音源（AudioPlayer クラス）
// * レフェリー
//
//------------------------------------------------------------

public class GameManager : MonoBehaviour
{
  [SerializeField]
  [Tooltip("AR カメラ")]
  ARDeviceManager _arManager = null;
  public ARDeviceManager arManager { get { return _arManager; } }
  public ARModel player1 { get { return _arManager.player1; } }
  public ARModel player2 { get { return _arManager.player2; } }


  [SerializeField]
  AudioPlayer _audioPlayer = null;
  /// <summary> <see cref="AudioPlayer"/> を取得 </summary>
  public new AudioPlayer audio { get { return _audioPlayer; } }


  [SerializeField]
  [Tooltip("ゲーム画面上部の各種ボタン")]
  GameMenu _menu = null;

  [SerializeField]
  [Tooltip("ゲームルールを表示するボードのプレハブを指定")]
  RuleBoard _ruleBoard = null;

  [SerializeField]
  Referee _referee = null;
  /// <summary> レフェリーのオブジェクトを取得 </summary>
  public Referee referee { get { return _referee; } }


  [SerializeField]
  [Tooltip("ゲーム開始時のカウントダウンを表示するキャンバス")]
  GameAnnounce _announce = null;
  /// <summary> カウントダウン表示など、通知の呼び出し </summary>
  public GameAnnounce announce { get { return _announce; } }

  [SerializeField]
  [Tooltip("ゲーム終了時のエフェクト")]
  GameFinish _finishEffect = null;


  // TIPS: 動作中のコルーチンを保持
  Coroutine _playThread = null;

  // TIPS: プレイボタンが押されたかどうか
  bool _isStart = false;

  // TIPS: ミニゲームの管理クラス
  AbstractGame _game = null;


  void Start()
  {
    _audioPlayer.Play(ClipIndex.bgm_No04_MiniGame, true);
    _playThread = StartCoroutine(GameLoop());

    _menu.start.onClick.AddListener(OnPlay);
    _menu.back.onClick.AddListener(OnBackToMenu);

    _referee.gameObject.SetActive(false);
  }


  // プレイボタンの処理
  void OnPlay()
  {
    _isStart = true;
    _menu.ButtonSetActive(false);
  }

  // 戻るボタンの処理
  void OnBackToMenu()
  {
    StopCoroutine(_playThread);

    // TIPS: 画面遷移の演出が終了したときの処理
    System.Action change = () =>
    {
      GameScene.Menu.ChangeScene();
      GameController.instance.Release();
      _audioPlayer.Stop();
    };
    ScreenSequencer.instance.SequenceStart(change, new Fade(1f));
  }


  // メインループ
  IEnumerator GameLoop()
  {
    // TIPS: デバイスの初期化待ちで１フレームスキップする
    yield return null;

    yield return StartCoroutine(DetectMarker());
    yield return StartCoroutine(Standby());
    yield return StartCoroutine(MainGame());
    yield return StartCoroutine(Result());
  }


  // AR マーカー認識フェイズ
  IEnumerator DetectMarker()
  {
    _isStart = false;

    while (!_isStart)
    {
      // TIPS: マーカーを２つ認識できたらプレイボタンを押せるようにする
      _menu.start.interactable = _arManager.DetectMarker();

      // TIPS: 処理が重いので、わざとフレームをスキップする
      yield return null;
      yield return null;
    }

    // TIPS: ゲームに使用されないモデルのインスタンスを解放
    _arManager.RemoveModel();
  }


  // プレイボタン押された -> ゲームルール解説
  IEnumerator Standby()
  {
    yield return StartCoroutine(Initialize());

    // TIPS: ゲームルールを表示
    var ruleBoardInstance = Instantiate(_ruleBoard);
    ruleBoardInstance.SetRuleText(_game.gameRule);

    // TIPS: 両方のプレイヤーが同時に操作キーを入力したら次のステップに進む
    while (!GameController.instance.IsGameStart())
    {
      _arManager.ModelUpdate();
      yield return null;
    }

    // TIPS: ルール説明のキャンバスを削除
    ruleBoardInstance.DeleteObject();
  }

  // ゲームの初期化
  IEnumerator Initialize()
  {
    // TIPS: 選択されたゲームモードに対応した初期化を行う
    switch (GameMode.type)
    {
      // 連射
      case GameType.Speed:
        _game = GameMode.Create<BarrageGame>(this);
        break;

      // チャージ
      case GameType.Power:
        _game = GameMode.Create<ChargeGameController>(this);
        break;

      // 反射（振り子）
      case GameType.Defense:
        _game = GameMode.Create<Pendulum>(this);
        break;

      // TIPS: 不正な値が入っていたらメニュー画面に戻す
      default:
        Debug.Assert(false, "ゲームモードが正しく設定されていません");
        OnBackToMenu();
        yield break;
    }

    // TIPS: Awake(), Start() の実行待ちのため、一時停止
    yield return null;
  }


  bool _isFinish = false;       // 完全にゲームが終了したかどうか
  bool _isSuddenDeath = false;  // サドンデスかどうか

  // ゲームループ
  IEnumerator MainGame()
  {
    _referee.gameObject.SetActive(true);

    // TIPS: メニューボタンをゆっくりと消しながらレフェリーを動かす
    while (_menu.group.alpha > 0f)
    {
      _menu.group.alpha -= Time.deltaTime * 0.5f;
      _referee.MoveToGamePosition();
      yield return null;
    }

    // TIPS: ゲームが完全に決着するまでサドンデスを繰り返す
    _isFinish = false;
    _isSuddenDeath = false;
    while (!_isFinish) { yield return StartCoroutine(Game()); }
  }

  // ゲーム部分
  IEnumerator Game()
  {
    // TIPS: ゲームの初期化
    _game.GameStart();

    // TIPS: サドンデスだったときの処理
    if (_isSuddenDeath)
    {
      _announce.hideSuddenDeath = false;
      _game.SuddenDeathAction();
      yield return new WaitForSeconds(2f);
    }

    // TIPS: カウントダウン開始
    _audioPlayer.Play(ClipIndex.se_No15_StartCountDown);
    yield return StartCoroutine(_announce.startCount.PlayCountDown());

    _announce.hideSuddenDeath = true;

    while (!_game.IsFinish())
    {
      _arManager.ModelUpdate();

      // TIPS: 基本的に AR マーカーを認識できている時だけメインの処理を行う
      _game.EarlyUpdate();
      if (_arManager.existsModels) { _game.Action(); }
      _game.LastUpdate();

      yield return null;
    }

    _isFinish = !_game.IsDraw();
    _isSuddenDeath = _game.IsDraw();
  }


  // ゲーム結果
  IEnumerator Result()
  {
    // TIPS: 戻るボタン復旧、SE 再生、レフェリーのボード更新
    _menu.BackMenuActivate();
    _audioPlayer.Play(ClipIndex.se_No20_Result);
    _referee.textBox.text = "メニューに戻る";

    // TIPS: エフェクト実行
    var winner = _game.GetWinner();
    _finishEffect.PaperActivate(winner);
    _finishEffect.PlayFireworks(winner);

    // TIPS: 勝敗表示
    System.Action victory = (winner == player1.transform) ?
      (System.Action)_finishEffect.WinnerP1 : _finishEffect.WinnerP2;
    victory();

    // TIPS: レフェリーがクリックされた場合もゲームを終了する
    while (!_referee.IsRaycastHit())
    {
      // TIPS: メニューのボタンをゆっくり見えるようにする
      if (_menu.group.alpha < 1f) { _menu.group.alpha += Time.deltaTime; }
      yield return null;
    }

    OnBackToMenu();
  }
}
