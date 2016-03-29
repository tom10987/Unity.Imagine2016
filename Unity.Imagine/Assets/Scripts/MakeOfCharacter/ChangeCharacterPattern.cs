using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/*
2 / 29
14 : 40 野本　変更

    前回のをかえて、
    Listに処理をいれ関数を一々作るのを辞めました

    各ボタンごとに番号をしていてやっています
    Test
*/

public class ChangeCharacterPattern : MonoBehaviour
{

    //Typeのモデル
    [SerializeField]
    GameObject[] _typePrefabs = null;

    //Costumeのモデル
    [SerializeField]
    GameObject[] _costumePrefabs = null;

    [SerializeField]
    GameObject _characterPlace = null;

    [SerializeField]
    Image _descriptionType = null;

    [SerializeField]
    Image _descriptionCostume = null;

    [SerializeField]
    Image _descriptionDecoration = null;

    [SerializeField]
    Transform _panelOfChangeType = null;

    [SerializeField]
    Transform _panelOfChangeCostume = null;

    [SerializeField]
    Transform _panelOfChangeDecoration = null;

    const int OFFSET_INDEX_TYPE = 0;
    const int OFFSET_INDEX_COSTUME = 3;
    const int OFFSET_INDEX_DECORATION = 6;

    List<Sprite> _descriptionSprites = new List<Sprite>();
    List<Texture> _characterTextures = new List<Texture>();

    GameObject _character = null;

    CharacterParameter _characterParamter = new CharacterParameter();
    ParameterBar _parameterBar = null;
    CharacterParameterInfo _characterParameterInfo = null;

    AudioPlayer _audioPlayer = null;

    CharacterViewController _characterViewController = null;

    bool _isPush = false;
    bool _isDecide = false;

    public CharacterParameter getCharacterParamter
    {
        get
        {
            return _characterParamter;
        }
    }

    public void Decide()
    {
        if (ScreenSequencer.instance.isEffectPlaying) return;
        if (_isDecide) return;
        _isDecide = true;
        _audioPlayer.Stop();
        _audioPlayer.Play(8);
        StartCoroutine(DecideCorutine());
        StartCoroutine(Transition());
    }

    //
    public void PushOfBackTitle()
    {
        //右上のButtonを押したら

        var screenSequencer = ScreenSequencer.instance;

        if (screenSequencer.isEffectPlaying) return;
        if (_isDecide) return;

        _audioPlayer.Stop();
        _audioPlayer.Play(7);

        screenSequencer.SequenceStart
        (
            () => { GameScene.Menu.ChangeScene(); },
            new Fade(1.0f)
        );
    }

    public void SetType(int index)
    {
        if ((uint)index > (int)CharacterParameter.ModelType.NONE) throw new ArgumentException("type is error");
        if (_isDecide) return;

        if (_isPush) return;
        _isPush = true;

        _characterViewController.ResetTransform();
        _audioPlayer.Play(5);

        StartCoroutine(ChangeType(index));
    }

    public void SetCostume(int index)
    {
        if ((uint)index > (int)CharacterParameter.CostumeType.NONE) throw new ArgumentException("costume is error");
        if (_isDecide) return;

        if (_isPush) return;
        _isPush = true;

        //_characterViewController.ResetTransform();
        _audioPlayer.Play(5);

        StartCoroutine(ChangeCostume(index));
    }

    public void SetDecoration(int index)
    {
        if ((uint)index > (int)CharacterParameter.DecorationType.C) throw new ArgumentException("type is error");
        if (_isDecide) return;

        if (_isPush) return;
        _isPush = true;

        //_characterViewController.ResetTransform();
        _audioPlayer.Play(5);

        StartCoroutine(ChangeDecoration(index));
    }

    GameObject CreateModel(uint index, GameObject[] prefabs, Transform parent)
    {
        if (index > prefabs.Length) throw new IndexOutOfRangeException("out of range");

        var child = parent.GetChild(0);
        if (child.gameObject == null) throw new NullReferenceException(parent.name + " is nothing child");
        Destroy(child.gameObject);

        var model = Instantiate(prefabs[index]);
        model.name = prefabs[index].name;
        model.transform.SetParent(parent, false);
        model.transform.localRotation = Quaternion.identity;

        return model;
    }

