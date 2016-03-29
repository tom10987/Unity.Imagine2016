using UnityEngine;
using System.Collections;

public class MultiTouch : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    protected delegate void TouchAction();

    protected void TouchUpdate(TouchAction action)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.touches[i].position);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
            }

        }
    }

    void A()
    {

    }

    void Start()
    {
        if (Input.touchSupported)
        {
            Debug.Log("Touch対応してる");
        }
        else
        {
            Debug.Log("Touch対応してない");
        }
    }

    void Update()
    {
        //Debug.Log(Input.touches.Length);

        TouchAction a = A;
        TouchUpdate(a);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
            }
        }
    }
}
