
using UnityEngine;

//------------------------------------------------------------
// TIPS:
// 描画する画面を指定した比率に変更する
// メインカメラをインスペクターから割り当てる
//
//------------------------------------------------------------

class ViewAspectUpdate : MonoBehaviour {

  [SerializeField]
  Vector2 _aspect = new Vector2(16f, 9f);
  float aspectRate { get { return _aspect.x / _aspect.y; } }

  [SerializeField, Range(0f, 1f)]
  float _xOffset = 0.5f;

  [SerializeField, Range(0f, 1f)]
  float _yOffset = 0.5f;

  [SerializeField]
  Color _backGroundColor = Color.black;

  [SerializeField]
  Camera _camera = null;
  static Camera _backgroundCamera = null;

  void Awake() {
    CreateBackgroundCamera();
    UpdateAspectRate();
  }

  void CreateBackgroundCamera() {
#if UNITY_EDITOR
    if (!UnityEditor.EditorApplication.isPlaying) { return; }
#endif

    if (_backgroundCamera != null) { return; }

    var backGroundCameraObject = new GameObject("BackGroundCamera");
    _backgroundCamera = backGroundCameraObject.AddComponent<Camera>();
    _backgroundCamera.depth = -99f;
    _backgroundCamera.fieldOfView = 1f;
    _backgroundCamera.farClipPlane = 1.1f;
    _backgroundCamera.nearClipPlane = 1f;
    _backgroundCamera.cullingMask = 0;
    _backgroundCamera.depthTextureMode = DepthTextureMode.None;
    _backgroundCamera.backgroundColor = _backGroundColor;
    _backgroundCamera.renderingPath = RenderingPath.VertexLit;
    _backgroundCamera.clearFlags = CameraClearFlags.SolidColor;
    _backgroundCamera.useOcclusionCulling = false;
    backGroundCameraObject.hideFlags = HideFlags.NotEditable;
  }

  /// <summary> カメラのアスペクト比が指定した値と異なるとき true を返す </summary>
  public bool IsChangeAspect() { return _camera.aspect != aspectRate; }

  /// <summary> カメラのアスペクト比を補正 </summary>
  public void UpdateAspectRate() {
    var nowAspect = (float)Screen.width / Screen.height;
    var isGreater = aspectRate < nowAspect;

    var currentAspect = isGreater ?
      aspectRate / nowAspect : nowAspect / aspectRate;

    _camera.rect = isGreater ?
      HeightBaseRect(currentAspect) : WidthBaseRect(currentAspect);

    _camera.ResetAspect();
  }

  Rect WidthBaseRect(float aspect) {
    return new Rect(0f, (1f - aspect) * _yOffset, 1f, aspect);
  }

  Rect HeightBaseRect(float aspect) {
    return new Rect((1f - aspect) * _xOffset, 0f, aspect, 1f);
  }
}
