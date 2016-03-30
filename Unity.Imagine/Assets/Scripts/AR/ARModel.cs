
using UnityEngine;

public class ARModel : MonoBehaviour {

  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;

  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }

  [SerializeField]
  MeshRenderer _body = null;
  public MeshRenderer bodyRenderer { get { return _body; } }

  [SerializeField]
  MeshRenderer _clip = null;
  public MeshRenderer clipRenderer { get { return _clip; } }

  [SerializeField]
  [Tooltip("帽子の RigidBody を登録")]
  Rigidbody _cap = null;
  public Rigidbody cap { get { return _cap; } }

  [SerializeField]
  CharacterData _data = null;
  public CharacterData data { get { return _data; } }

  /// <summary> 弾が破裂したときのエフェクト </summary>
  public GameEffect effect { get; set; }

  /// <summary> 点数を表示する <see cref="Canvas"/> オブジェクト </summary>
  public Score scoreBoard { get; set; }

  /// <summary> マーカーがカメラに認識されたか </summary>
  public bool isVisible { get; set; }

  /// <summary> ゲーム機能の呼び出し </summary>
  public AbstractGame game { get; private set; }
  public void GameSetup(AbstractGame game) { this.game = game; }

  /// <summary> マーカーを登録、ID を生成する </summary>
  public void MarkerSetup(ARDeviceManager device) {
    id = device.arSystem.addARMarker(_marker,
                                     device.markerResolution,
                                     device.markerEdge,
                                     device.markerScale);
  }
}
