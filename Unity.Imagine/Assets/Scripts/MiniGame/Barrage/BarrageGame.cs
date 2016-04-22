
using UnityEngine;

public class BarrageGame : AbstractGame
{
  public enum Player { _1, _2, }

  public override void Action()
  {
    if (inputP1.IsPush()) { Shot(Player._1); }
    if (inputP2.IsPush()) { Shot(Player._2); }
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
    shot.transform.position = player.transform.position + shot.offset;
    shot.transform.Translate(shot.transform.forward * 50f);
    shot.listener = action;
    gameManager.audio.Play(ClipIndex.se_No21_Shot);
  }

  // プレイヤー１のショットが命中したとき
  void ShotP1()
  {
    Debug.Log("Player 1");
  }

  // プレイヤー２のショットが命中したとき
  void ShotP2()
  {
    Debug.Log("Player 2");
  }


  public override bool IsFinish()
  {
    return Input.GetKeyDown(KeyCode.G);
  }

  public override bool IsDraw()
  {
    return false;
  }

  public override Transform GetWinner()
  {
    return null;
  }


  // TIPS: スピードのゲームのリソース管理クラス
  SpeedGameManager _speedGame = null;


  TimeCount _timeCount = null;

  void Start()
  {
    // ゲームルールのテキスト初期化
    string text = ("10").ToColor(RichText.ColorType.red).ToSize(100);
    text += (" びょうかん に\nたま を いっぱいあてよう！").ToSize(60);
    gameRule = text;

    // ゲームで使用するリソースの生成
    _speedGame = Instantiate(GameResources.instance.barrage);
    GameResources.instance.Release();
  }
}

static class Extension
{
  public static bool IsPlayer1(this BarrageGame.Player p)
  {
    return p == BarrageGame.Player._1;
  }
}
