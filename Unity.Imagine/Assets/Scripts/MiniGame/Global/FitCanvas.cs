using UnityEngine;
using System.Collections;

public class FitCanvas : MonoBehaviour {

    [SerializeField]
    float _splitWidth = 3.0f;
    [SerializeField]
    float _magnificationWidth = 2.0f;

    [SerializeField]
    float _splitHeight = 3.0f;
    [SerializeField]
    float _magnificationHeight = 2.0f;

    RectTransform rect;

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        rect.sizeDelta = new Vector2(Screen.width / _splitWidth * _magnificationWidth, Screen.height / _splitHeight * _magnificationHeight);
	}
}
