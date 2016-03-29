using UnityEngine;
using System.Collections;

public class HowToPlayButtonPos : MonoBehaviour {

    [SerializeField]
    RectTransform _parentRect;

    RectTransform _myRect;

	// Use this for initialization
	void Start () {
        _myRect = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        _myRect.anchoredPosition = _parentRect.sizeDelta / 2;
        float size = _parentRect.sizeDelta.x / 5;
        _myRect.sizeDelta = new Vector2(size, size);
    }
}
