
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NyAR.MarkerSystem;
using NyARUnityUtils;

//------------------------------------------------------------
// NOTICE:
// 接続されているデバイスとしてのカメラを管理する
//
//------------------------------------------------------------

public class ARDeviceManager : MonoBehaviour
{
  NyARUnityWebCam _device = null;
  /// <summary> AR マーカーの認識に使用する、デバイスとしてのカメラ </summary>
  public NyARUnityWebCam device { get { return _device; } }

  NyARUnityMarkerSystem _arSystem = null;
  /// <summary> AR ライブラリの機能群 </summary>
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


  [SerializeField, Range(-180f, 180f)]
  [Tooltip("モデルの回転補正：X軸")]
  float _axisX = 0f;

  [SerializeField, Range(-180f, 180f)]
  [Tooltip("モデルの回転補正：Y軸")]
  float _axisY = 0f;

  [SerializeField, Range(-180f, 180f)]
  [Tooltip("モデルの回転補正：Z軸")]
  float _axisZ = 0f;


  /// <summary> 管理下にある <see cref="ARModel"/> を全て取得 </summary>
  public IEnumerable<ARModel> GetModels() { return this.GetOnlyChildren<ARModel>(); }

  // TIPS: プレイヤーの人数
  readonly int _existsCount = 2;

  // TIPS: 認識できたマーカーに関連付けられたモデルのデータを格納
  List<ARModel> _models = new List<ARModel>();

  /// <summary> ゲーム開始後に使用するモデルの一覧 </summary>
  public IEnumerable<ARModel> models { get { return _models; } }
  public ARModel player1 { get { return _models[0]; } }
  public ARModel player2 { get { return _models[1]; } }

  /// <summary> モデルが全て認識できていれば true を返す </summary>
  public bool existsModels { get { return _models.All(model => model.isVisible); } }

  void Start()
  {
    // TIPS: デバイス側カメラが接続されてなければ処理をスキップ
    if (WebCamTexture.devices.Length <= 0) { return; }

    // TIPS: デバイス側カメラが写した映像を反映するテクスチャの登録と初期化
    var wcTexture = new WebCamTexture(320, 200, 15);
    _device = NyARUnityWebCam.CreateInstance(wcTexture);
    _panel.material.mainTexture = wcTexture;

    // TIPS: ゲームカメラの設定を初期化
    var config = new NyARMarkerSystemConfig(_device.width, _device.height);
    _arSystem = new NyARUnityMarkerSystem(config);
    _arSystem.setARBackgroundTransform(_panel.transform);
    _arSystem.setARCameraProjection(_camera);

    // TIPS: マーカー画像から固有の ID を生成
    foreach (var model in GetModels()) { model.MarkerSetup(this); }

    // TIPS: デバイス側カメラの起動
    _device.Start();
  }

  // TIPS: インスタンスが削除されたらデバイス側のカメラを止める
  void OnDestroy() { _device.Stop(); }

  void Update()
  {
    // TIPS: デバイスと AR ライブラリの状態を更新
    _device.Update();
    _arSystem.update(_device);
  }


  // TIPS: マーカーを認識できなかったモデルの座標を初期化
  void ResetTransform(ARModel model)
  {
    model.transform.position = Vector3.back * 100f;
    model.isVisible = false;
  }

  // TIPS: マーカーを規定数、認識できているかどうか
  bool EnableUpdate(ARModel model)
  {
    var enable = (_models.Count < _existsCount);
    // TIPS: 認識済みマーカーの数が規定数未満なら、マーカーの検出を実行
    if (enable) { enable = _arSystem.isExistMarker(model.id); }
    return enable;
  }

  // TIPS: ゲーム続行可能かどうか
  bool EnableGamePlay()
  {
    var exist1 = _arSystem.isExistMarker(player1.id);
    var exist2 = _arSystem.isExistMarker(player2.id);
    return exist1 && exist2;
  }


  /// <summary> マーカーを検出、規定数（２体）認識できたら true を返す </summary>
  public bool DetectMarker()
  {
    // TIPS: 認識済みモデルの数をリセット
    _models.Clear();

    // TIPS: マーカー検出
    foreach (var model in GetModels())
    {
      if (!EnableUpdate(model)) { ResetTransform(model); continue; }
      // TIPS: マーカーを認識できたらモデルを画面に表示する
      _arSystem.setMarkerTransform(model.id, model.transform);
      _models.Add(model);
      // TIPS: モデルが変な方向に向いてしまうので補正する
      model.transform.Rotate(Vector3.right * _axisX);
      model.transform.Rotate(Vector3.up * _axisY);
      model.transform.Rotate(Vector3.forward * _axisZ);
    }

    return (_models.Count == _existsCount);
  }

  /// <summary> 認識済みの AR モデル以外を削除 </summary>
  public void RemoveModel()
  {
    var models = GetModels().Except(_models);
    foreach (var model in models) { Destroy(model.gameObject); }
  }

  /// <summary> 認識済みマーカーのみを使ってモデルを更新する </summary>
  public void ModelUpdate()
  {
    // TIPS: AR マーカーの検出
    foreach (var model in _models)
    {
      if (!_arSystem.isExistMarker(model.id)) { ResetTransform(model); continue; }
      model.isVisible = true;
      _arSystem.setMarkerTransform(model.id, model.transform);
      // TIPS: モデルの向きを補正する
      model.transform.Rotate(Vector3.right * _axisX);
      model.transform.Rotate(Vector3.up * _axisY);
      model.transform.Rotate(Vector3.forward * _axisZ);
    }

    // TIPS: マーカーが認識できていなければスキップ
    if (!_models.All(model => model.isVisible)) { return; }

    // TIPS: AR モデルをそれぞれの対戦相手の方向に向ける
    player1.transform.LookAt(player2.transform);
    player2.transform.LookAt(player1.transform);
  }
}
