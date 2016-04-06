using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyGage : MonoBehaviour
{

    [SerializeField]
    ChargePlayer _player;

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
        if (_selectPlayer == Player.Player2)
        {
            _player2Gage = -1;
        }
        _size = _powerGage.rectTransform.sizeDelta;
        _oneUpGage = _backgroundGage.rectTransform.sizeDelta.x / _round.getRoundCount / _gage.getRangeGageCount;

        
        _initialPosition = _backgroundGage.rectTransform.localPosition;
        Debug.Log(_initialPosition);
        _initialPosition.x -= _backgroundGage.rectTransform.sizeDelta.x / 2 * _player2Gage;
        _initialPosition.y = _powerGage.rectTransform.localPosition.y;
        _initialPosition.z = _powerGage.rectTransform.localPosition.z;
        _powerGage.rectTransform.localPosition = _initialPosition;
    }

    void Update() {
        
    }


    public bool ChargePowerGage()
    {

        if (!_player.PressOnce) return _isPowerGage = false;
        float upGage = _oneUpGage * _player._totalScore;
        if (_powerGage.rectTransform.sizeDelta.x >=
    _backgroundGage.rectTransform.sizeDelta.x)
            return _isPowerGage = false;

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
