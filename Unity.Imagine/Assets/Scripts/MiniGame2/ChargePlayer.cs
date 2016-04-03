using UnityEngine;
using System.Collections;

public class ChargePlayer : MonoBehaviour
{
    [SerializeField]
    private Gage _gage;

    bool _pressOnce = false;

    public bool PressOnce { get { return _pressOnce; } set { _pressOnce = value; } }


    public float _totalScore = 0;

    public float getTotalScore { get { return _totalScore; } }


    [SerializeField]
    GameController _controller;


    [SerializeField]
    private EnergyGage[] _energyGage;

    [SerializeField]
    Round _round;

    void Start()
    {
    }

    void Update()
    {

        IsKeyDownMoveGage();
        EnergyGageMove();
    }

    void IsKeyDownMoveGage()
    {
        if (_pressOnce) return;

        var P1Key = _controller.player1.GetEnumerator();
        if (P1Key.MoveNext() && Input.GetKey(P1Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1)
        {
            _gage.MoveSelectGage();
        }
        else
        if (Input.GetKeyUp(P1Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1)
        {
            _totalScore += _gage.RangeSelectNow();
            _pressOnce = true;
        }

        var P2Key = _controller.player2.GetEnumerator();
        if (P2Key.MoveNext() && Input.GetKey(P2Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player2)
        {
            _gage.MoveSelectGage();
        }
        else
        if (Input.GetKeyUp(P2Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player2)
        {
            _totalScore += _gage.RangeSelectNow();
            _pressOnce = true;
        }

    }

    void EnergyGageMove()
    {


        if (_energyGage[0].ChargePowerGage() == true)
        {
            _round.NextRound();
        }

    }

   public void Init()
    {
        _gage.InitGage();
        _pressOnce = false;
    }

}
