
using UnityEngine;
using System;

class TouchTest : MonoBehaviour {

  [SerializeField]
  Material _default = null;

  [SerializeField]
  Material _clicked = null;

  [SerializeField]
  Renderer _renderer = null;

  int _timeCount = 0;
  bool activeMaterial { get { return _timeCount > 0; } }

  void Update() {
    if (activeMaterial) { --_timeCount; }
    UpdateMaterial();
  }

  void UpdateMaterial() {
    var success = TouchController.IsDoubleTap();
    if (success) { _timeCount = 60; }
    var active = activeMaterial && success;
    _renderer.material = active ? _clicked : _default;
  }

  void TransformScreenPosition() {
    var touch = TouchController.GetScreenPosition();
    Debug.Log("touch = " + touch);

    Action<Vector3> PrintLog = position => {
      var _stov = Camera.main.ScreenToViewportPoint(position);
      var _vtos = Camera.main.ViewportToScreenPoint(position);
      Debug.Log("stov = " + _stov);
      Debug.Log("vtos = " + _vtos);
    };

    var stov = Camera.main.ScreenToViewportPoint(touch);
    var vtos = Camera.main.ViewportToScreenPoint(touch);
    Debug.Log("stov = " + stov);
    Debug.Log("vtos = " + vtos);

    Debug.LogWarning("----- stov -----");
    PrintLog(stov);
    Debug.LogWarning("----- vtos -----");
    PrintLog(vtos);
  }
}
