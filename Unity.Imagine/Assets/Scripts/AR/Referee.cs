
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------
// NOTICE:
// レフェリーの機能一覧
//
//------------------------------------------------------------
// TIPS:
// ボードに対する操作は textBox プロパティを使用してください
//
//------------------------------------------------------------

public class Referee : MonoBehaviour
{
  [SerializeField]
  Text _textBox = null;
  /// <summary> レフェリーのボード </summary>
  public Text textBox { get { return _textBox; } }

  [SerializeField]
  Vector3 _gamePosition = Vector3.zero;
  /// <summary> ゲーム中のレフェリー待機位置 </summary>
  public Vector3 gamePosition { get { return _gamePosition; } }
  /// <summary> 待機位置と現在位置の距離 </summary>
  public Vector3 distance { get { return _gamePosition - transform.position; } }

  [SerializeField, Range(1f, 10f)]
  [Tooltip("待機位置までの移動速度")]
  float _velocity = 1f;

  /// <summary> 待機位置に向かって移動する </summary>
  public void MoveToGamePosition()
  {
    transform.position += distance * Time.deltaTime * _velocity;
  }

  /// <summary> レフェリーがクリックされたら true を返す </summary>
  public bool IsRaycastHit()
  {
    // TIPS: クリックされてなければ何もしない
    if (!TouchController.IsTouchBegan()) { return false; }

    // TIPS: レイキャスト判定でレフェリーに当たっていれば true を返す
    RaycastHit hit;
    return TouchController.IsRaycastHitWithLayer(out hit, gameObject.layer);
  }
}
