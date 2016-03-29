
using UnityEngine;

//------------------------------------------------------------
// TIPS:
// transform.localRotation に対してカメラ角度を反映させた場合、
// スクリプトを設定したオブジェクトの角度に対してのみ角度が反映される
//
// スクリプトを設定したオブジェクトに親オブジェクトが存在する場合、
// 親オブジェクトの角度が変わった時に画像の角度も変わってしまう
//
// そのため、親オブジェクトの角度も反映される、
// transform.rotation に対してカメラ角度を反映させている
//
// XXX:
// 子オブジェクトの座標を動かしている場合、
// 親オブジェクトを回転させるとカメラに対しては正面を向くが、
// 親の座標を中心に回転するため、画像の位置がずれるので注意
//
//------------------------------------------------------------

class SpriteBillBoard : MonoBehaviour {

  Quaternion _rotate = Quaternion.identity;

  SpriteRenderer _renderer = null;
  public new SpriteRenderer renderer {
    get {
      if (_renderer == null) { _renderer = GetComponent<SpriteRenderer>(); }
      return _renderer;
    }
  }

  void Start() {
    _rotate = Camera.main.transform.rotation;
    _renderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (transform.rotation == _rotate) { return; }
    transform.rotation = _rotate;
  }

  void OnValidate() {
    Start();
    Update();
  }
}
