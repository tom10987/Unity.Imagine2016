using UnityEngine;
using System.Collections;

public class SelectGameStatus : MonoBehaviour
{
    [SerializeField]
    int _selectGameNum;

    public int SelectGameNum
    {
        get { return _selectGameNum; }

        set { _selectGameNum = value; }
    }


    static GameObject _instance = null;
    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
