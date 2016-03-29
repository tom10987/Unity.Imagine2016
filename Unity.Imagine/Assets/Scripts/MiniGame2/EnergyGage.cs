using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyGage : MonoBehaviour {

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
    int _player2Gage = 1;

    [SerializeField]
    float _speed = 0;

    Vector2 _size = new Vector2(0,0);

    Vector2 _gagePosition = new Vector2(0, 0);

    bool _isPowerGage = false;

    public bool _getIsPowerGage {get { return _isPowerGage; }}

    void Start ()
    {
        //プレイヤーによって左右にゲージのスタート位置を分ける

        //_gagePosition = _backgroundGage.rectTransform.anchoredPosition;
        //_gagePosition.x = (_backgroundGage.rectTransform.anchoredPosition.x + (_backgroundGage.rectTransform.sizeDelta.x / 4));
        //_powerIamge.rectTransform.anchoredPosition = _gagePosition;
        _size = _powerGage.rectTransform.sizeDelta;
        //Debug.Log(_powerIamge.rectTransform.anchoredPosition );
    }
	 
	void Update (){}


    public bool PowerGage()
    {
        //ちょっとゲージがずれる(後で直す)
        if (_gage._getChargeScore * _cross > _powerGage.rectTransform.sizeDelta.x)
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

        if (_gage._getChargeScore != 0&&
            _gage._getChargeScore * _cross <=
            _powerGage.rectTransform.sizeDelta.x)
        {
            Debug.Log("debu");

            if (_player._getIsInit == true)
            {
                Debug.Log("homo");
                return _isPowerGage = false;
            }
            else
            if (_player._getIsInit == false)
            {
                return _isPowerGage = true;
            }
        }

        return _isPowerGage = false;
    }
}
