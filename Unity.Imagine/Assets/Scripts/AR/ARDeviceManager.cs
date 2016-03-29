
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NyAR.MarkerSystem;
using NyARUnityUtils;

//------------------------------------------------------------
// NOTICE:
// 接続されているデバイスとしてのカメラを管理する
// ARCamera を登録、まとめて更新する
//
//------------------------------------------------------------

public class ARDeviceManager : MonoBehaviour {

  NyARUnityWebCam _device = null;
  public NyARUnityWebCam device { get { return _device; } }

  NyARUnityMarkerSystem _arSystem = null;
  public NyARUnityMarkerSystem arSystem { get { return _arSystem; } }

  [SerializeField]
  Camera _camera = null;

  [SerializeField]
  [Tooltip("カメラ映像を投影するパネルの Renderer コンポーネント")]
  Renderer _panel = null;

  /// <summary> カメラ映像を投影しているパネルオブジェクト </summary>
  public GameObject cameraScreen { get { return _panel.gameObject; } }

  [SerializeField, Range(2, 64)]
  [Tooltip("マーカーの解像度")]
  int _resolution = 16;
  public int markerResolution { get { return _resolution; } }

  [SerializeField, Range(10, 50)]
  [Tooltip("マーカーのエッジ割合")]
  int _edge = 25;
  public int markerEdge { get { return _edge; } }

  [SerializeField, Range(10, 320)]
  [Tooltip("マーカーサイズ")]
  int _markerScale = 80;
  public int markerScale { get { return _markerScale; } }

  /// <summary> 管理下にある <see cref="ARModel"/> を全て取得 </summary>
  public IEnumerable<ARModel> GetModels() { return this.GetOnlyChildren<ARModel>(); }

  readonly int _existsCount = 2;
  List<ARModel> _models = new List<ARModel>();

  /// <summary> ゲーム開始後に使用するモデルの一覧 </summary>
  public IEnumerable<ARModel> models { get { return _models; } }
  public ARModel player1 { get { return _models[0]; } }
  public ARModel player2 { get { return _models[1]; } }

  void Awake() {
    if (WebCamTexture.devices.Length <= 0) { return; }

    var wcTexture = new WebCamTexture(320, 240, 15);
    _device = NyARUnityWebCam.CreateInstance(wcTexture);
    _panel.material.mainTexture = wcTexture;

    var config = new NyARMarkerSystemConfig(_device.width, _device.height);
    _arSystem = new NyARUnityMarkerSystem(config);
    _arSystem.setARBackgroundTransform(_panel.transform);
    _arSystem.setARCameraProjection(_camera);

    foreach (var model in GetModels()) { model.MarkerSetup(this); }
    _device.Start();
  }

  void OnDestroy() { _device.Stop(); }

  void FixedUpdate() {
    _device.Update();
    _arSystem.update(_device);
  }

  void ModelReset(ARModel model) {
    model.transform.position = Vector3.back * 100f;
    model.isVisible = false;
  }

  // TIPS: マーカーを規定数、認識できているかどうか
  bool EnableUpdate(ARModel model) {
    var enable = (_models.Count < _existsCount);
    if (enable) { enable = _arSystem.isExistMarker(model.id); }
    return enable;
  }

  // TIPS: ゲーム続行可能かどうか
  bool EnableGamePlay() {
    var exist1 = _arSystem.isExistMarker(player1.id);
    var exist2 = _arSystem.isExistMarker(player2.id);
    return exist1 && exist2;
  }

  /// <summary> マーカー検出 </summary>
  public bool DetectMarker() {
    _models.Clear();

    foreach (var model in GetModels()) {
      if (!EnableUpdate(model)) { ModelReset(model); continue; }
      _arSystem.setMarkerTransform(model.id, model.transform);
      _models.Add(model);
      model.transform.Rotate(Vector3.right * 90f);
    }

    return (_models.Count == _existsCount);
  }

  /// <summary> 認識済みマーカーのみを使って更新する </summary>
  public void ModelUpdate() {
    foreach (var model in _models) {
      if (!_arSystem.isExistMarker(model.id)) { ModelReset(model); continue; }
      model.isVisible = true;
      _arSystem.setMarkerTransform(model.id, model.transform);
      model.transform.Rotate(Vector3.right * 90f);
    }

    if (!_models.All(model => model.isVisible)) { return; }

    player1.transform.LookAt(player2.transform);
    player2.transform.LookAt(player1.transform);
  }
}
