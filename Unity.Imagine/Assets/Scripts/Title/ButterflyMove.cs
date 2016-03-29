using UnityEngine;
using System.Collections;

public class ButterflyMove : MonoBehaviour
{
    [SerializeField, Tooltip("サイズ変更用")]
    private float _scale = 1.0f;

    private float _sinWaveCount = 0.0f;

    [SerializeField, Tooltip("スピード変更用")]
    private float _moveSpeed = 60.0f;

    [SerializeField, Tooltip("縦の動きの幅")]
    private float _sinMove = 5.0f;

    private float _startPosY = 0.0f;

    [SerializeField, Tooltip("端から出現したときに出てくるY座標を乱数で動かす")]
    private float _randomPosY = 0.0f;

    private float _randomMoveY = 0.0f;


    void Start()
    {
        transform.localScale = Vector3.one * _scale;
        _startPosY = transform.localPosition.y;
    }

    void Update()
    {
        UpdateOfMove();
    }

    private void UpdateOfMove()
    {
        _sinWaveCount += Time.deltaTime;

        transform.localPosition =
            new Vector3(transform.localPosition.x + _moveSpeed * Time.deltaTime,
                        transform.localPosition.y + UnityEngine.Mathf.Sin(_sinWaveCount) * _sinMove,
                        transform.localPosition.z);

        if (transform.localPosition.x < -165)
        {
            _randomMoveY = Random.Range(-_randomPosY, _randomPosY);
            _moveSpeed *= -1;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            transform.localPosition =
            new Vector3(-165.0f,
                        _startPosY + _randomMoveY,
                        transform.localPosition.z);
        }
        else if (transform.localPosition.x > 165)
        {
            _randomMoveY = Random.Range(-_randomPosY, _randomPosY);
            _moveSpeed *= -1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition =
            new Vector3(165.0f,
                        _startPosY + _randomMoveY,
                        transform.localPosition.z);
        }
    }
}
