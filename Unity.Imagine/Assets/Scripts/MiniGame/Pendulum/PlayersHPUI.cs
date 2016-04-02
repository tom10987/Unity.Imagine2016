using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayersHPUI : MonoBehaviour {

    [SerializeField]
    Text[] _hpText = null;

    public Ball ball { get; set; }

    const int PLAYER_NUM = 2;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < PLAYER_NUM; ++i)
        {
            _hpText[i].text = "HP : ";
        }
    }
	
	// Update is called once per frame
	void Update () {
        var shields = ball.targetShield;
        if(shields == null || shields.Length != PLAYER_NUM) { return; }
        for(int i = 0; i < PLAYER_NUM; ++i)
        {
            _hpText[i].text = "HP : " + shields[i].hp.ToString();
        }
	}
}
