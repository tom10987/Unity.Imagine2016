using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {

    [SerializeField]
    GameMnueTab _gameMnueTab;

    [SerializeField]
    Vector3 _targetPos;
    public Vector3 targetPos { get { return _targetPos; } }

    [SerializeField]
    float _speed = 10.0f;

    Vector3 _initPos;

    // Use this for initialization
    void Start()
    {
        _initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameMnueTab.isPushPlayButton) {
            transform.position = _initPos;
            return;
        }
        transform.position -= (transform.position - _targetPos) / _speed;
    }
}
