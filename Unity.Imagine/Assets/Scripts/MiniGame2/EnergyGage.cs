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
    float _cross;

    [SerializeField]
    private Player _selectPlayer;

    int _player2Gage = 1;
    public enum Player
    {
        Player1,
        Player2
    }

    public Player getSelectPlayer { get { return _selectPlayer; } }

    [SerializeField]
    float _speed = 1;

    Vector2 _size = new Vector2(0, 0);

    Vector2 _gagePosition = new Vector2(0, 0);

    bool _isPowerGage = false;

    public bool _getIsPowerGage { get { return _isPowerGage; } }

    void Start()
    {
        if (_selectPlayer == Player.Player2)
        {
            _player2Gage = -1;
        }
        _size = _powerGage.rectTransform.sizeDelta;
    }

    void Update() { }


    public bool ChargePowerGage()
    {
        float oneRoundUpGage = (_player._totalScore - _gage.getOneRoundScore) * _cross;

        if (_gage.getOneRoundScore * _cross >= _size.x - oneRoundUpGage)
        {
            if (_powerGage.rectTransform.sizeDelta.x >=
                _backgroundGage.rectTransform.sizeDelta.x)
                return _isPowerGage = false;
            _size.x += _speed;
            _powerGage.rectTransform.sizeDelta = _size;
            _gagePosition = _powerGage.rectTransform.anchoredPosition;
            _gagePosition.x += _speed / 2 * _player2Gage;
            _powerGage.rectTransform.anchoredPosition = _gagePosition;
            return _isPowerGage = false;
        }
        else
        if (_gage.getOneRoundScore * _cross < _size.x - oneRoundUpGage)
        {
            return _isPowerGage = true;
        }

        return _isPowerGage = false;
    }
}
