
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// スピードのゲームで使用するリソースの管理を行う
//
//------------------------------------------------------------

public class SpeedGameManager : MonoBehaviour
{
  [SerializeField]
  SpeedGameModelResource _player1 = null;
  /// <summary> プレイヤー１の弾エフェクトを生成 </summary>
  public ShotEffect p1ShotEffect { get { return Instantiate(_player1.shotEffect); } }

  [SerializeField]
  SpeedGameModelResource _player2 = null;
  /// <summary> プレイヤー２の弾エフェクトを生成 </summary>
  public ShotEffect p2ShotEffect { get { return Instantiate(_player2.shotEffect); } }


  [SerializeField]
  SpeedGameShot _shotObject = null;
  /// <summary> ショット生成 </summary>
  public SpeedGameShot shotObject { get { return Instantiate(_shotObject); } }


  /// <summary> ゲーム開始前のモデル初期化 </summary>
  public void ModelSetup(ARDeviceManager arManager)
  {
    _player1.MaterialSetup(arManager.player1);
    _player2.MaterialSetup(arManager.player2);
  }
}
