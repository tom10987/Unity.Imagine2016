using UnityEngine;
using System.Collections;

public class GameMnueTab : MonoBehaviour {

  [SerializeField]
  GameManager _manager = null;

    [SerializeField]
    ReadyKey _readyKey = null;

    [SerializeField]
    GameObject _modelsParent;
    ActionManager[] _models;

    [SerializeField]
    RectTransform _start;
    [SerializeField]
    RectTransform _howToPlay;
    [SerializeField]
    RectTransform _backMnue;

    public enum TabPattern
    {
        ARLoad,
        Ready,
        Play,
        Result,
    }

    [SerializeField]
    TabPattern _tabPattern = TabPattern.ARLoad;

    [SerializeField]
    float _speed = 10.0f;

    Vector3[] _startPos = new Vector3[4];
    Vector3[] _howToPlayPos = new Vector3[4];
    Vector3[] _backMnuePos = new Vector3[4];

    GameObject howToPlayImage { get; set; }

    bool _isBackMnue = false;
    bool _isPushPlayButton = false;
    public bool isPushPlayButton { get { return _isPushPlayButton; } }

    // Use this for initialization
    void Start () {
        _models = _modelsParent.GetComponentsInChildren<ActionManager>();
	}
	
	// Update is called once per frame
	void Update () {
        ModelCheck();
        SizeAndPosSet();
        Move();
    }

    void ModelCheck()
    {
        if (_tabPattern == TabPattern.ARLoad || _tabPattern == TabPattern.Ready)
        {
            if (_manager.isStart)
            {
                _tabPattern = TabPattern.Ready;
            }
            else
            {
                _tabPattern = TabPattern.ARLoad;
            }
        }
    }

    void SizeAndPosSet()
    {
        float sizeWidth = Screen.width * 1.2f;
        float sizeHeight = Screen.height * 1.2f;

        _start.sizeDelta = new Vector2(sizeWidth, sizeHeight);
        _howToPlay.sizeDelta = new Vector2(sizeWidth, sizeHeight);
        _backMnue.sizeDelta = new Vector2(sizeWidth, sizeHeight);

        //Vector3 offset = new Vector3(-Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
        Vector3 offset = new Vector3(-Screen.width / 2.0f, Screen.height * 1.01f, 0.0f);
        {
            _startPos[0] = new Vector3(Screen.width / 3 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
            _howToPlayPos[0] = new Vector3(Screen.width / 4, -sizeHeight / 2, 0.0f) + offset;
            _backMnuePos[0] = new Vector3(Screen.width / 4 * 3, -sizeHeight / 2, 0.0f) + offset;
        }
        {
            _startPos[1] = new Vector3(Screen.width / 3 - Screen.width / 6, -sizeHeight / 2, 0.0f) + offset;
            _howToPlayPos[1] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 2, 0.0f) + offset;
            _backMnuePos[1] = new Vector3(Screen.width / 3 * 3 - Screen.width / 6, -sizeHeight / 2, 0.0f) + offset;
        }
        {
            _startPos[2] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
            _howToPlayPos[2] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
            _backMnuePos[2] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
        }
        {
            _startPos[3] = new Vector3(Screen.width / 3 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
            _howToPlayPos[3] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 5, 0.0f) + offset;
            _backMnuePos[3] = new Vector3(Screen.width / 3 * 2 - Screen.width / 6, -sizeHeight / 2, 0.0f) + offset;
        }
    }

    void Move()
    {
        _start.anchoredPosition3D -= (_start.anchoredPosition3D - _startPos[(int)_tabPattern]) / _speed;
        _howToPlay.anchoredPosition3D -= (_howToPlay.anchoredPosition3D - _howToPlayPos[(int)_tabPattern]) / _speed;
        _backMnue.anchoredPosition3D -= (_backMnue.anchoredPosition3D - _backMnuePos[(int)_tabPattern]) / _speed;
    }

    public void StartOn()
    {
        if (_isBackMnue) { return; }
        if(howToPlayImage != null) { Destroy(howToPlayImage); }
        _tabPattern = TabPattern.Play;
        //_manager.OnPlay();
        _readyKey.DrawReadyImage();
        _isPushPlayButton = true;
    }

    public void howToPlayOn()
    {
        if(howToPlayImage == null && !_isPushPlayButton && !_isBackMnue)
        {
            howToPlayImage = Instantiate(Resources.Load("MiniGame/HowToPlay/BarrgeHowToPlay") as GameObject);
        }
    }

    public void BackMnueOn()
    {
        if (howToPlayImage == null && !_isPushPlayButton && !_isBackMnue)
        {
            _isBackMnue = true;
            ScreenSequencer.instance.SequenceStart(() => GameScene.Menu.ChangeScene(), new Fade(1f));
        }
    }
}
