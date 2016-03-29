using UnityEngine;
using System.Collections;

public class CountDestroy : MonoBehaviour {

    [SerializeField]
    float _second = 1.5f;

    float _count = 0.0f;

	// Use this for initialization
	void Start () {
        _second *= 60.0f;
    }
	
	// Update is called once per frame
	void Update () {
        ++_count;
        if (_count > _second)
        {
            Destroy(gameObject);
        }
    }
}
