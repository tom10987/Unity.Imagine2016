
using UnityEngine;
using System.Collections;

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
  public OneTimeEffect p1ShotEffect { get { return _player1.shotEffect; } }

  [SerializeField]
  SpeedGameModelResource _player2 = null;
  /// <summary> プレイヤー２の弾エフェクトを生成 </summary>
  public OneTimeEffect p2ShotEffect { get { return _player2.shotEffect; } }


  [SerializeField]
  SpeedGameShot _shotObject = null;
  /// <summary> ショット生成 </summary>
  public SpeedGameShot shotObject { get { return Instantiate(_shotObject); } }


  [SerializeField]
  SpeedGameUI _gameUI = null;
  /// <summary> <see cref="Canvas"/> の情報を取得 </summary>
  public SpeedGameUI gameUI { get { return _gameUI; } }


  [SerializeField]
  SpeedGameTime _timeManager = null;
  /// <summary> ゲーム時間の管理機能 </summary>
  public SpeedGameTime timeManager { get { return _timeManager; } }

  
  void Start() { StartCoroutine(Setup()); }

  // TIPS: UI 周りの初期化
  IEnumerator Setup()
  {
    _gameUI.group.alpha = 0f;
    while (_gameUI.group.alpha < 1f)
    {
      _gameUI.group.alpha += Time.deltaTime;
      yield return null;
    }
  }


  /// <summary> ゲーム開始前のモデル初期化 </summary>
  public void ModelSetup(ARDeviceManager arManager)
  {
    _player1.MaterialSetup(arManager.player1);
    _player2.MaterialSetup(arManager.player2);
  }
}
