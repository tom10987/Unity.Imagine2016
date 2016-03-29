using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SuddenDeath : MonoBehaviour {
    ScoreCompare _scoreCompare;
    TimeCount _timeCount;

    [SerializeField]
    AudioPlayer _audioPlayer;

    [SerializeField]
    float _drawTime = 4.0f;

    float _time;

    bool _isSe = false;

    [SerializeField, TooltipAttribute("表示する順番にImageを入れてください")]
    Image[] _startCountImage = null;

    bool _countFinish = false;
    public bool getCountFinish { get { return _countFinish; } set { _countFinish = value; } }

    void Start()
    {
        _timeCount = FindObjectOfType<TimeCount>();
        _scoreCompare = FindObjectOfType<ScoreCompare>();
        //_regularInterval = _drawTime / _startCountImage.Length;
        _time = _drawTime;
        foreach (var image in _startCountImage)
        {
            image.enabled = false;
        }

    }

    void Update()
    {
        Renovation();

    }

    public void Renovation()
    {
        if (_countFinish)
        {
            _scoreCompare.getIsDraw = false;
        }

        if (_scoreCompare.getIsDraw == true)
        {
            CountDrawImage();
            CountDown();
        }
    }

    void CountDown()
    {
        if (_time <= 0) return;
        _time -= Time.deltaTime;

    }

    void CountDrawImage()
    {
        if (_countFinish) return;

        if (_time <= _drawTime && _time > 4)
        {
            _startCountImage[0].enabled = true;
        }
        else
                if (_time <= 4 && _time > 3)
        {
            if(_isSe == false)
            {
                _audioPlayer.Play(14, false);
                _isSe = true;
            }
            _startCountImage[0].enabled = false;
            _startCountImage[1].enabled = true;
        }
        else
        if (_time <= 3 && _time > 2)
        {
            _startCountImage[1].enabled = false;
            _startCountImage[2].enabled = true;
        }
        else
        if (_time < 2 && _time > 1)
        {
            _startCountImage[2].enabled = false;
            _startCountImage[3].enabled = true;
        }
        else
        if (_time <= 1 && _time > 0)
        {
            _startCountImage[3].enabled = false;
            _startCountImage[4].enabled = true;
        }
        else
        if( _time <0)
        {
            _startCountImage[4].enabled = false;
            _time = _drawTime;
            _countFinish = true;
            _isSe = false;
            _timeCount.time = 6;
            
        }


    }
}
