using UnityEngine;
using System.Collections;

public class ImageSizeChanger : MonoBehaviour {

    [SerializeField]
    bool _drawFlag = false;
    public bool isDraw { get { return _drawFlag; } }

    [SerializeField]
    RectTransform _rect = null;
    [SerializeField]
    float _changeSizeSpeed = 5.0f;

    Vector2 initSize { get; set; }

	// Use this for initialization
	void Start () {
        initSize = _rect.sizeDelta;
        _rect.sizeDelta = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (_drawFlag)
        {
            _rect.sizeDelta += (initSize - _rect.sizeDelta) / _changeSizeSpeed;
        }
        else
        {
            _rect.sizeDelta += -_rect.sizeDelta / _changeSizeSpeed;
        }
    }

    public void ChangeSize()
    {
        _drawFlag = _drawFlag ? false : true;
    }
}
