
using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour {

  [SerializeField]
  AudioPlayer _audio = null;

  [SerializeField]
  ARDeviceManager _device = null;

  [SerializeField]
  GameController _controller = null;

  [SerializeField]
  GameMenu _menu = null;

  [SerializeField]
  CanvasGroup _gameUI = null;

  [SerializeField]
  TimeCount _counter = null;

  [SerializeField]
  GameCounter _startCount = null;

  [SerializeField]
  GameCounter _finishCount = null;

  [SerializeField]
  GameSuddenDeath _suddenDeath = null;

  [SerializeField]
  GameFinish _finish = null;

  [SerializeField]
  ARModelMaterial _materials = null;

  [SerializeField]
  RefereeFloat _referee = null;

  [SerializeField]
  GameShot _shot = null;

  [SerializeField]
  GameObject _ruleCanvas = null;

  [SerializeField]
  GameEffectManager _effect = null;

  [SerializeField]
  LayerMask _refereeMask;

  [SerializeField]
  Vector3 _refereePosition = Vector3.zero;

  [SerializeField, Range(1f, 5f)]
  float _refereeVelocity = 1f;

  bool _isStart = false;

  public enum State { Detect, Standby, MainGame, Result, }
  State _state = State.Detect;
  public State state { get { return _state; } }

  /// <summary> プレイボタンが押せるようになったら true を返す </summary>
  public bool isStart { get { return _isStart; } }

  Coroutine _playThread = null;

  void Start() {
    _playThread = StartCoroutine(GameLoop());
    _gameUI.alpha = 0f;
    _referee.enabled = false;
    _audio.Play(3, true);
  }

  void OnDestroy() { _audio.Stop(); }

  /// <summary> プレイボタンが押された </summary>
  public void OnPlay() {
    _isStart = true;
    _menu.start.interactable = false;
    _menu.back.interactable = false;
    _menu.hint.interactable = false;
  }

  /// <summary> 戻るボタンが押された </summary>
  public void OnBackToMenu() {
    StopCoroutine(_playThread);
    System.Action Change = () => { GameScene.Menu.ChangeScene(); };
    ScreenSequencer.instance.SequenceStart(Change, new Fade(1f));
  }

  IEnumerator GameLoop() {
    yield return StartCoroutine(DetectMarker());
    yield return StartCoroutine(Standby());
    yield return StartCoroutine(MainGame());
    yield return StartCoroutine(Result());
  }

  // TIPS: マーカー検出
  IEnumerator DetectMarker() {
    _state = State.Detect;
    _isStart = false;

    while (!_isStart) {
      _menu.start.interactable = _device.DetectMarker();
      yield return null;
    }

    var models = _device.GetModels().Except(_device.models);
    foreach (var model in models) { Destroy(model.gameObject); }

    _device.player1.inputKey = _controller.IsPlayer1KeyDown;
    _device.player2.inputKey = _controller.IsPlayer2KeyDown;
    _device.player1.body.material = _materials.p1body;
    _device.player1.clip.material = _materials.p1clip;
    _device.player2.body.material = _materials.p2body;
    _device.player2.clip.material = _materials.p2clip;
    _device.player1.scoreBoard = _menu.player1;
    _device.player2.scoreBoard = _menu.player2;
    _device.player1.effect = _effect.p1effect;
    _device.player2.effect = _effect.p2effect;
  }

  // TIPS: ゲームルール説明（キー入力でゲーム開始）
  IEnumerator Standby() {
    _state = State.Standby;

    var canvas = Instantiate(_ruleCanvas);

    while (true) {
      _device.ModelUpdate();
      var p1 = _device.player1.inputKey();
      var p2 = _device.player2.inputKey();
      if (p1 && p2) { break; }
      yield return null;
    }

    Destroy(canvas);

    while (_menu.group.alpha > 0f) {
      var time = Time.deltaTime;
      _menu.group.alpha -= time;
      _gameUI.alpha += time;
      _device.ModelUpdate();
      yield return null;
    }

    while (true) {
      var distance = _refereePosition - _referee.transform.position;
      if (distance.magnitude < 10f) { break; }
      _referee.transform.position += distance * Time.deltaTime * _refereeVelocity;
      _device.ModelUpdate();
      yield return null;
    }

    _referee.enabled = true;
  }

  // TIPS: ゲームループ
  IEnumerator MainGame() {
    _state = State.MainGame;

    _counter.TimeReset();
    _startCount.Visible();
    _audio.Play(14);

    var countDown = 3.5f;
    while (countDown > 0f) {
      countDown -= Time.deltaTime;
      _startCount.UpdateCount(Mathf.RoundToInt(countDown));
      _device.ModelUpdate();
      yield return null;
    }

    _startCount.Visible();
    if (_suddenDeath.isVisible) {
      _suddenDeath.Visible();
      _counter.time = _counter.timeCount * 0.5f;
    }

    var finishCountStart = false;
    while (_counter.time > 0f) {
      _device.ModelUpdate();
      _counter.board.text = "Time: " + _counter.timeToInt;

      // TIPS: モデルが表示されている間だけ実行
      if (_device.models.All(model => model.isVisible)) {
        ActiveGameMode();
        if (_counter.time < 3.5f && !finishCountStart) {
          finishCountStart = true;
          _audio.Play(15);
          _finishCount.Visible();
        }
      }

      yield return null;
    }

    _finishCount.Visible();
    var p1score = _device.player1.scoreBoard.count;
    var p2score = _device.player2.scoreBoard.count;
    var draw = (p1score == p2score);

    if (draw) { _suddenDeath.Visible(); yield return StartCoroutine(MainGame()); }
  }

  // TIPS: リザルト表示
  IEnumerator Result() {
    _state = State.Result;

    var p1score = _device.player1.scoreBoard.count;
    var p2score = _device.player2.scoreBoard.count;
    _finish.ActivateImage(p1score, p2score);
    var winner = (p1score > p2score ? _device.player1 : _device.player2);

    _menu.group.alpha = 1f;
    _menu.start.image.color = Color.white * 0f;
    _menu.hint.image.color = Color.white * 0f;
    _menu.back.interactable = true;

    _effect.CreateFireWorks(winner.transform);
    _effect.CreateFireWorks(winner.transform);
    _effect.CreateFireWorks(winner.transform);
    _effect.ActivatePaper(winner.transform);

    RaycastHit hit;
    while (true) {
      if (TouchController.IsTouchBegan()) {
        var isHit = TouchController.IsRaycastHitWithLayer(out hit, _refereeMask);
        if (isHit) { break; }
      }
      _device.ModelUpdate();
      yield return null;
    }

    OnBackToMenu();
  }

  void ActiveGameMode() {
    _counter.UpdateTimeCount();

    if (_device.player1.inputKey()) { Shot(_device.player1, _device.player2); }
    if (_device.player2.inputKey()) { Shot(_device.player2, _device.player1); }

    if (_counter.time > 3.5f) { return; }
    _finishCount.UpdateCount(_counter.timeToInt);
  }

  void Shot(ARModel player, ARModel target) {
    var shot = Instantiate(_shot);
    shot.transform.position = player.transform.position + shot.offset;
    shot.transform.Translate(shot.transform.forward * 50f);
    shot.target = target.transform;
    shot.listener = (player == _device.player1 ? (System.Action)ShotHitP1 : ShotHitP2);
    shot.effect = player.effect;
    _audio.Play(20);
  }

  void HitPlay() { _audio.Play(21); }
  void ShotHitP1() { _device.player1.scoreBoard.CountUp(); HitPlay(); }
  void ShotHitP2() { _device.player2.scoreBoard.CountUp(); HitPlay(); }
}
