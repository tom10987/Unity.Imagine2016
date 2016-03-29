using UnityEngine;
using System.Collections;

public class UIMover : MonoBehaviour {

    [SerializeField]
    Vector3 _targetPos;
    public Vector3 targetPos
    {
        get { return _targetPos; }
        set { _targetPos = value; }
    }

    [SerializeField]
    float _speed = 10.0f;

    RectTransform rect { get; set; }

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        rect.anchoredPosition3D -= (rect.anchoredPosition3D - _targetPos) / _speed;
    }
}
