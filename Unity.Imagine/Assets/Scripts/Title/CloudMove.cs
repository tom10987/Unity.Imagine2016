
using UnityEngine;

public class CloudMove : MonoBehaviour
{
  [SerializeField]
  RectTransform _cloud = null;

  [SerializeField, Range(1f, 100f)]
  float _moveSpeed = 10f;

  [SerializeField, Range(900f, 1100f)]
  [Tooltip("画面右の雲が移動する x 座標")]
  float _rightBound = 1000f;

  [SerializeField, Range(900f, 1100f)]
  [Tooltip("画面左側の雲が出現する x 座標")]
  float _leftBound = 1000f;

  void FixedUpdate()
  {
    _cloud.position += Vector3.right * _moveSpeed * Time.deltaTime;
    if (_cloud.position.x < _rightBound) { return; }

    var position = _cloud.position;
    position.x = (_rightBound + _leftBound) * -1f;
    _cloud.position = position;
  }
}
