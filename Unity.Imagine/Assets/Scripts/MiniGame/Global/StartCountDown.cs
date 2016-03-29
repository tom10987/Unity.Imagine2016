using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCountDown : MonoBehaviour {

    [SerializeField]
    KeyAction _gameManager = null;

    Text text { get; set; }

    [SerializeField, TextArea(2, 5)]
    string[] _texts;

    int _textIndex = 0;

    float _count = 0.0f;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = _texts[_textIndex];
        if(_gameManager == null) { Debug.Log("_gameManager が null です。KeyAction スクリプトが入ってるオブジェクトをいれてください。"); }
    }
	
	// Update is called once per frame
	void Update () {
        if (_gameManager == null || !_gameManager.isGameStart) { return; }
        _count += 1.0f * Time.deltaTime;
        Debug.Log("_count : " + _count);
        if(_count > _texts.Length) { }
        if(_textIndex != (int)_count && _textIndex != _texts.Length)
        {
            ++_textIndex;
            if(_textIndex == _texts.Length)
            {
                text.text = "";
                return;
            }
            text.text = _texts[_textIndex];
        }
	}
}
