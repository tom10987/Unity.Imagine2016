
using UnityEngine;

public class SpeedGameShot : MonoBehaviour
{
  [SerializeField, Range(10f, 20f)]
  [Tooltip("弾の速度")]
  float _velocity = 15f;

  [SerializeField, Range(0f, 100f)]
  [Tooltip("弾の初期座標：高さ")]
  float _offsetY = 40f;
  public Vector3 offsetY { get { return Vector3.up * _offsetY; } }

  [SerializeField, Range(0f, 100f)]
  [Tooltip("弾の初期座標：正面方向")]
  float _offsetForward = 50f;
  public float offsetForward { get { return _offsetForward; } }


  [SerializeField, Range(1f, 100f)]
  [Tooltip("エフェクトのサイズ")]
  float _effectScale = 20f;

  /// <summary> 弾のエフェクト </summary>
  public OneTimeEffect effect { get; set; }


  /// <summary> 弾の目標オブジェクト </summary>
  public Transform target { get; set; }
  /// <summary> 目標オブジェクトとの距離 </summary>
  Vector3 distance { get { return target.position - transform.position + offsetY; } }

  /// <summary> 目標オブジェクトと接触したときのコールバック処理 </summary>
  public System.Action listener { get; set; }


  // TIPS: 一度発射されたらターゲットに向かって進み続ける
  void Update()
  {
    var velocity = distance.normalized * _velocity * Time.deltaTime * 10f;
    transform.position += velocity;

    // TIPS: ターゲットに近づくまで何もしない
    if (distance.magnitude > 20f) { return; }

    // TIPS: コールバック実行（得点加算）
    listener();

    // TIPS: エフェクト実行
    var e = Instantiate(effect);
    e.transform.position = transform.position;
    e.transform.localScale = Vector3.one * _effectScale;
    Destroy(gameObject);
  }
}
