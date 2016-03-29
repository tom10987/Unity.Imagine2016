using UnityEngine;

public class SelectAnimator : MonoBehaviour
{

    Animator _animator = null;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Start");
    }

    public bool IsPlay()
    {
        return _animator.GetTime() < 1.0f;
    }
    
}
