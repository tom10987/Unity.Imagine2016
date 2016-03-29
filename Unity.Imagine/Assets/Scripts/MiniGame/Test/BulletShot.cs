using UnityEngine;
using System.Collections;

public class BulletShot : MonoBehaviour {

    public Vector3 _vectorValue { get; set; }
    public GameObject _parent { get; set; }

    [SerializeField]
    float _bulletSpeed = 2;

    [SerializeField]
    float _bulletDeleteTime = 5;

    float _time ;

    void Start ()
    {
        _time = _bulletDeleteTime;
    }
	
	void Update ()
    {
        Renovation();
    }

    //これを読んでください
   public void Renovation()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            Destroy(gameObject);
        }
        transform.position += _vectorValue * _bulletSpeed;
    }


    void OnTriggerEnter(Collider collision)
    {
        if(_parent == null) { return; }

        if (_parent.transform.name == collision.transform.name)
        {
            Destroy(gameObject);
        }
    }
}
