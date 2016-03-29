using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndImageDraw : MonoBehaviour {

    KeyAction _gameManager = null;

    [SerializeField]
    float _drawTime = 4.0f;

    float _regularInterval;

    [SerializeField]
    AudioPlayer _audioPlayer;

    [SerializeField, TooltipAttribute("表示する順番にImageを入れてください")]
    Image[] _startCountImage = null;

    bool _countFinish = false;
    public bool getCountFinish { get { return _countFinish; } set { _countFinish = value; } }

    bool _isSe = false;

    void Start()
    {
        
        if (_timeCount == null)
        {
            _timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        }
        
        foreach (var image in _startCountImage)
        {
            image.enabled = false;
        }

        if (_gameManager == null) { Debug.Log("_gameManager が null です。KeyAction スクリプトが入ってるオブジェクトをいれてください。"); }
    }

    StartCount _startCount;

    [SerializeField]
    TimeCount _timeCount;
	
	void Update ()
    {
	if(_timeCount.time <= _drawTime)
        {
            Draw();
        }
	}

    void Draw()
    {
        if (_timeCount.time <= _drawTime && _timeCount.time >  3)
        {
            if (_isSe == false)
            {
                _isSe = true;
                _audioPlayer.Play(15, false);
            }
            _startCountImage[0].enabled = true;
        }
        else
        if (_timeCount.time <=  3 && _timeCount.time > 2)
        {
            _startCountImage[0].enabled = false;
            _startCountImage[1].enabled = true;
        }
        else
if (_timeCount.time <= 2 && _timeCount.time > 1)
        {
            _startCountImage[1].enabled = false;
            _startCountImage[2].enabled = true;
        }
        else
if (_timeCount.time < 1 && _timeCount.time > 0)
        {
            _startCountImage[2].enabled = false;
            _startCountImage[3].enabled = true;
            _countFinish = true;
        }
        else
if (_timeCount.time <= 0)
        {
           
            _startCountImage[3].enabled = false;
            _isSe = false;
        }

    }
}
