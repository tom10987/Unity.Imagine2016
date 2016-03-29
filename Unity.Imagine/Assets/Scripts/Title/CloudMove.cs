
using UnityEngine;

public class CloudMove : MonoBehaviour
{
  [SerializeField]
  RectTransform _cloud = null;

  [SerializeField, Range(1f, 100f)]
  float _moveSpeed = 10f;

  [SerializeField, Range(500f, 1000f)]
  [Tooltip("画面右の雲が移動する x 座標")]
  float _rightBound = 500f;

  [SerializeField, Range(500f, 1000f)]
  [Tooltip("画面左側の雲が出現する x 座標")]
  float _leftBound = 500f;

  void FixedUpdate()
  {
    _cloud.position += Vector3.right * _moveSpeed * Time.deltaTime;
    if (_cloud.position.x < _rightBound) { return; }

    var position = _cloud.position;
    position.x = (_rightBound + _leftBound) * -1f;
    _cloud.position = position;
  }
}
