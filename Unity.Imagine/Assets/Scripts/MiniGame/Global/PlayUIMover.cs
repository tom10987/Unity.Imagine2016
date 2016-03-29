using UnityEngine;
using System.Collections;

public class PlayUIMover : MonoBehaviour {

    [SerializeField]
    GameMnueTab _gameMnueTab;

    [SerializeField]
    Vector3 _targetPos;
    public Vector3 targetPos { get { return _targetPos; } }

    [SerializeField]
    float _speed = 10.0f;

    RectTransform rect { get; set; }

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_gameMnueTab.isPushPlayButton) { return; }
        rect.anchoredPosition3D -= (rect.anchoredPosition3D - _targetPos) / _speed;
	}
}
