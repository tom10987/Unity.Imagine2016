using UnityEngine;
using System.Collections;

public class GameTest1 : ActionManager
{
    //[SerializeField]
    GameObject _bulletObj = null;
    GameObject bulletObj {
        get {
            if(_bulletObj == null)
            {
                _bulletObj = Resources.Load("MiniGame/Test/Sphere") as GameObject;
            }
            return _bulletObj;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action(ARModel model)
    {
        transform.LookAt(Enemy.transform);
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log(keyCode + " : ゲーム01テスト : " + Enemy.transform.name);

            var obj = Instantiate(bulletObj);
            var value = Enemy.transform.position - transform.position;
            obj.transform.position = transform.position;
            obj.transform.position += value.normalized;
            obj.GetComponent<BulletShot>()._vectorValue = value.normalized;
            obj.GetComponent<BulletShot>()._parent = gameObject;
        }
        //if (Input.GetKey(keyCode)) {
        //    transform.Rotate(5.0f, 0.0f, 0.0f);
        //}
    }
}
