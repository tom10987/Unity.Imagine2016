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

    public float getTotalScorePlayer1 { get { return _totalScorePlayer1; } }

    public float _totalScorePlayer2 = 0;

    public float getTotalScorePlayer2 { get { return _totalScorePlayer2; } }



    GameController _controller;

    IEnumerator<KeyCode> P1Key;
    IEnumerator<KeyCode> P2Key;

   // [SerializeField]
    private EnergyGage[] _energyGage;

    //[SerializeField]
    Round _round = null;

    void Start()
    {
        _pressOnce = new bool[2];
        _gage = new Gage[2];
        //_controller = GetComponentInParent<GameController>();
        _round = FindObjectOfType<Round>();
        _chargeGameController = GetComponent<ChargeGameController>();
        //        _controller = GetComponentInParent<GameController>();
        _controller = FindObjectOfType<GameController>();

        _gage[0] = GameObject.Find("GageUI1").GetComponent<Gage>();
        _gage[1] = GameObject.Find("GageUI2").GetComponent<Gage>();
        _energyGage = FindObjectsOfType<EnergyGage>();
 

    }

    void Update()
    {

        // IsKeyDownMoveGage();
        // EnergyGageMove();

        Debug.Log("hit1" + _pressOnce[0]);
        Debug.Log("hit2" + _pressOnce[1]);
    }

    public void IsKeyDownMoveGage()
    {
        P1Key = _controller.player1.GetEnumerator();
        P2Key = _controller.player2.GetEnumerator();

        if (P1Key.MoveNext() && Input.GetKey(P1Key.Current) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
            if (_pressOnce[0]) return;
            _gage[0].MoveSelectGage();
         //   Debug.Log("homo1");
        }
        else
        if (Input.GetKeyUp(P1Key.Current) /*&& _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1*/)
        {
            if (_pressOnce[0]) return;
            _totalScorePlayer1 += _gage[0].RangeSelectNow();
            _pressOnce[0] = true;
        }

        if (P2Key.MoveNext() && Input.GetKey(P2Key.Current) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
            if (_pressOnce[1]) return;
            _gage[1].MoveSelectGage();
         //   Debug.Log("homo2");
        }
        else
        if (Input.GetKeyUp(P2Key.Current) /*&& _energyGage[1].getSelectPlayer == EnergyGage.Player.Player2*/)
        {
            if (_pressOnce[1]) return;
            _totalScorePlayer2 += _gage[1].RangeSelectNow();
            _pressOnce[1] = true;
        }

    }

    public void EnergyGageMove()
    {
        _chargeGameController = GetComponent<ChargeGameController>();
        //Debug.Log(_chargeGameController.player1Obj);
        //Debug.Log(_chargeGameController.player2Obj);
        if (_chargeGameController.player1Obj == gameObject.transform.parent.gameObject)
        {
            if (_energyGage[0].ChargePowerGage() == true)
            {
                _round.NextRound();
            }
        }
        else
        if (_chargeGameController.player2Obj == gameObject.transform.parent.gameObject)
        {
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

    }

}
