using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatesChange : MonoBehaviour
{

    private CharacterParameterInfo _info = null;
    [SerializeField]
    private List<Sprite> _states = null;
    private Image _image = null;

    private enum ModelType
    {
        MODEL,
        COSTUME,
        DECORATION
    }

    [SerializeField]
    private ModelType _type;

    private CharacterParameter.ModelType _model;
    private CharacterParameter.CostumeType _costume;
    private CharacterParameter.DecorationType _decoration;

    void Start()
    {
        Load();

        if (_type == ModelType.MODEL)
        {
            _model = _info.getCharacterParameter.modelType;
            _image.sprite = _states[(int)_model];
        }
        else if (_type == ModelType.COSTUME)
        {
            _costume = _info.getCharacterParameter.costumeType;
            _image.sprite = _states[(int)_costume];
        }
        else if (_type == ModelType.DECORATION)
        {
            _decoration = _info.getCharacterParameter.decorationType;
            _image.sprite = _states[(int)_decoration];
        }
    }

    /// <summary>
    /// 最初の読み込み
    /// </summary>
    void Load()
    {
        _image = GetComponent<Image>();
        _info = GameObject.Find("ParameterSave").GetComponent<CharacterParameterInfo>();
        _states = new List<Sprite>();
        if (_type == ModelType.MODEL)
        {
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_1_A"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_1_B"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_1_C"));
        }
        else if (_type == ModelType.COSTUME)
        {
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_2_A"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_2_B"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_2_C"));
        }
        else if (_type == ModelType.DECORATION)
        {
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_3_0"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_3_A"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_3_B"));
            _states.Add(Resources.Load<Sprite>("MakeOfCharacter/Texture/Description/Customize_setumei_3_C"));
        }
    }

    void Update()
    {
    }

}
