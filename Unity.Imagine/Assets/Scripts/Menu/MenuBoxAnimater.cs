using UnityEngine;
using System.Collections;

public class MenuBoxAnimater : MonoBehaviour
{
    [SerializeField]
    bool _isPlay;
    [SerializeField]
    bool _isBack;
    [SerializeField]
    float _animationSpeed;

    Animator _animator = null;

    public bool isPlay
    {
        get
        {
            return _isPlay;
        }

        set
        {
            _isPlay = value;
        }
    }

    public bool isBack
    {
        get
        {
            return _isBack;
        }

        set
        {
            _isBack = value;
        }
    }

    public double animationTime
    {
        get
        {
            return _animator.GetTime();
        }

        set
        {
            _animator.SetTime(value);
        }

    }

    public float animationSpeed
    {
        get
        {
            return _animationSpeed;
        }

        set
        {
            _animationSpeed = value;
        }
    }

    private bool _isStarted = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isPlay == true && _isStarted == false)
        {
            _animator.SetTrigger("isPlay");
            _isPlay = false;
            _isStarted = true;
        }

        if (_isBack == true && _isStarted == true)
        {
            _animator.SetTrigger("isBack");
            _isBack = false;
            _isPlay = false;
            _isStarted = false;
        }
    }

}
