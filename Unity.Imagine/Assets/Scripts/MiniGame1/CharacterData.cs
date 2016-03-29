using UnityEngine;
using System.Collections;

public class CharacterData : MonoBehaviour {
    [SerializeField]
    CharacterParameter _characterParameter;

    public CharacterParameter getCharacterData
    {
        get
        {
            return _characterParameter;
        }
    }
}
