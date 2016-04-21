
using UnityEngine;

public class ARModel : MonoBehaviour
{
  [SerializeField]
  [Tooltip("このモデルを表示するマーカー")]
  Texture2D _marker = null;
  /// <summary> モデルを表示するマーカー </summary>
  public Texture2D marker { get { return _marker; } }

  /// <summary> マーカーに対応した ID </summary>
  public int id { get; private set; }


  [SerializeField]
  MeshRenderer _body = null;
  /// <summary> 大砲本体の <see cref="MeshRenderer"/> </summary>
  public MeshRenderer bodyRenderer { get { return _body; } }

  [SerializeField]
  MeshRenderer _clip = null;
  /// <summary> 大砲の足の <see cref="MeshRenderer"/> </summary>
  public MeshRenderer clipRenderer { get { return _clip; } }


  [SerializeField]
  [Tooltip("帽子の RigidBody を登録")]
  Rigidbody _cap = null;
  /// <summary> 装備しているコスチューム </summary>
  public Rigidbody costume { get { return _cap; } }


  [SerializeField]
  CharacterData _data = null;
  /// <summary> キャラクターのコスチュームを含めた能力値 </summary>
  public CharacterData data { get { return _data; } }


  /// <summary> マーカーがカメラに認識されたか </summary>
  public bool isVisible { get; set; }

  /// <summary> マーカーを登録、ID を生成する <para></para>
  /// TIPS: ID を 外部から書き換えられてはいけないため、
  /// <see cref="ARDeviceManager"/> を内部に引き入れて ID を生成している </summary>
  public void MarkerSetup(ARDeviceManager device)
  {
    id = device.arSystem.addARMarker(_marker,
                                     device.markerResolution,
                                     device.markerEdge,
                                     device.markerScale);
  }
}
