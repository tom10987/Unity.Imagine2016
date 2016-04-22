using UnityEngine;
using System.Collections.Generic;

public class ChargePlayer : MonoBehaviour
{
    //[SerializeField]
    //private Gage _gage;

    Gage[] _gage;

    ChargeGameController _chargeGameController;

    bool[] _pressOnce;

    public bool PressOnce1 { get { return _pressOnce[0]; } set { _pressOnce[0] = value; } }
    public bool PressOnce2 { get { return _pressOnce[1]; } set { _pressOnce[1] = value; } }


    public float _totalScorePlayer1 = 0;

    public float getTotalScorePlayer1 { get { return _totalScorePlayer1; } set { _totalScorePlayer1 = value; } }

    public float _totalScorePlayer2 = 0;

    public float getTotalScorePlayer2 { get { return _totalScorePlayer2; } set { _totalScorePlayer2 = value; } }

    int[] _firstSkipCount;

	GameManager _gameManager;

	ARDeviceManager _aRDeviceManager;

    GameController _controller;

	KeyCode _key1P;
	KeyCode _key2P;


   // [SerializeField]
    private EnergyGage[] _energyGage;

    //[SerializeField]
    Round _round = null;

	bool[] _isSE;
    void Start()
    {
		_isSE = new bool[2];
		_isSE [0] = false;
		_isSE [1] = false;
        _firstSkipCount = new int[2];
        _firstSkipCount[0] = 0;
        _firstSkipCount[1] = 0;
        _pressOnce = new bool[2];
        _pressOnce[1] = false;
        _pressOnce[0] = false;
        _gage = new Gage[2];
		_gameManager = FindObjectOfType<GameManager> ();
        _round = FindObjectOfType<Round>();
        _chargeGameController = GetComponent<ChargeGameController>();
        
        _controller = FindObjectOfType<GameController>();

        _gage[0] = GameObject.Find("GageUI1").GetComponent<Gage>();
        _gage[1] = GameObject.Find("GageUI2").GetComponent<Gage>();
        _energyGage = new EnergyGage[2];
        _energyGage[0] = GameObject.Find("EnergyGage1").GetComponent<EnergyGage>();
        _energyGage[1] = GameObject.Find("EnergyGage2").GetComponent<EnergyGage>();
		_aRDeviceManager = FindObjectOfType<ARDeviceManager> ();

		var key1P = _controller.player1.GetEnumerator();
		var key2P = _controller.player2.GetEnumerator();
		key1P.MoveNext();
		key2P.MoveNext();

		_key1P = key1P.Current;
		_key2P = key2P.Current;


    }

    void Update()
    {
        // IsKeyDownMoveGage();
        // EnergyGageMove();

    }

    public void IsKeyDownMoveGage()
    {

		if ( Input.GetKey(_key1P) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
			
                if (_pressOnce[0]) return;
			if (!_isSE [0]) {
				_gameManager.audio.Play (ClipIndex.se_No25_PowerCharge, 0.5f);
				_isSE [0] = true;
			}
                _gage[0].MoveSelectGage();
                //   Debug.Log("homo1");
           
        }
        else
			if (Input.GetKeyUp(_key1P) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
				
            if (_pressOnce[0]) return;
				_gameManager.audio.Play (ClipIndex.se_No23_JustCharge,0.5f);
                _totalScorePlayer1 += _gage[0].RangeSelectNow();
                _pressOnce[0] = true;

           
        
        }

		if ( Input.GetKey(_key2P) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
            

                if (_pressOnce[1]) return;
			if (!_isSE [1]) {
				_gameManager.audio.Play (ClipIndex.se_No25_PowerCharge, 0.5f);
				_isSE [1] = true;
			}
                _gage[1].MoveSelectGage();
                //   Debug.Log("homo2");
           
        }
        else
			if (Input.GetKeyUp(_key2P) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
				
            if (_pressOnce[1]) return;
				_gameManager.audio.Play (ClipIndex.se_No23_JustCharge);
                _totalScorePlayer2 += _gage[1].RangeSelectNow();
                _pressOnce[1] = true;
           
        
        }

    }

    public void EnergyGageMove()
    {

		if (_aRDeviceManager.player1.gameObject == gameObject.transform.gameObject)
        {
			Debug.Log ("homo1");
            if (_energyGage[0].ChargePowerGage() == true)
            {
                _round.NextRound();
            }
        }
        else
			if (_aRDeviceManager.player2.gameObject == gameObject.transform.gameObject)
        {
				Debug.Log ("homo2");
            if (_energyGage[1].ChargePowerGage() == true)
            {
                _round.NextRound();
            }
        }

    }

    public void Init()
    {
        foreach(var gage in _gage)
        {
            gage.InitGage();
        }
        //_gage.InitGage();
        _pressOnce[0] = false;
        _pressOnce[1] = false;
		_isSE [0] = false;
		_isSE [1] = false;

    }

}
