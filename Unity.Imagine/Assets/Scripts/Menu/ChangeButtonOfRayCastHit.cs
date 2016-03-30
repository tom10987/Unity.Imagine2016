using UnityEngine;
using System.Collections;

public class ChangeButtonOfRayCastHit : MonoBehaviour
{


    
    void Update()
    {
        if (TouchController.IsTouchBegan())
        {
            var hitObject = new RaycastHit();
            var isHit = TouchController.IsRaycastHit(out hitObject);

           if(isHit == true && hitObject.transform.name.GetHashCode() == gameObject.transform.name.GetHashCode())
            {
                Hit();
            }
        }

        else if(TouchController.IsTouchEnded())
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, 1.0f);
        }
    }

    void Hit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(200.0f/ 255.0f, 200.0f / 255.0f, 200.0f / 255.0f,1.0f);
    }

}
