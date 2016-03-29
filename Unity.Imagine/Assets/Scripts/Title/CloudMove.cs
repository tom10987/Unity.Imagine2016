using UnityEngine;
using System.Collections;

public class CloudMove : MonoBehaviour
{

    //移動する雲
    [SerializeField]
    GameObject _cloud = null;
    //雲の移動速度
    [SerializeField]
    float _moveSpeed;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _cloud.transform.localPosition =
          new Vector3(_cloud.transform.localPosition.x + _moveSpeed * Time.deltaTime,
                      _cloud.transform.localPosition.y,
                      _cloud.transform.localPosition.z);

        if (_cloud.transform.localPosition.x <= 1100)
            return;

        //範囲の外に出たら左側に戻す
            _cloud.transform.localPosition =
            new Vector3(_cloud.transform.localPosition.x - 2800,
                        _cloud.transform.localPosition.y,
                        _cloud.transform.localPosition.z);
    }
}
