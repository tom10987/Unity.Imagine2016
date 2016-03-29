using UnityEngine;
using System;
using System.Collections;

public class LookAtCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject _target;

    void Start()
    {

    }


    void Update()
    {
        transform.LookAt(_target.transform.localPosition,transform.up);
    }
}
