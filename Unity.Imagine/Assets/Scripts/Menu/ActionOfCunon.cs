using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class ActionOfCunon : MonoBehaviour
{
    [SerializeField]
    AudioPlayer _player = null;

    [SerializeField]
    GameObject _bullet = null;

    [SerializeField]
    Vector3 _target;

    [SerializeField]
    GameObject _animationStop = null;

    private float _gravityCount = 0.0f;

    private float _gravity = 1.5f;

    private bool _isShot = false;

    private bool _isStart = false;

    public bool isStart
    {
        get
        {
            return _isStart;
        }

        set
        {
            _isStart = value;
        }
    }

    private bool _isEnd = false;

    public bool isEnd
    {
        get
        {
            return _isEnd;
        }

        set
        {
            _isEnd = value;
        }
    }

    void Update()
    {
        if (isStart != true) return;
        Fall();
        Move();
    }

    private void Fall()
    {
        _gravityCount += Time.deltaTime;

        gameObject.transform.position
            = Vector3.MoveTowards(gameObject.transform.position, _animationStop.transform.position,
                                  _gravityCount * _gravityCount * _gravity);
    }

    private void Move()
    {
        if (_gravityCount > 1.3f && _isEnd == false)
        {
            if (_isShot == false)
            {
                _player.Play(20, 1.0f, false);
                _isShot = true;
            }

            _bullet.transform.position
                = Vector3.MoveTowards(_bullet.transform.position, _target, 0.5f);

            _bullet.transform.localScale =
                new Vector3(_bullet.transform.localScale.x + 0.1f,
                            _bullet.transform.localScale.y + 0.1f,
                            _bullet.transform.localScale.z + 0.1f);

            if (_gravityCount > 2.0f)
            {
                _isEnd = true;
                _player.Play(21, 1.0f, false);
            }
        }
    }

}
