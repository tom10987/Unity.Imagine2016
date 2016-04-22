using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyGage : MonoBehaviour
{

    //[SerializeField]
    ChargePlayer[] _player;
    

    //[SerializeField]
   ARDeviceManager _aRDeviceManager;

    [SerializeField]
    Gage _gage;

    [SerializeField]
    Image _powerGage;

    [SerializeField]
    Image _backgroundGage;

    

    [SerializeField]
    private Player _selectPlayer;

    [SerializeField]
    Round _round;

    int _player2Gage = 1;
    public enum Player
    {
        Player1,
        Player2
    }

    float _oneUpGage = 0;

    public Player getSelectPlayer { get { return _selectPlayer; } }

    [SerializeField]
    float _speed = 1;

    Vector2 _size = new Vector2(0, 0);

    Vector2 _gagePosition = new Vector2(0, 0);

    bool _isPowerGage = false;

    public bool _getIsPowerGage { get { return _isPowerGage; } }

    Vector3 _initialPosition = new Vector3(0,0,0);

    void Start()
    {
        _aRDeviceManager = FindObjectOfType<ARDeviceManager>();
        _player = new ChargePlayer[2];
        if (_selectPlayer == Player.Player2)
        {
            _player2Gage = -1;
        }
        _size = _powerGage.rectTransform.sizeDelta;
        _oneUpGage = _backgroundGage.rectTransform.sizeDelta.x / _round.getRoundCount / _gage.getRangeGageCount;

        _initialPosition = _backgroundGage.rectTransform.localPosition;
        //Debug.Log(_initialPosition);
        _initialPosition.x -= _backgroundGage.rectTransform.sizeDelta.x / 2 * _player2Gage;
        _initialPosition.y = _powerGage.rectTransform.localPosition.y;
        _initialPosition.z = _powerGage.rectTransform.localPosition.z;
        _powerGage.rectTransform.localPosition = _initialPosition;
    }

    void Update()
    { }


    public void Init()
    {
        _initialPosition = _backgroundGage.rectTransform.localPosition;
        _initialPosition.x -= _backgroundGage.rectTransform.sizeDelta.x / 2 * _player2Gage;
        _initialPosition.y = _powerGage.rectTransform.localPosition.y;
        _initialPosition.z = _powerGage.rectTransform.localPosition.z;
        Vector2 size = _powerGage.rectTransform.sizeDelta;
        size.x = 0;
		_size.x = 0;
        _powerGage.rectTransform.sizeDelta = size;
        _powerGage.rectTransform.localPosition = _initialPosition;
        foreach(var player in _player)
        {
            player._totalScorePlayer1 = 0;
            player._totalScorePlayer2 = 0;
        }
    }

    public bool ChargePowerGage()
    {
        
        _player[0] = _aRDeviceManager.player1.gameObject.GetComponentInChildren<ChargePlayer>();
        _player[1] = _aRDeviceManager.player2.gameObject.GetComponentInChildren<ChargePlayer>();

        if (_player[0].PressOnce1 == false && _selectPlayer == Player.Player1) return _isPowerGage = false;

        if (_player[1].PressOnce2 == false && _selectPlayer == Player.Player2) return _isPowerGage = false;

        float upGage = 0;
        if (_selectPlayer == Player.Player1)
        {
            upGage = _oneUpGage * _player[0].getTotalScorePlayer1;
        }else
        if (_selectPlayer == Player.Player2)
        {
            upGage = _oneUpGage * _player[1].getTotalScorePlayer2;
        }

        if (_powerGage.rectTransform.sizeDelta.x >=
    _backgroundGage.rectTransform.sizeDelta.x-2)
            return _isPowerGage = true;

        if (upGage > _size.x)
        {

            _size.x += _speed;
            _powerGage.rectTransform.sizeDelta = _size;
            _gagePosition = _powerGage.rectTransform.localPosition;
            _gagePosition.x += (_speed / 2) * _player2Gage;
            _powerGage.rectTransform.localPosition = _gagePosition;
            return _isPowerGage = false;
        }
        else
        if (upGage <= _size.x)
        {
            return _isPowerGage = true;
        }

        return _isPowerGage = false;
    }
}
