using UnityEngine;
using System.Collections.Generic;
using System;

public class ChargeGameController : AbstractGame {

    bool _isStart = false;
    GageLengthChange[] _gageLengthChange;

    public CharacterData parameter { get; set; }

    ARDeviceManager _aRDeviceManager;

    bool _isDraw = false;

    public bool getIsDraw { get { return _isDraw; } }

    public GameObject player1Obj { get { return _aRDeviceManager.player1.gameObject; } }

    public GameObject player2Obj { get { return _aRDeviceManager.player2.gameObject; } }

    EnergyGage[] _energyGage;

    ChargePlayer _chargePlayer;

    bool _isFinish = false;

    bool _drawCheck = false;

    Round _round = null;
    
	GameObject[] ressouces;

    static bool _isCreateUI = false;

	ChargePlayer _p1charge; 
	ChargePlayer _p2charge;


    void Start()
    {


        _energyGage = FindObjectsOfType<EnergyGage>();
		_round = FindObjectOfType<Round>();

		ressouces = GameResources.instance.charge.CreateResourceArray();


			 _p1charge = gameManager.player1.gameObject.AddComponent<ChargePlayer>();
			 _p2charge = gameManager.player2.gameObject.AddComponent<ChargePlayer>();
						

        _gageLengthChange = new GageLengthChange[2];  
        _gageLengthChange[0] = GameObject.Find("GageUI1").GetComponent<GageLengthChange>();
        _gageLengthChange[1] = GameObject.Find("GageUI2").GetComponent<GageLengthChange>();



        // ゲームルールのテキスト初期化
        string text = ("タイミングよく\n").ToColor(RichText.ColorType.red).ToSize(100);
            text += ("ボタンをはなしてパワーをためよう!!").ToSize(60);//("").ToColor(RichText.ColorType.red).ToSize(100);

        gameRule = text;


		_aRDeviceManager = FindObjectOfType<ARDeviceManager>();

		_round = FindObjectOfType<Round>();
		//_gageLengthChange = FindObjectOfType<GageLengthChange>();

		parameter = GetComponentInChildren<CharacterData>();

		_isStart = true;
		_aRDeviceManager = FindObjectOfType<ARDeviceManager>();
		if(_aRDeviceManager.name == gameObject.name)
		if (gameManager.player1.gameObject == gameObject.transform.parent.gameObject)
		{
			_gageLengthChange[0].Parameter = parameter.getCharacterData.attack;
			_gageLengthChange[0].StatusGageLengthChange();
		}
		else
			if (gameManager.player2.gameObject == gameObject.transform.parent.gameObject)
			{
				_gageLengthChange[1].Parameter = parameter.getCharacterData.attack;
				_gageLengthChange[1].StatusGageLengthChange();
			}
        //Action();

    }

    void Update()
    {
        //Action();
    }

    public override void Action()
    {
   
        if (_aRDeviceManager == null) return;
        if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) { return; }

        if (_aRDeviceManager.player1.isVisible == false || _aRDeviceManager.player1.isVisible == false) return;

			
		     _p1charge.IsKeyDownMoveGage ();
		     _p1charge.EnergyGageMove ();
			 _p2charge.IsKeyDownMoveGage ();
			 _p2charge.EnergyGageMove ();


		if (_round.getRoundFinish) {
			if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) {
				return ;
			}

			if (_aRDeviceManager.player1.GetComponentInChildren<ChargePlayer> ().getTotalScorePlayer1 == _aRDeviceManager.player2.GetComponentInChildren<ChargePlayer> ().getTotalScorePlayer2) {
				IsDraw ();
			} else {
				GetWinner ();

			}
		}

    }  


    public override bool IsFinish()
    {
        if (_isFinish) return true;
		_round = FindObjectOfType<Round>();
        if (_round.getRoundFinish)
        {
            if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) { return _isFinish = false; }

                GetWinner();


            return _isFinish = true;
        }

        return _isFinish = false;
    }

    public override bool IsDraw()
    {
		if (gameManager.player1.gameObject.GetComponent<ChargePlayer> ().getTotalScorePlayer1 == gameManager.player2.gameObject.GetComponent<ChargePlayer> ().getTotalScorePlayer2) 
		{
			return _drawCheck = true;
		}
		return _drawCheck = false;
    }

    public override void GameStart()
    {
        if (_drawCheck == true)
        {
            _round.getRoundCount = 2;
            _round.getRoundFinish = false;
            _isFinish = false;
			_energyGage = FindObjectsOfType<EnergyGage>();
            foreach (var energy in _energyGage)
            {
                energy.Init();

            }
        }
    }

    public override void SuddenDeathAction()
    {

    }


    public override Transform GetWinner()
    {

        if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) { return null; }

		if (gameManager.player1.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer1 > gameManager.player2.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer2)
        {
            LaserCreate();
            return _aRDeviceManager.player1.transform;
        }
        else
			if (gameManager.player1.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer1 < gameManager.player2.gameObject.GetComponent<ChargePlayer>().getTotalScorePlayer2)
        {
            LaserCreate();
            return _aRDeviceManager.player2.transform;
        }


        return null;
    }



    //レーザーのエフェクトの生成
    public void LaserCreate()
    {
		var creater = ressouces [1].GetComponent<LeserCreater>();

		gameManager.audio.Play (ClipIndex.se_No26_BeamFire);
		var laser1 = Instantiate (creater.getPlayer1);
		laser1.transform.rotation = gameManager.player1.transform.rotation;
		laser1.transform.position = gameManager.player1.transform.position;
		laser1.transform.SetParent (gameManager.player1.transform);
		laser1.transform.Translate (0.0f,50.2f,50.2f);

		var laser2 = Instantiate (creater.getPlayer2);
		laser2.transform.rotation = gameManager.player2.transform.rotation;
		laser2.transform.position = gameManager.player2.transform.position;
		laser2.transform.SetParent (gameManager.player2.transform);
		laser2.transform.Translate (0.0f,50.2f,50.2f);
    }

}
