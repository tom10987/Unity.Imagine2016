using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PendulumCanvas : MonoBehaviour {

    [SerializeField]
    private Text[] _texts = null;

    private Shield[] _shields = null;

	// Use this for initialization
	void Start () {
       
    }

    void Init()
    {
        var players = GameObject.Find("MiniGameManager").GetComponent<KeyAction>().GetPlayers();
        if(players == null) { return; }
        _shields = new Shield[players.Count];
        for (int i = 0; i < players.Count; ++i)
        {
            _shields[i] = players[i].GetComponentInChildren<Shield>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_shields == null || _shields.Length < 2 || 
            _shields[0] == null || _shields[1] == null) {
            Init();
            return;
        }
        for (int i = 0; i < _texts.Length; ++i)
        {
            _texts[i].text = _shields[i].hp.ToString();
        }
    }
}
