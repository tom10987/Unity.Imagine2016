
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// スピードのゲームのメイン処理
//
//------------------------------------------------------------

public class BarrageGame : AbstractGame
{
  public enum Player { _1, _2, }

  public override void Action()
  {
    _speedGame.timeManager.UpdateTime();
    gameManager.referee.textBox.text = _speedGame.timeManager.currentTimeString;
    if (inputP1.IsPush()) { Shot(Player._1); }
    if (inputP2.IsPush()) { Shot(Player._2); }

    // TIPS: 残り時間が少なくなったらカウントダウン開始
    if (_isPlaying) { return; }
    if (_speedGame.timeManager.currentTimeToInt > 3) { return; }
    _isPlaying = true;
    gameManager.audio.Play(ClipIndex.se_No16_FinishCountDown);
    StartCoroutine(gameManager.announce.finishCount.PlayCountDown());
  }

  // ショット発射
  void Shot(Player sign)
  {
    // 各プレイヤーに対応した要素を取得
    var player = sign.IsPlayer1() ? gameManager.player1 : gameManager.player2;
    var target = sign.IsPlayer1() ? gameManager.player2 : gameManager.player1;
    var effect = sign.IsPlayer1() ? _speedGame.p1ShotEffect : _speedGame.p2ShotEffect;
    var action = sign.IsPlayer1() ? (System.Action)ShotP1 : ShotP2;

    // ショットのインスタンス生成
    var shot = _speedGame.shotObject;
    shot.effect = effect;
    shot.target = target.transform;
    shot.transform.position = player.transform.position + shot.offsetY;
    shot.transform.Translate(player.forward * shot.offsetForward);
    shot.listener = action;

    // SE 再生
    gameManager.audio.Play(ClipIndex.se_No21_Shot);
  }
  // プレイヤー１のショットが命中したとき
  void ShotP1()
  {
    ++_p1score;
    _speedGame.gameUI.p1Score.text = _p1score.ToString();
    gameManager.audio.Play(ClipIndex.se_No22_Hit);
  }
  // プレイヤー２のショットが命中したとき
  void ShotP2()
  {
    ++_p2score;
    _speedGame.gameUI.p2Score.text = _p2score.ToString();
    gameManager.audio.Play(ClipIndex.se_No22_Hit);
  }


  public override void GameStart()
  {
    _speedGame.gameObject.SetActive(true);
    _speedGame.timeManager.TimeReset();
    _isPlaying = false;
  }
  public override void SuddenDeathAction()
  {
    _speedGame.timeManager.SuddenDeathMode();
  }


  public override bool IsFinish()
  {
    return _speedGame.timeManager.isFinish;
  }
  public override bool IsDraw()
  {
    return _p1score == _p2score;
  }

  public override Transform GetWinner()
  {
    return _p1score > _p2score ?
      gameManager.player1.transform : gameManager.player2.transform;
  }


  SpeedGameManager _speedGame = null;
  int _p1score = 0;
  int _p2score = 0;

  // TIPS: 終了前のカウントダウン開始したか
  bool _isPlaying = false;


  void Start()
  {
    // ゲームルールのテキスト初期化
    string text = ("10").ToColor(RichText.ColorType.red).ToSize(100);
    text += (" びょうかん に\nたま を いっぱいあてよう！").ToSize(60);
    gameRule = text;

    // ゲームで使用するリソースの生成
    var resources = GameResources.instance.barrage.CreateResourceArray();
    _speedGame = resources[0].GetComponent<SpeedGameManager>();

    // モデル初期化
    _speedGame.ModelSetup(gameManager.arManager);
    _speedGame.gameUI.p1Score.text = _p1score.ToString();
    _speedGame.gameUI.p2Score.text = _p2score.ToString();
    _speedGame.gameObject.SetActive(false);
  }
}

public static class GameUtility
{
  public static bool IsPlayer1(this BarrageGame.Player p)
  {
    return p == BarrageGame.Player._1;
  }
}
