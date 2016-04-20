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

    ChargePlayer _chargePlayer;

    bool _isFinish = false;

    Round _round = null;
    GameResource ressouces;
    IEnumerator<GameObject> ressouce;

    static bool _isCreateUI = false;

    void Start()
    {
        
        ressouces = GameResources.instance.charge;
        ressouce = ressouces.CreateResource().GetEnumerator();

        if (_isCreateUI == false)
        {
            

            ressouce.MoveNext();
            _isCreateUI = true;
        }
        //ここクソコード
        _gageLengthChange = new GageLengthChange[2];  
        _gageLengthChange[0] = GameObject.Find("GageUI1").GetComponent<GageLengthChange>();
        _gageLengthChange[1] = GameObject.Find("GageUI2").GetComponent<GageLengthChange>();

        
        
        //Action();

    }

    void Update()
    {
        Action();
    }

    public override void Action()
    {
        if (_isStart == false)
        {
            _round = FindObjectOfType<Round>();
            //_gageLengthChange = FindObjectOfType<GageLengthChange>();
            gameObject.AddComponent<ChargePlayer>();
            parameter = GetComponentInChildren<CharacterData>();
            _aRDeviceManager = GetComponentInParent<ARDeviceManager>();
            _chargePlayer = GetComponent<ChargePlayer>();
            _isStart = true;
            if (_aRDeviceManager.player1.gameObject == gameObject.transform.parent.gameObject)
            {
                _gageLengthChange[0].Parameter = parameter.getCharacterData.attack;
                _gageLengthChange[0].StatusGageLengthChange();
            }
            else
            if (_aRDeviceManager.player2.gameObject == gameObject.transform.parent.gameObject)
            {
                _gageLengthChange[1].Parameter = parameter.getCharacterData.attack;
                _gageLengthChange[1].StatusGageLengthChange();
            }
            
            
        }
        if (_aRDeviceManager == null) return;
        if (_aRDeviceManager.player1 == null || _aRDeviceManager.player2 == null) { return; }
        
        _chargePlayer = GetComponent<ChargePlayer>();
        _chargePlayer.IsKeyDownMoveGage();
        _chargePlayer.EnergyGageMove();
        IsFinish();
    }  

    public override bool IsFinish()
    {
        if (_isFinish) return true;
        if (_round.getRoundFinish)
        {
            LaserCreate();
            GetWinner();
            
            return _isFinish = true;
        }

        return _isFinish = false;
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

    //レーザーのエフェクトの生成
    public void LaserCreate()
    {
        float distance = Vector3.Distance(_aRDeviceManager.player1.gameObject.GetComponentInChildren<ChargeGameController>().gameObject.transform.position, _aRDeviceManager.player2.gameObject.GetComponentInChildren<ChargeGameController>().gameObject.transform.position);

        //Debug.Log(distance);

        float halfScaleLaser = distance / 2;

        if (_aRDeviceManager.player1.gameObject == gameObject.transform.parent.gameObject)
        {
          
            ressouce.MoveNext();
            Destroy(ressouce.Current.gameObject);
            ressouce.MoveNext();
            ressouce.Current.transform.rotation = transform.rotation;
            ressouce.Current.transform.position = transform.position;
            ressouce.Current.transform.localScale = new Vector3(1,1, 1/ halfScaleLaser);
            ressouce.Current.transform.parent = transform.parent;
            ressouce.Current.name = ressouce.Current.name;
        }
        else
        if(_aRDeviceManager.player2.gameObject == gameObject.transform.parent.gameObject)
        {
            ressouce.MoveNext();
            Destroy(ressouce.Current.gameObject);
            ressouce.MoveNext();
            Destroy(ressouce.Current.gameObject);
            ressouce.MoveNext();
            ressouce.Current.transform.rotation = transform.rotation;
            ressouce.Current.transform.position = transform.position;
            ressouce.Current.transform.localScale = new Vector3(1, 1, 1/ halfScaleLaser);
            ressouce.Current.transform.parent = transform.parent;
            ressouce.Current.name = ressouce.Current.name;
        }
    }

}
