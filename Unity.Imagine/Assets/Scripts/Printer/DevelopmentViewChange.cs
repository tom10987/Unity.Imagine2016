using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DevelopmentViewChange : MonoBehaviour
{

    private int _index = 0;

    private CharacterParameterInfo _info;

    private Image _image = null;
    private List<Sprite> _sprite;

    public struct Parameter
    {
        public CharacterParameter.ModelType modelType;
        public CharacterParameter.CostumeType costumeType;
        public CharacterParameter.DecorationType decorationType;
    }

    Parameter _param;
    Dictionary<Parameter, int> _params = new Dictionary<Parameter, int>();

    void Start()
    {
        _param = new Parameter();
        Load();
        _image = GetComponent<Image>();
        _info = GameObject.Find("ParameterSave").GetComponent<CharacterParameterInfo>();
        _sprite = new List<Sprite>();
        _sprite.AddRange(Resources.LoadAll<Sprite>("DevelopmentView"));
        _param.modelType = _info.getCharacterParameter.modelType;
        _param.costumeType = _info.getCharacterParameter.costumeType;
        _param.decorationType = _info.getCharacterParameter.decorationType;
        _index = _params[_param];
        if(_index > _sprite.Count)throw new IndexOutOfRangeException("_sprite index out of range");
        _image.sprite = _sprite[_index];
    }

    /// <summary>
    /// Parameterの組み合わせにつきID追加
    /// </summary>
    void Load()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    _params.Add(_param, _index);
                    _index++;
                    _param.decorationType++;
                }
                _param.decorationType = CharacterParameter.DecorationType.NONE;
                _param.costumeType++;
            }
            _param.decorationType = CharacterParameter.DecorationType.NONE;
            _param.costumeType = CharacterParameter.CostumeType.A;
            _param.modelType++;
        }
    }
}
