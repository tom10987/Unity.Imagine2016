
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// GameResource プレハブの生成、管理を行う
//
//------------------------------------------------------------
// TIPS:
// GameResources.instance プロパティから、各要素を取り出せます
//
// GameResources.instance.barrage.CreateResourece() で、
// 登録したプレハブを全て取り出すことができます
//
// 下記の方法で、各プレハブを生成、操作ができます
//
// var barrage = GameResources.instance.barrage;
// foreach (var res in barrage.CreateResouce())
// {
//   /* 各プレハブに対する処理 */
// }
//
//------------------------------------------------------------

public class GameResources : SingletonBehaviour<GameResources>
{
  [SerializeField]
  SpeedGameManager _barrage = null;
  public SpeedGameManager barrage { get { return _barrage; } }

  [SerializeField]
  GameResource _charge = null;
  public GameResource charge { get { return _charge; } }

  [SerializeField]
  GameResource _pendulum = null;
  public GameResource pendulum { get { return _pendulum; } }
}
