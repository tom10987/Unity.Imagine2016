using UnityEngine;

public class SequencerNotDestoryer : MonoBehaviour
{

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
