using UnityEngine;
using System.Collections;

public class ResultEffectPosition : MonoBehaviour
{

    [SerializeField]
    float OFF_SET = 300.0f;

    [SerializeField]
    Vector3 EULER = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(EULER.x, EULER.y, EULER.z);

        var parent_position = transform.parent.position;
        transform.position = new Vector3
            (
                parent_position.x,
                parent_position.y + OFF_SET,
                parent_position.z
            );
    }
}
