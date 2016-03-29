using UnityEngine;
using System.Collections;

public class PropellerRotation : MonoBehaviour
{
    [SerializeField, Range(0f, 90f)]
    float _angle = 1f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * _angle);
    }
}
