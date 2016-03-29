using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    AudioPlayer _player = null;

    [SerializeField]
    GameObject _character = null;

    [SerializeField, Range(-50f, 50f)]
    float _stopPos;

    [SerializeField]
    bool _canJump;

    [SerializeField]
    bool _canSpin;

    //何回目でこかすか
    [SerializeField]
    int _totalKickCount;
    //飛ぶ威力
    [SerializeField, Range(1f, 5f)]
    int _jumpPower;

    //重力
    [SerializeField, Range(0f, 1f)]
    float _gravity;

    //待っている時間
    [SerializeField]
    float _waitTime;

    //こけるSpeed
    [SerializeField, Range(-20f, 20f)]
    int _spinSpeed;
    [SerializeField]
    Quaternion _setRotation;

    public struct CharacterStatus
    {
        //最大待ち時間
        public float _totalWaitTime;
        //飛んでいるかどうか
        public bool _isJump;
        //ジャンプしている時間
        public float _jumpCount;
        //_ジャンプ開始位置
        public float _tempPosY;
        //こけているかどうか
        public bool _isSpin;
        public bool _canSpin;
        //こけている秒数
        public float _fallCount;
        //こけている最大時間
        public float _totalFallCount;
    }

    CharacterStatus _characterStatus;

    /*
    ////////////////開幕落ちてくる処理///////////////////////
    */

    //落ちる処理が終わってるかどうか
    private bool _isEndFalled = false;


    //ちょっとバウンドする。
    private bool _isDrop = false;

    void Start()
    {
        _characterStatus._totalWaitTime = 0.0f;
        _characterStatus._isJump = false;
        _characterStatus._jumpCount = 0;
        _characterStatus._tempPosY = _character.transform.localPosition.y;
        _characterStatus._isSpin = false;
        _characterStatus._canSpin = true;
        _characterStatus._fallCount = 0.0f;
        _characterStatus._totalFallCount = 0.5f;
        _player.Play(10, 1.0f, false);
    }


    void FixedUpdate()
    {

        UpdateOfCharacterFall();
        UpdateOfCharacterDrop();
        if (_isDrop == false) return;
        UpdateOfCharacterSetIsJump();
        UpdateofCharacterJump();
        UpdateOfCharacterSpin();

        if (TouchController.IsTouchBegan())
        {
            PushHit();
        }
    }

    private void UpdateOfCharacterFall()
    {
        if (_isEndFalled == true) return;

        _characterStatus._jumpCount += Time.deltaTime;
        _character.transform.Translate(0,
                                       -_gravity * _characterStatus._jumpCount * _characterStatus._jumpCount * 360,
                                       0);

        if (_character.transform.localPosition.y >= _stopPos) return;

        _player.Play(11, 1.0f, false);
        _isEndFalled = true;
        _character.transform.localPosition
        = new Vector3(_character.transform.localPosition.x,
                      _stopPos,
                      _character.transform.localPosition.z);
        _characterStatus._jumpCount = 0;

    }


    private void UpdateOfCharacterDrop()
    {
        if (_isDrop == false && _isEndFalled == true)
        {
            _characterStatus._jumpCount += Time.deltaTime;

            _character.transform.Translate(0,
                                           +_jumpPower - _gravity * _characterStatus._jumpCount * _characterStatus._jumpCount * 360,
                                           0);

            if (_character.transform.localPosition.y >= _stopPos) return;

            _isDrop = true;
            _character.transform.localPosition
            = new Vector3(_character.transform.localPosition.x,
                          _stopPos,
                          _character.transform.localPosition.z);
            _characterStatus._jumpCount = 0.0f;

        }
    }

    private void UpdateOfCharacterSetIsJump()
    {
        if (_characterStatus._isJump == false && _canJump == true)
            _waitTime += Time.deltaTime;

        if (_waitTime < _characterStatus._totalWaitTime) return;

        //Randomで飛ぶタイミングを変更
        _characterStatus._totalWaitTime = UnityEngine.Random.Range(0.5f, 2.0f);
        _characterStatus._isJump = true;
        _waitTime = 0.0f;
        _player.Play(13, 1.0f, false);

    }

    private void UpdateofCharacterJump()
    {
        if (_characterStatus._isJump != true) return;

        _characterStatus._jumpCount += Time.deltaTime;

        _character.transform.localPosition
            = new Vector3(_character.transform.localPosition.x,
                          _character.transform.localPosition.y
                        + _jumpPower - _gravity * _characterStatus._jumpCount * _characterStatus._jumpCount * 360,
                          _character.transform.localPosition.z);

        //こけていないときに回転させる
        if (_characterStatus._isSpin == false)
        {
            _character.transform.Rotate(new Vector3(0, _spinSpeed * 2, 0));
        }

        //初期位置より下にいったら元の位置に戻し、ジャンプ処理の終了
        if (_character.transform.localPosition.y >= _stopPos) return;

        _characterStatus._isJump = false;
        _character.transform.localPosition
        = new Vector3(_character.transform.localPosition.x,
                      _stopPos,
                      _character.transform.localPosition.z);

        _character.transform.localRotation = new Quaternion(_setRotation.x, _setRotation.y, _setRotation.z, 1);

        _characterStatus._jumpCount = 0.0f;
        _player.Play(11, 1.0f, false);


    }

    private void UpdateOfCharacterSpin()
    {
        if (_characterStatus._isSpin != true) return;

        _characterStatus._fallCount += Time.deltaTime;
        _characterStatus._isJump = true;
        //Rotateをいじりこかす
        _character.transform.Rotate(new Vector3(0, 0, _spinSpeed * _characterStatus._fallCount));
        if (_characterStatus._fallCount <= 0.5f) return;

        //起こすためにSpeedを-1
        _spinSpeed *= -1;
        _characterStatus._canSpin = false;
        _characterStatus._isSpin = false;
        _characterStatus._fallCount = 0.0f;

    }

    void PushHit()
    {
        var hitObject = new RaycastHit();
        var isHit = TouchController.IsRaycastHit(out hitObject);

        if (!isHit) return;

        if (hitObject.transform.name == _character.name
            && _characterStatus._isSpin == false && _characterStatus._isJump == false)
        {
            _characterStatus._isSpin = true;
            _characterStatus._isJump = true;
        }
    }
}
