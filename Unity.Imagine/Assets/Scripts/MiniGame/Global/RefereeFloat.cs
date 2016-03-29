using UnityEngine;
using System.Collections;

public class RefereeFloat : MonoBehaviour {

    [SerializeField]
    float _speed = 2.0f;

    [SerializeField,Range(100f,500f)]
    float _length = 300.0f;

    //Vector3 _initPos;

    float _time = 0.0f;

	// Use this for initialization
	void Start () {
        //_initPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 sin = Vector3.up * Mathf.Sin(++_time * _speed) * Time.deltaTime * _length;
        transform.position += (/*_initPos + */sin) / 10.0f;
    }
}
