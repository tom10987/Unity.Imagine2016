using UnityEngine;
using System.Collections;

public class FireWorksCreate : MonoBehaviour {

    [SerializeField]
    float _coolDownTime = 1;

    float _time;

    [SerializeField]
    Vector3 _changePosition = new Vector3(0,-30,0);

    [SerializeField]
    GameObject _particle;

	void Start ()
    {
        _time = _coolDownTime;
	}
	
	void Update ()
    {
        Create();
	}

    public void Create()
    {
        if (CresteCoolDown())
        {
            ParticleCreate();
            _time = _coolDownTime;
        }
    }

    bool CresteCoolDown()
    {
        _time -= Time.deltaTime;

        
        if (_time <= 0) return true;
        return false;

    }

    void ParticleCreate()
    {
        GameObject particleObj = Instantiate(_particle);
        particleObj.transform.SetParent(gameObject.transform, false);
        
        particleObj.transform.localPosition = gameObject.transform.position + RandomPosition() + _changePosition;
    }

    Vector3 RandomPosition()
    {
        Vector3 position = new Vector3(0,0,0);
        position.x = Random.Range(-50, 51);
        position.z = Random.Range(-50, 51);
        return position;
    }

}
