
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// Speed のゲームで使用するリソースの管理を行う
//
//------------------------------------------------------------

[System.Serializable]
public class SpeedGameMaterial
{
  [SerializeField]
  Material _body = null;
  public Material body { get { return _body; } }

  [SerializeField]
  Material _clip = null;
  public Material clip { get { return _clip; } }
}

public class SpeedGameManager : MonoBehaviour
{
  [SerializeField]
  SpeedGameMaterial _player1 = null;

  [SerializeField]
  SpeedGameMaterial _player2 = null;


  /// <summary> ゲーム開始前のモデル初期化 </summary>
  public void ModelSetup(ARDeviceManager arManager)
  {
    MaterialSetup(arManager.player1, _player1);
    MaterialSetup(arManager.player2, _player2);
  }


  // TIPS: モデルのマテリアルを設定
  void MaterialSetup(ARModel model, SpeedGameMaterial materials)
  {
    model.bodyRenderer.material = materials.body;
    model.clipRenderer.material = materials.clip;
  }
}
