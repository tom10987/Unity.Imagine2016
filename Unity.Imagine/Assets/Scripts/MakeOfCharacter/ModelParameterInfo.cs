using UnityEngine;

public class ModelParameterInfo : MonoBehaviour
{
    [SerializeField]
    ModelParameter _modelParameter;

    public ModelParameter getModelParameter
    {
        get
        {
            return _modelParameter;
        }
    }
}