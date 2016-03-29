using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyKey : MonoBehaviour {

    [SerializeField]
    GameManager _manager = null;

    [SerializeField]
    GameController _controller = null;

    [SerializeField]
    ImageSizeChanger[] _myImages = null;

    [SerializeField]
    Text[] _playerKeyText = null;

    [SerializeField]
    UIMover _uiMover = null;

    Vector3 _drawPos = Vector3.zero;
    Vector3 _hidePos = new Vector3(0.0f, 1200.0f, 0.0f);

    bool _isReady = false;
    //public bool isReady { get { return _isReady; } }

    //[SerializeField]
    //bool _debugFlag = false;

    [SerializeField]
    float _delayPlayTime = 2.0f;

    void Update()
    {
        if (!_myImages[0].isDraw)
        {
            var P1Key = _controller.player1.GetEnumerator();           
            if (P1Key.MoveNext()  && Input.GetKeyDown(P1Key.Current))
            {
                _myImages[0].ChangeSize();
            }
            _playerKeyText[0].text = P1Key.Current.ToString();
        }

        if (!_myImages[1].isDraw)
        {
            var P2Key = _controller.player2.GetEnumerator();
            if (P2Key.MoveNext() && Input.GetKeyDown(P2Key.Current))
            {
                _myImages[1].ChangeSize();
            }
            _playerKeyText[1].text = P2Key.Current.ToString();
        }

        ReadyCheak();

        //if (_debugFlag)
        //{
        //    DrawReadyImage();
        //}
        //else
        //{
        //    HideReadyImage();
        //}

        if(_isReady)
        {
            _delayPlayTime -= Time.deltaTime;
            if(_delayPlayTime <= 0)
            {
                HideReadyImage();
            }
        }
    }

    void ReadyCheak()
    {
        foreach (var image in _myImages)
        {
            if (!image.isDraw) { return; }
        }
        _isReady = true;
    }

    // これを呼べば描画される(描画位置に画像が飛んでいく)
    public void DrawReadyImage()
    {
        _uiMover.targetPos = _drawPos;
    }

    // これを呼べば描画されなくなる(画面外に画像が飛んでいく)
    public void HideReadyImage()
    {
        _uiMover.targetPos = _hidePos;
        _manager.OnPlay();
    }
}
