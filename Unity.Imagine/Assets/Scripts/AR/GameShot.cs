
using UnityEngine;

public class GameShot : MonoBehaviour
{
  [SerializeField, Range(1f, 20f)]
  [Tooltip("弾の速度")]
  float _velocity = 5f;

  [SerializeField]
  [Tooltip("弾の初期座標")]
  Vector3 _offset = Vector3.zero;
  public Vector3 offset { get { return _offset; } }


  [SerializeField, Range(1f, 100f)]
  [Tooltip("エフェクトのサイズ")]
  float _effectScale = 10f;

  /// <summary> 弾のエフェクト </summary>
  public GameEffect effect { get; set; }


  /// <summary> 弾の目標オブジェクト </summary>
  public Transform target { get; set; }
  /// <summary> 目標オブジェクトとの距離 </summary>
  Vector3 distance { get { return target.position - transform.position + _offset; } }

  /// <summary> 目標オブジェクトと接触したときのコールバック処理 </summary>
  public System.Action listener { get; set; }


  // TIPS: 一度発射されたらターゲットに向かって進み続ける
  void FixedUpdate()
  {
    var velocity = distance.normalized * _velocity;
    transform.position += velocity;

    if (distance.magnitude > 20f) { return; }
    listener();
    var e = Instantiate(effect);
    e.transform.position = transform.position;
    e.transform.localScale = Vector3.one * _effectScale;
    e.particle.Play();
    Destroy(gameObject);
  }
}
