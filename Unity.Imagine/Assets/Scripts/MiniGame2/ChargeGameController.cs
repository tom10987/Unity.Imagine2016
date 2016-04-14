using UnityEngine;
using System.Collections;
using System;

public class ChargeGameController : AbstractGame {

    bool _isStart = false;
    GageLengthChange _gageLengthChange;

    public CharacterData parameter { get; set; }

    ARDeviceManager _aRDeviceManager;

    bool _isDraw = false;

    public bool getIsDraw { get { return _isDraw; } }

    public GameObject player1Obj { get { return _aRDeviceManager.player1.gameObject; } }

    public GameObject player2Obj { get { return _aRDeviceManager.player2.gameObject; } }

    ChargePlayer _chargePlayer;

    Round _round = null;
        

    void Start()
    {
        
        Action();
    }

    void Update()
    {
        //_aRDeviceManager.player1
        // player1.CreateGameComponent<AbstractGame>(player1);
        // player2.CreateGameComponent<AbstractGame>();

        //Debug.Log(_aRDeviceManager.player1);
        Action();
    }

    public override void Action()
    {
        if(_isStart == false)
        {
            _round = FindObjectOfType<Round>();
            _gageLengthChange = FindObjectOfType<GageLengthChange>();
           gameObject.AddComponent<ChargePlayer>();
            parameter = GetComponentInChildren<CharacterData>();
            _aRDeviceManager = GetComponentInParent<ARDeviceManager>();
            _gageLengthChange.Parameter = parameter.getCharacterData.attack;
            _gageLengthChange.StatusGageLengthChange();
            _chargePlayer = GetComponent<ChargePlayer>();
            _isStart = true;
        }
        //_chargePlayer = GetComponent<ChargePlayer>();
        _chargePlayer.IsKeyDownMoveGage();
        _chargePlayer.EnergyGageMove();
        IsFinish();
    }  

    public override bool IsFinish()
    {
        if (_round.getRoundFinish)
        {
            GetWinner();
            Debug.Log(GetWinner());
            return true;
        }

        return false;
    }

    public override bool IsDraw()
    {

        return true;
    }

    public override Transform GetWinner()
    {

        if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) { return null; }

        if (_aRDeviceManager.player1.gameObject.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer1 > _aRDeviceManager.player2.gameObject.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer2)
        {
            return _aRDeviceManager.player1.transform;
        }
        else
        if (_aRDeviceManager.player1.gameObject.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer1 < _aRDeviceManager.player2.gameObject.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer2)
        {
            return _aRDeviceManager.player2.transform;
        }
        else
        if (_aRDeviceManager.player1.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer1 == _aRDeviceManager.player2.GetComponentInChildren<ChargePlayer>().getTotalScorePlayer2)
        {
            IsDraw();
        }

        return null;
    }
}
