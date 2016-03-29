using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// デバッグ用
    /// </summary>
    [SerializeField]
    CharacterParameter _characterParamter;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }
}