
using UnityEngine;

//------------------------------------------------------------
// TIPS:
// 描画する画面を指定した比率に変更する
// メインカメラに必ず MainCamera タグを設定すること
//
//------------------------------------------------------------

class ViewAspectUpdate : MonoBehaviour {

  [SerializeField]
  Vector2 _aspect = new Vector2(16f, 9f);

  [SerializeField]
  Color _backGroundColor = Color.black;

  float _aspectRate = 0.0f;
  Camera _camera = null;
  static Camera _backgroundCamera = null;

  void Start() {
    _aspectRate = _aspect.x / _aspect.y;
    _camera = Camera.main;

    CreateBackgroundCamera();
    UpdateScreenRate();
  }

  void CreateBackgroundCamera() {
#if UNITY_EDITOR
    if (!UnityEditor.EditorApplication.isPlaying) { return; }
#endif

    if (_backgroundCamera != null) { return; }

    var backGroundCameraObject = new GameObject("BackGroundCamera");
    _backgroundCamera = backGroundCameraObject.AddComponent<Camera>();
    _backgroundCamera.depth = -99;
    _backgroundCamera.fieldOfView = 1;
    _backgroundCamera.farClipPlane = 1.1f;
    _backgroundCamera.nearClipPlane = 1;
    _backgroundCamera.cullingMask = 0;
    _backgroundCamera.depthTextureMode = DepthTextureMode.None;
    _backgroundCamera.backgroundColor = _backGroundColor;
    _backgroundCamera.renderingPath = RenderingPath.VertexLit;
    _backgroundCamera.clearFlags = CameraClearFlags.SolidColor;
    _backgroundCamera.useOcclusionCulling = false;
    backGroundCameraObject.hideFlags = HideFlags.NotEditable;
  }

  void UpdateScreenRate() {
    float baseAspect = _aspect.y / _aspect.x;
    float nowAspect = (float)Screen.height / Screen.width;

    if (baseAspect > nowAspect) {
      var changeAspect = nowAspect / baseAspect;
      _camera.rect = new Rect((1 - changeAspect) * 0.5f, 0, changeAspect, 1);
    }
    else {
      var changeAspect = baseAspect / nowAspect;
      _camera.rect = new Rect(0, (1 - changeAspect) * 0.5f, 1, changeAspect);
    }
  }

  bool IsChangeAspect() {
    return _camera.aspect != _aspectRate;
  }

  void Update() {
    if (!IsChangeAspect()) { return; }

    UpdateScreenRate();
    _camera.ResetAspect();
  }
}
