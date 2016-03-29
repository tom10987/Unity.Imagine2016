using UnityEngine;
using System.Collections;

public class Pendulum : ActionManager
{
    GameObject _shieldObj = null;
    public GameObject shieldObj
    {
        get
        {
            if(_shieldObj == null)
            {
                _shieldObj = Resources.Load("MiniGame/Pendulum/Shield") as GameObject;
            }
            return _shieldObj;
        }
    }

    // Use this for initialization
    void Start () {

    }

    public GameObject CreateShield(GameObject hitParticle)
    {
        var shield = Instantiate(shieldObj);
        shield.transform.position = transform.position;
        shield.transform.rotation = transform.rotation;
        shield.transform.Translate(new Vector3(0.0f, 0.0f, 0.8f * 40.0f));
        shield.transform.parent = transform;
        shield.name = shieldObj.name;
        shield.GetComponent<Shield>().hitParticle = hitParticle;
        _shieldObj = shield;
        //_shield.transform.Translate(shield.transform.position);
        //transform.LookAt(Enemy.transform);

        return _shieldObj;
    }

    // Update is called once per frame
    void Update () {

	}

    public override void Action(ARModel model)
    {
        transform.LookAt(Enemy.transform);
        rotation = transform.eulerAngles;
        if (_shieldObj == null)
        {
            if (GetComponentInChildren<Shield>() != null)
            {
                var shield = GetComponentInChildren<Shield>();
                shield.enabled = true;
                shield.Reset();
                var obj = GetComponentInChildren<Shield>().gameObject;
                if (obj != null) { _shieldObj = obj; }
            }
        }
        if (Input.GetKeyDown(keyCode))
        {
            //Debug.Log(keyCode + " : ペンデュラム : " + Enemy.transform.name);
            if (_shieldObj == null) { return; }
            _shieldObj.GetComponent<Shield>().PushOn();
        }
        //if (Input.GetKey(keyCode))
        //{
            //transform.Rotate(5.0f, 0.0f, 0.0f);
        //}
    }
}
