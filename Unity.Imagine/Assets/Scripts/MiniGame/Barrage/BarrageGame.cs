
using UnityEngine;

public class BarrageGame : AbstractGame
{
  public override void Action()
  {
    if (inputP1.IsPush()) { Shot(gameManager.player1, gameManager.player2); }
    if (inputP2.IsPush()) { Shot(gameManager.player2, gameManager.player1); }
  }

  void Shot(ARModel player, ARModel target)
  {
    /*
    var shot = Instantiate(_shot);
    shot.transform.position = player.transform.position + shot.offset;
    shot.transform.Translate(shot.transform.forward * 50f);
    shot.target = target.transform;
    shot.listener = (player == _device.player1 ? (System.Action)ShotHitP1 : ShotHitP2);
    shot.effect = player.effect;
    _audio.Play(20);
    */
  }


  public override bool IsFinish()
  {
    return false;
  }

  public override bool IsDraw()
  {
    return false;
  }

  public override Transform GetWinner()
  {
    return null;
  }


  TimeCount _timeCount = null;

  void Start()
  {
    // ゲームルールのテキスト初期化
    string text = ("10").ToColor(RichText.ColorType.red).ToSize(100);
    text += (" びょうかん に\nたま を いっぱいあてよう！").ToSize(60);
    gameRule = text;

    // ゲームで使用するリソースの生成
    var resources = GameResources.instance.barrage.CreateResource();
    foreach (var res in resources)
    {
      res.transform.SetParent(transform);
    }

    _timeCount = gameObject.AddComponent<TimeCount>();
  }
}
