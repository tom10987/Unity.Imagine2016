
using UnityEngine;

public class ARModel : MonoBehaviour {

  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;

  [SerializeField]
  MeshRenderer _body = null;
  public MeshRenderer body { get { return _body; } }

  [SerializeField]
  MeshRenderer _clip = null;
  public MeshRenderer clip { get { return _clip; } }

  [SerializeField]
  Rigidbody _rigidBody = null;
  public Rigidbody rigidBody { get { return _rigidBody; } }

  [SerializeField]
  CharacterData _data = null;
  public CharacterData data { get { return _data; } }

  /// <summary> 弾が破裂したときのエフェクト </summary>
  public ParticleSystem effect { get; set; }

  /// <summary> 点数を表示する <see cref="Canvas"/> オブジェクト </summary>
  public Score scoreBoard { get; set; }

  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }

  /// <summary> マーカーを登録、ID を生成する </summary>
  public void MarkerSetup(ARDeviceManager device) {
    id = device.arSystem.addARMarker(_marker,
                                     device.markerResolution,
                                     device.markerEdge,
                                     device.markerScale);
  }

  /// <summary> 対象オブジェクトの方向に、正面を向ける </summary>
  public void LookAt(Transform target) {
    var distance = target.position - transform.position;
    transform.rotation.SetLookRotation(distance);
  }

  /// <summary> マーカーがカメラに認識されたか </summary>
  public bool isVisible { get; set; }

  /// <summary> ゲームで使用するキーバインド </summary>
  public System.Func<bool> inputKey { get; set; }
}
