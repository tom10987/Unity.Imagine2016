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

    private bool _canSelectGame = true;

    //Characterの移動アニメーションをしていいかどうか
    private bool _canMoveCharacter = false;

    [SerializeField]
    GameObject[] _characterAnimation = null;

    [SerializeField]
    Transform[] _animationStop = null;

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

    private bool _isEndedChoiseScene = false;

    void Start()
    {
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

        _ListsOfActionPushButton.Add(() =>
        {
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

        _ListsOfActionPushButton.Add(() =>
        {
            ////選択のはい
            if (_isEndedChoiseScene == true) return;
            if (_isBackCamera) return;
            _player.Play(6, 1.0f, false);
            _cunon.SetActive(true);
            FindObjectOfType<ActionOfCunon>().isStart = true;
        });

        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            _isEndedChoiseScene = true;
            _player.Play(7, 1.0f, false);
            //Titleに移動
            var screenSequencer = ScreenSequencer.instance;

            if (screenSequencer.isEffectPlaying) return;

            screenSequencer.SequenceStart
                (
                    () => { GameScene.Title.ChangeScene(); },
                    new Fade(1.0f)
                );
        });
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            FindObjectOfType<ChangeText>().ChangeExplanationText(5);
        });
        _ListsOfActionPushButton.Add(() =>
        {
            if (_isEndedChoiseScene == true) return;
            if (_canMoveCharacter == false && _canSelectGame == false)
            {
                _player.Play(8, 1.0f, false);

                FindObjectOfType<ChangeText>().ChangeExplanationText(6);
            }
        });



        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title"));
        _gameNames.Add(Resources.Load<Sprite>("Menu/Texture/menu_title1"));

        _selectGameName.sprite = _gameNames[1];
    }

    void Update()
    {
        if (_nowCameraMode == NowCameraMode.DOWN_ANGLE)
        {
            StartCoroutine(StartDirection());
            StartCoroutine(ChangeStartCameraAngle());
        }

        else if (_nowCameraMode == NowCameraMode.UP_ANGLE)
        {
            StartCoroutine(EndDirection());
            StartCoroutine(ChangeEndCameraAngle());
        }

        if (TouchController.IsTouchBegan() && _canMoveCharacter == false && _canSelectGame == true)
        {
            if (_isEndedChoiseScene == true) return;
            TouchCharacter();
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

    private void TouchCharacter()
    {
        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        if (!isHit) return;
        for (int i = 0; i < 2; ++i)
            if (hitObject.transform.name == _characterAnimation[i].name)
            {
                if (_characterAnimation[i].name == "Asobu")
                {
                    _canMoveCharacter = true;
                    _canSelectGame = false;
                    _nowCameraMode = NowCameraMode.DOWN_ANGLE;
                    _animationCount = 0.0f;
                    FindObjectOfType<ChangeText>().ChangeExplanationText(0);
                    _player.Play(6, 1.0f, false);
                }

                else if (_characterAnimation[i].name == "Tukuru")
                {
                    if (_isEndedChoiseScene == true) return;
                    _isEndedChoiseScene = true;
                    _player.Play(6, 1.0f, false);
                    //Createに移動
                    var screenSequencer = ScreenSequencer.instance;

                    if (screenSequencer.isEffectPlaying) return;

                    screenSequencer.SequenceStart
                        (
                            () => { GameScene.Create.ChangeScene(); },
                            new Fade(1.0f)
                        );
                }
            }
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
        while (_animationCount <= 1.0f)
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
            Debug.Log("play");
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
            _canSelectGame = true;
            _reverseAnimationCount = 0.0f;
            _isBackCamera = false;
            yield return null;
        }
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
            if (nowSelectGameNum_ != 0) return;
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            FindObjectOfType<ChangeTarget>().ChangeTargetCursor(_nowSelectGameNum);
            ChangeStatusCursor(_nowSelectGameNum);
        }
        else if (nowSelectGameNum_ == 3 && _canMoveCharacter == false)
        {
            _nowSelectGameNum = 0;
            FindObjectOfType<SelectGameStatus>().SelectGameNum = _nowSelectGameNum;
            //FindObjectOfType<ChangeTarget>().ChangeTargetCursor(3);
            FindObjectOfType<ChangeText>().ChangeExplanationText(4);
            ChangeStatusCursor(_nowSelectGameNum);
        }

        //if (_nowSelectGameNum == 0)
        //    _selectGameName.sprite = _gameNames[1];
        //else
        //    _selectGameName.sprite = _gameNames[0];

        _player.Play(8, 1.0f, false);
    }

    private void ChangeStatusCursor(int _selectGameNum)
    {
        _statusCursor.transform.localRotation = Quaternion.Euler(0, 0, (_selectGameNum * -120) + 240);
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

