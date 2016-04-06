using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;

public class MenuDirecter : MonoBehaviour
{
    [SerializeField]
    AudioPlayer _player = null;

    [SerializeField]
    Camera _camera = null;

    public List<Action> _ListsOfActionPushButton = new List<Action>();

    //今どのゲームを選択しているか
    private int _nowSelectGameNum = 0;

    //現在のカメラRotation
    private float _nowCameraRotation = 0;

    //Gameを選べるかどうか
    private bool _canSelectMode = true;

    //Characterの移動アニメーションをしていいかどうか
    private bool _canMoveCharacter = false;


    //AnimationのCharacter
    [SerializeField]
    GameObject[] _characterAnimation = null;
    //AnimationのCharacterの停止位置
    [SerializeField]
    Transform[] _animationStop = null;
    //AnimationのCharacterの開始位置
    private Vector3[] _startPosition = new Vector3[2];

    private float _animationCount = 0.0f;
    private Vector3[] _def = new Vector3[2];
    private Vector3 _defAngle;

    [SerializeField]
    GameObject _animation = null;

    [SerializeField]
    GameObject _cunon = null;
    private double _totalReverseAnimationCount = 0.0f;
    private double _reverseAnimationCount = 0.0f;
    private float _waitAnimationCount = 3.0f;
    private bool _isChangedAnimationActive = false;

    private float _waitPlayAnimationAudioCount = 0.0f;
    private bool _isWaitPlayAnimationAudio = false;

    private bool _isBackCamera = false;

    [SerializeField]
    GameObject _statusCursor = null;

    MenuBoxAnimater _menuBoxAnimater = null;

    enum NowCameraMode
    {
        NONE,
        UP_ANGLE,
        DOWN_ANGLE
    }

    private NowCameraMode _nowCameraMode = NowCameraMode.NONE;

    [SerializeField]
    Image _selectGameName = null;

    private List<Sprite> _gameNames = new List<Sprite>();

    private bool _isChangeScene = false;

    //Sceneが選ばれたかどうか
    //True-> ボタン反応させない　false->ボタン反応していい
    private bool _isEndedChoiseScene = false;

    private bool _isSelectGameRandom = false;
    private int _randomCount = 0;
    private int _tempSelectGameNum = 0;
    void Start()
    {

        //_menuBoxAnimater = FindObjectOfType<MenuBoxAnimater>();
        //_menuBoxAnimater.gameObject.SetActive(false);

        Register();

        ChangeStatusCursor(_nowSelectGameNum);

        for (int i = 0; i < 2; ++i)
        {
            _startPosition[i] = new Vector3(_characterAnimation[i].transform.localPosition.x,
                                            _characterAnimation[i].transform.localPosition.y,
                                            _characterAnimation[i].transform.localPosition.z);

            _def[i] = new Vector3(_animationStop[i].transform.localPosition.x - _startPosition[i].x,
                                  _animationStop[i].transform.localPosition.y - _startPosition[i].y,
                                  _animationStop[i].transform.localPosition.z - _startPosition[i].z);
        }

        _defAngle = _animationStop[1].transform.localEulerAngles;

        _player.Play(0, 1.0f, true);
    }

