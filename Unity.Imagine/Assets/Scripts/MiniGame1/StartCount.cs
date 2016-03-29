using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCount : MonoBehaviour {

    [SerializeField]
    AudioPlayer _audioPlayer;

    [SerializeField]
    KeyAction _gameManager = null;

    [SerializeField]
    float _drawTime = 4.0f;

    float _time;

    [SerializeField, TooltipAttribute("表示する順番にImageを入れてください")]
    Image[] _startCountImage = null;
  
    bool _isStartImageDraw = false;
    public bool countFinish { get; set; }

    void Start ()
    {
    countFinish = false;
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _time = _drawTime;
        foreach (var image in _startCountImage)
        {
            image.enabled = false;
        }

        if (_gameManager == null) { Debug.Log("_gameManager が null です。KeyAction スクリプトが入ってるオブジェクトをいれてください。"); }
    }
	
	void Update ()
    {
        Renovation();
    }

    public void Renovation()
    {
        if (_gameManager == null || !_gameManager.isGameStart) { return; }
        CountDrawImage();
        CountDown();
    }

    void  CountDown()
    {
        if (_time <= 0) return;
        _time -= Time.deltaTime *2;
        
    }

    void CountDrawImage()
    {
        if (_isStartImageDraw) return;

        if(_time <= _drawTime && _time >  3)
        {
            if (_time == _drawTime)
            {
                _audioPlayer.Play(14, false);
             }

            _startCountImage[0].enabled = true;
        }
        else
                if (_time <=  3 && _time >  2)
        {
            
            _startCountImage[0].enabled = false;
            _startCountImage[1].enabled = true;
        }
        else
        if (_time <=  2 && _time > 1)
        {
            
            _startCountImage[1].enabled = false;
            _startCountImage[2].enabled = true;
        }
        else
        if ( _time < 1 && _time > 0)
        {
            
            _startCountImage[2].enabled = false;
            _startCountImage[3].enabled = true;
            countFinish = true;
        }
        else
        if (_time <= 0)
        {
            _isStartImageDraw = true;
            _time = _drawTime;
            _startCountImage[3].enabled = false;
        }

    }
}
