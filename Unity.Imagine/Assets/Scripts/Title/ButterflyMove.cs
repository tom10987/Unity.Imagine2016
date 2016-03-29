
using UnityEngine;

public class ButterflyMove : MonoBehaviour
{
    [SerializeField, Range(1f, 10f), Tooltip("サイズ変更用")]
    private float _scale = 1.0f;

    private float _sinWaveCount = 0.0f;

    [SerializeField, Range(1f, 120f), Tooltip("スピード変更用")]
    private float _moveSpeed = 60.0f;

    [SerializeField, Range(0.1f, 5f), Tooltip("縦の動きの幅")]
    private float _sinMove = 0.1f;

    private float _startPosY = 0.0f;

    [SerializeField, Range(0f, 1f), Tooltip("端から出現したときに出てくるY座標を乱数で動かす")]
    private float _randomPositionY = 2.5f;

    [SerializeField, Range(150f, 170f)]
    [Tooltip("移動方向を反転させる x 座標")]
    float _reverseRange = 165f;

    [SerializeField]
    Vector3 _rightDirection = Vector3.zero;

    [SerializeField]
    Vector3 _leftDirection = Vector3.back * 90f;

    void Start()
    {
        transform.localScale = Vector3.one * _scale;
        _startPosY = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        _sinWaveCount += Time.deltaTime;

        var moveX = Vector3.right * _moveSpeed * Time.deltaTime;
        var moveY = Vector3.up * Mathf.Sin(_sinWaveCount) * _sinMove / 2;
        transform.localPosition += (moveX + moveY);

        var absX = Mathf.Abs(transform.localPosition.x);
        if (absX < _reverseRange) { return; }

        var isLeft = transform.localPosition.x < 0f;

        // TIPS: 移動方向反転して、移動範囲内に少し戻す
        _moveSpeed *= -1;
        transform.localPosition += isLeft ? Vector3.right : Vector3.left;

        // TIPS: オブジェクトの y 座標を少し移動する
        var randomY = Random.Range(-1f, 1f) * _randomPositionY;
        var position = transform.localPosition;
        position.y = (_startPosY + randomY);
        transform.localPosition = position;

        // TIPS: オブジェクトの向きを反転
        transform.localRotation = Quaternion.Euler(isLeft ? _leftDirection : _rightDirection);
    }
}