    void ChangeSelect(int index, int type, Transform panel)
    {
        if (index == type) return;
        Destroy(panel.GetChild(type).GetChild(0).gameObject);
        var circle = Instantiate(Resources.Load<GameObject>("MakeOfCharacter/TypeSelect"));
        circle.transform.SetParent(panel.GetChild(index), false);
    }

    void Start()
    {
        _descriptionSprites.AddRange
        (
            Resources.LoadAll<Sprite>("MakeOfCharacter/Texture/Description")
        );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Beast/Texture")
            );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Human/Texture")
            );

        _characterTextures.AddRange
            (
                Resources.LoadAll<Texture>("Character/Robo/Texture")
            );

        _character = _characterPlace.transform.GetChild(0).gameObject;

        _descriptionType.sprite = _descriptionSprites[OFFSET_INDEX_TYPE];
        _descriptionCostume.sprite = _descriptionSprites[OFFSET_INDEX_COSTUME];
        _descriptionDecoration.sprite = _descriptionSprites[OFFSET_INDEX_DECORATION];

        _parameterBar = FindObjectOfType<ParameterBar>();
        _characterParameterInfo = FindObjectOfType<CharacterParameterInfo>();

        StartCoroutine(DecideCorutine());
        _characterParamter.modelType = CharacterParameter.ModelType.BEAST;
        _characterParamter.costumeType = CharacterParameter.CostumeType.A;
        _characterParamter.decorationType = CharacterParameter.DecorationType.NONE;

        _audioPlayer = FindObjectOfType<AudioPlayer>();

        _characterViewController = FindObjectOfType<CharacterViewController>();

        _audioPlayer.Play(1, 1.0f, true);
    }

    IEnumerator DecideCorutine()
    {

        yield return new WaitForEndOfFrame();

        _characterParamter.attack = 0;
        _characterParamter.defense = 0;
        _characterParamter.speed = 0;
        foreach (var parameter in FindObjectsOfType<ModelParameterInfo>())
        {
            _characterParamter.attack += parameter.getModelParameter.attack;
            _characterParamter.defense += parameter.getModelParameter.defence;
            _characterParamter.speed += parameter.getModelParameter.speed;
        }
        _characterParameterInfo.Decide();
        _parameterBar.ChangeParameterGauge();

        _isPush = false;

        yield return null;
    }

    IEnumerator Transition()
    {
        yield return new WaitForEndOfFrame();

        // 遷移処理

        ScreenSequencer.instance.SequenceStart
            (
                () => { GameScene.Printer.ChangeScene(); },
                new Fade(1.0f)
            );
    }

    IEnumerator ChangeType(int index)
    {
        ChangeSelect(index, (int)getCharacterParamter.modelType, _panelOfChangeType);

        _character = CreateModel((uint)index, _typePrefabs, _character.transform.parent);
        StartCoroutine(ChangeCostume((int)getCharacterParamter.costumeType));

        _characterParamter.modelType = (CharacterParameter.ModelType)index;

        StartCoroutine(ChangeDecoration((int)_characterParamter.decorationType));

        _descriptionType.sprite = _descriptionSprites[index + OFFSET_INDEX_TYPE];

        _character.transform.GetChild(0).GetComponentInChildren<CharacterAppearance>().enabled =
            false;

        yield return StartCoroutine(DecideCorutine());
    }

    IEnumerator ChangeCostume(int index)
    {
        ChangeSelect(index, (int)getCharacterParamter.costumeType, _panelOfChangeCostume);

        var place = _character.transform.GetChild(0);
        CreateModel((uint)index, _costumePrefabs, place);

        _characterParamter.costumeType = (CharacterParameter.CostumeType)index;
        _descriptionCostume.sprite = _descriptionSprites[index + OFFSET_INDEX_COSTUME];
        yield return StartCoroutine(DecideCorutine());
    }

    IEnumerator ChangeDecoration(int index)
    {
        ChangeSelect(index, (int)getCharacterParamter.decorationType, _panelOfChangeDecoration);

        _characterParamter.decorationType = (CharacterParameter.DecorationType)index;
        var meshRenderer = _character.GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = _characterTextures[index + (int)_characterParamter.modelType * 4];
        _descriptionDecoration.sprite = _descriptionSprites[index + OFFSET_INDEX_DECORATION];
        yield return StartCoroutine(DecideCorutine());
    }
}
