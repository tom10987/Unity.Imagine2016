using UnityEngine;
using System;

public class CharacterParameterInfo : MonoBehaviour
{

    static CharacterParameter _characterParameter;

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

    public CharacterParameter getCharacterParameter
    {
        get
        {
            return _characterParameter;
        }
    }

    public void Decide()
    {
        var changeCharacterPattern = FindObjectOfType<ChangeCharacterPattern>();
        if (changeCharacterPattern == null) throw new NullReferenceException("ChangeCharacterPattern is nothing");
        _characterParameter = changeCharacterPattern.getCharacterParamter;
    }
}