    private void Register()
    {
        _player.manageMode = AudioPlayer.SourceManageMode.Additive;


        //Titleに移動 0
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            _isEndedChoiseScene = true;
            _player.Play(7, 1.0f, false);

            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { GameScene.Title.ChangeScene(); },
                    new Fade(1.0f)
                );
        });

        //Createに移動 1
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            _isEndedChoiseScene = true;
            _player.Play(6, 1.0f, false);

            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { GameScene.Create.ChangeScene(); },
                    new Fade(1.0f)
                );
        });

        //GameSelectに移動 2
        _ListsOfActionPushButton.Add(() =>
        {
            if (_nowCameraMode != NowCameraMode.NONE) return;
            _canMoveCharacter = true;
            _canSelectMode = false;
            _nowCameraMode = NowCameraMode.DOWN_ANGLE;
            _animationCount = 0.0f;
            FindObjectOfType<ChangeText>().ChangeExplanationText(0);
            _player.Play(6, 1.0f, false);
        });

        // 3
        _ListsOfActionPushButton.Add(() =>
        {

            //逆再生
            //GameSelectから２つのモード選択に
            if (_isEndedChoiseScene == true) return;
            if (_reverseAnimationCount != 0.0f) return;
            _isBackCamera = true;
            _nowCameraMode = NowCameraMode.UP_ANGLE;
            _animationCount = 1.0f;
            _reverseAnimationCount = FindObjectOfType<MenuBoxAnimater>().animationTime;
            _totalReverseAnimationCount = _reverseAnimationCount;
            FindObjectOfType<MenuBoxAnimater>().isBack = true;
            FindObjectOfType<MenuBoxAnimater>().animationSpeed = -1.0f;

            _player.Play(7, 1.0f, false);
            _isWaitPlayAnimationAudio = true;
            Debug.Log("Modosu");
            _isChangedAnimationActive = false;
        });

        // 4
        _ListsOfActionPushButton.Add(() =>
        {
            //GameSelectの
            ////選択のはい
            if (_isEndedChoiseScene == true) return;
            if (_isBackCamera) return;
            _player.Play(6, 1.0f, false);
            _isEndedChoiseScene = true;
            _cunon.SetActive(true);
            FindObjectOfType<ActionOfCunon>().isStart = true;
        });


        //関係のない場所を触ったら 5
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            FindObjectOfType<ChangeText>().ChangeExplanationText(0);
        });

        //Characterを触ったとき 6
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            if (_canMoveCharacter == false && _canSelectMode == false)
            {
                _player.Play(8, 1.0f, false);

                FindObjectOfType<ChangeText>().ChangeExplanationText(6);
            }
        });



        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title"));
        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title1"));
        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title2"));
        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title3"));

        _selectGameName.sprite = _gameNames[1];
    }

    void Update()
    {
        if (_nowCameraMode == NowCameraMode.DOWN_ANGLE)
        {
            StartCoroutine(StartDirection());
            StartCoroutine(ChangeStartCameraAngle());

            //Test
            if (TouchController.IsTouchBegan())
                SkipStartDirection();

        }

        else if (_nowCameraMode == NowCameraMode.UP_ANGLE)
        {
            StartCoroutine(EndDirection());
            StartCoroutine(ChangeEndCameraAngle());

            if (TouchController.IsTouchBegan())
                SkipEndDirection();
        }

        if (TouchController.IsTouchBegan() && _canMoveCharacter == false)
        {
            if (_isEndedChoiseScene == true) return;
            SelectGameMode();
            SelectGameType();
        }


        if (_isWaitPlayAnimationAudio == true)
        {
            _waitPlayAnimationAudioCount += Time.deltaTime;
            if (_waitPlayAnimationAudioCount > 1.3f)
            {
                _player.Play(12, 1.0f, false);
                _isWaitPlayAnimationAudio = false;
                _waitPlayAnimationAudioCount = 0.0f;
            }
        }

        ChangeMiniGame();
    }

    void FixedUpdate()
    {
        SelectGameRandom();
    }

    //Scene選択
    private void SelectGameMode()
    {
        if (!_canSelectMode) return;

        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        if (!isHit) return;
        Debug.Log(hitObject.transform.name);
        if (hitObject.transform.name == "BackTitle")
        {
            _ListsOfActionPushButton[0]();
            Debug.Log("AAAAAA");
        }
        else if (hitObject.transform.name == "Tukuru")
            _ListsOfActionPushButton[1]();
        else if (hitObject.transform.name == "Asobu")
            _ListsOfActionPushButton[2]();
    }

    //Game選択
    private void SelectGameType()
    {
        if (_canSelectMode) return;

        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        if (!isHit)
        {
            _ListsOfActionPushButton[5]();
            return;
        }

        if (hitObject.transform.name == "BackSelectMode")
            _ListsOfActionPushButton[3]();
        else if (hitObject.transform.name == "Asobu")
        {
            if (_nowCameraMode != NowCameraMode.NONE) return;
            _ListsOfActionPushButton[6]();
        }
        else if (hitObject.transform.name == "OkButton")
            _ListsOfActionPushButton[4]();
        else if (hitObject.transform.name == "RandomButton")
        {
            FindObjectOfType<ChangeText>().ChangeExplanationText(4);
            SelectOfGameNum(3);
        }
        else if (hitObject.transform.name == "SpeedGameButton")
            SelectOfGameNum(0);
        else if (hitObject.transform.name == "ShotGameButton")
            SelectOfGameNum(1);
        else if (hitObject.transform.name == "DeffenceGameButton")
        {
            SelectOfGameNum(2);
            Debug.Log("Hit");
        }
        else if (hitObject.transform.name == "StatusButton")
            FindObjectOfType<ChangeText>().ChangeExplanationText(5);

    }


    IEnumerator ChangeStartCameraAngle()
    {
        while (_nowCameraRotation < 90.0f)
        {
            _nowCameraRotation += Time.deltaTime * 30;

            _camera.transform.localRotation =
                 Quaternion.Euler(_nowCameraRotation, 0, 0);

            yield return null;
        }

    }

    IEnumerator ChangeEndCameraAngle()
    {
        if (_totalReverseAnimationCount > _reverseAnimationCount + _waitAnimationCount)
        {
            while (_nowCameraRotation > 0.0f)
            {
                _nowCameraRotation -= Time.deltaTime * 30;

                _camera.transform.localRotation =
                     Quaternion.Euler(_nowCameraRotation, 0, 0);
                yield return null;
            }
        }
    }

    IEnumerator StartDirection()
    {
        // 遊びオブジェクトの移動処理
        _animationCount += Time.deltaTime;
        while (_animationCount < 1.0f)
        {
            for (int i = 0; i < 2; ++i)
            {
                _characterAnimation[i].transform.localPosition
                       = new Vector3(_startPosition[i].x + _def[i].x * _animationCount,
                                     _startPosition[i].y + _def[i].y * _animationCount,
                                     _startPosition[i].z + _def[i].z * _animationCount);
            }

            _characterAnimation[1].transform.localRotation
                = Quaternion.Euler(_defAngle.x * _animationCount,
                                   _defAngle.y * _animationCount,
                                   _defAngle.z * _animationCount);

            yield return null;
        }

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                    = new Vector3(_animationStop[i].transform.localPosition.x,
                                  _animationStop[i].transform.localPosition.y,
                                  _animationStop[i].transform.localPosition.z);
        }

        if (_canMoveCharacter == true)
        {
            _characterAnimation[1].transform.localRotation = Quaternion.Euler(60, 105, 110);

            _nowCameraMode = NowCameraMode.NONE;
            _animation.SetActive(true);
            FindObjectOfType<MenuBoxAnimater>().isPlay = true;
            FindObjectOfType<MenuBoxAnimater>().animationSpeed = 1.0f;
            _player.Play(12, 1.0f, false);
            _characterAnimation[0].SetActive(false);
            _canMoveCharacter = false;
        }
        yield return null;
    }

    IEnumerator EndDirection()
    {

        _totalReverseAnimationCount += Time.deltaTime;

        if (_totalReverseAnimationCount > _reverseAnimationCount + _waitAnimationCount)
        {
            if (_isChangedAnimationActive == false)
            {
                _animation.SetActive(false);
                _characterAnimation[0].SetActive(true);
                _isChangedAnimationActive = true;
            }

            // 遊びオブジェクトの移動処理
            _animationCount -= Time.deltaTime;

            while (_animationCount > 0.0f)
            {
                for (int i = 0; i < 2; ++i)
                {
                    _characterAnimation[i].transform.localPosition
                           = new Vector3(_startPosition[i].x + _def[i].x * _animationCount,
                                         _startPosition[i].y + _def[i].y * _animationCount,
                                         _startPosition[i].z + _def[i].z * _animationCount);
                }

                _characterAnimation[1].transform.localRotation
                              = Quaternion.Euler(_defAngle.x * _animationCount,
                                                 _defAngle.y * _animationCount,
                                                 _defAngle.z * _animationCount);

                yield return null;
            }

            for (int i = 0; i < 2; ++i)
            {
                _characterAnimation[i].transform.localPosition
                    = new Vector3(_startPosition[i].x,
                                  _startPosition[i].y,
                                  _startPosition[i].z);
            }

            //Test
            _characterAnimation[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
            _nowCameraMode = NowCameraMode.NONE;
            _canMoveCharacter = false;
            _canSelectMode = true;
            _reverseAnimationCount = 0.0f;
            _isBackCamera = false;
            yield return null;
        }
    }

    private void SkipStartDirection()
    {
        _animation.SetActive(true);
        //_animation.GetComponent<MenuBoxAnimater>().Stop();
        _animation.GetComponent<MenuBoxAnimater>().Play("TransisionState", 1.0f);
        _animationCount = 1.0f;

        StopAllCoroutines();

        _camera.transform.localRotation =
                 Quaternion.Euler(90.0f, 0, 0);

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                        = _animationStop[i].transform.localPosition;
        }

        if (_canMoveCharacter == true)
        {
            _characterAnimation[1].transform.localRotation = Quaternion.Euler(60, 105, 110);

            _nowCameraMode = NowCameraMode.NONE;
            _animation.SetActive(true);
            _characterAnimation[0].SetActive(false);
            _canMoveCharacter = false;
            _nowCameraRotation = 90.0f;
        }
    }

    private void SkipEndDirection()
    {
        //if(_animation.activeInHierarchy == true)
        //FindObjectOfType<MenuBoxAnimater>().Play("Back",1.0f);
        _animation.GetComponent<MenuBoxAnimater>().Stop();
        _animation.SetActive(false);
        _animationCount = 0.0f;

        StopAllCoroutines();

        _camera.transform.localRotation =
                 Quaternion.Euler(0.0f, 0, 0);

        for (int i = 0; i < 2; ++i)
        {
            _characterAnimation[i].transform.localPosition
                        = _startPosition[i];
        }

        _characterAnimation[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
        _nowCameraMode = NowCameraMode.NONE;
        _canMoveCharacter = false;
        _canSelectMode = true;
        _reverseAnimationCount = 0.0f;
        _nowCameraRotation = 0.0f;
        _isBackCamera = false;
        _characterAnimation[0].SetActive(true);
    }



    public void ActionOfPushButton(int _buttonNum)
    {
        if (_isEndedChoiseScene == true) return;
        if (_canMoveCharacter == false)
            _ListsOfActionPushButton[_buttonNum]();
    }

    public void SelectOfGameNum(int nowSelectGameNum_)
    {
        if (_isEndedChoiseScene == true) return;
        if (nowSelectGameNum_ >= 0 && nowSelectGameNum_ <= 2 && _canMoveCharacter == false)
        {
            _nowSelectGameNum = nowSelectGameNum_;
            FindObjectOfType<ChangeText>().ChangeExplanationText(1 + _nowSelectGameNum);
            //if (nowSelectGameNum_ != 0) return;
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
            ChangeStatusCursor(_nowSelectGameNum);
            _selectGameName.sprite = _gameNames[_nowSelectGameNum + 1];
        }
        else if (nowSelectGameNum_ == 3 && _canMoveCharacter == false)
        {
            _isSelectGameRandom = true;
            _isEndedChoiseScene = true;
            _nowSelectGameNum = 0;
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            //FindObjectOfType<ChangeTarget>().ChangeTargetCursor(3);
            FindObjectOfType<ChangeText>().ChangeExplanationText(4);
            ChangeStatusCursor(_nowSelectGameNum);
        }

        _player.Play(8, 1.0f, false);
    }

    private void SelectGameRandom()
    {
        if (!_isSelectGameRandom) return;
        ++_randomCount;
        if (_randomCount % 15 != 0) return;
        RandomSelect();
        FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
        FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
        ChangeStatusCursor(_nowSelectGameNum);
        _player.Play(6, 1.0f, false);
        _tempSelectGameNum = _nowSelectGameNum;

        if (_randomCount < 151) return;
        FindObjectOfType<ChangeText>().ChangeExplanationText(7);

        if (_randomCount < 160) return;
        _player.Play(8, 1.0f, false);
        FindObjectOfType<ChangeText>().ChangeExplanationText(1 + _nowSelectGameNum);
        _selectGameName.sprite = _gameNames[_nowSelectGameNum + 1];
        _isSelectGameRandom = false;
        _isEndedChoiseScene = false;
        _randomCount = 0;
    }

    private void RandomSelect()
    {
        _nowSelectGameNum = UnityEngine.Random.Range(0, 3);
        if (_tempSelectGameNum != _nowSelectGameNum) return;
        RandomSelect();

    }

    private void ChangeStatusCursor(int _selectGameNum)
    {
        _statusCursor.transform.localRotation = Quaternion.Euler(0, 0, -120 * (_nowSelectGameNum -1));
    }

    private void ChangeMiniGame()
    {
        if (_isEndedChoiseScene == true) return;
        if (_cunon.activeInHierarchy == false) return;
        if (FindObjectOfType<ActionOfCunon>().isEnd == true && _isChangeScene == false)
        {
            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { GameScene.Game.ChangeScene(); },
                    new Fade(1.0f)
                );
        }

        if (FindObjectOfType<ActionOfCunon>().isEnd == true && _isChangeScene == false)
        {
            _isChangeScene = true;
            _isEndedChoiseScene = true;
        }
    }
}

