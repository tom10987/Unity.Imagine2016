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

    void Start()
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_isPlay == true)
        {
            _animator.SetTrigger("isPlay");
            _isPlay = false;
        }

        if (_isBack == true)
        {
            Debug.Log("Back");
            _animator.SetTrigger("isBack");
            _isBack = false;
        }
    }

    public void Play(string _stateName,float _normalizedtime)
    {
        Debug.Log(_animator);
        _animator.Play(_stateName, 0, _normalizedtime);
    }

    public void Stop()
    {
        _animator.Stop();
    }

}
