using UnityEngine;
using System.Collections;

public class HitEffectCreate : MonoBehaviour
{
    [SerializeField]
    GameObject[] _effectObject;

    void Start()
    {

    }

    void Update()
    {
         
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HitEffectCreate>() == null) return;
        
            foreach (ContactPoint point in collision.contacts)
            {
                foreach (GameObject effect in _effectObject)
                {
                    effect.SetActive(true);
                    effect.transform.position = (Vector3)point.point;
                }
            }
        
    }  

}
