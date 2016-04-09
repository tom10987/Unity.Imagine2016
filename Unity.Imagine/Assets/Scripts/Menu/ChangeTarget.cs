using UnityEngine;
using System.Collections;

public class ChangeTarget : MonoBehaviour
{
    [SerializeField]
    GameObject[] _targetParent = null;

    void Start()
    {
        ChangeTargetCursor(0);
    }
 
    public void ChangeTargetCursor(int _selectTargetNum)
    {
        transform.SetParent(_targetParent[_selectTargetNum].transform);
        transform.localPosition = new Vector3(0, 0, 0.03f);
        transform.localRotation = Quaternion.identity;
    }
}
