
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// スピードのゲームのリソース一覧
//
//------------------------------------------------------------

[System.Serializable]
public class SpeedGameModelResource
{
  [SerializeField]
  Material _body = null;
  public Material body { get { return _body; } }

  [SerializeField]
  Material _clip = null;
  public Material clip { get { return _clip; } }


  [SerializeField]
  OneTimeEffect _effect = null;
  public OneTimeEffect shotEffect { get { return _effect; } }


  /// <summary> AR モデルのマテリアルを設定する </summary>
  public void MaterialSetup(ARModel model)
  {
    model.bodyRenderer.material = _body;
    model.clipRenderer.material = _clip;
  }
}
