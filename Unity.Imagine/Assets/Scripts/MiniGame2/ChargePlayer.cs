using UnityEngine;
using System.Collections;

public class ChargePlayer : ActionManager
{
    [SerializeField]
    private Gage _gage;

    bool _pressOnce = false;

    public bool _getPressOnce { get { return _pressOnce; } }

    bool _isInit;

    public bool _getIsInit { get { return _isInit; } }

    bool _isGage = false;

    [SerializeField]
    GameController _controller;


    [SerializeField]
    private EnergyGage[] _energyGage;

    void Start(){
        _isInit = false;
    }

    void Update()
    {
       
        IsKeyDownMoveGage();
        EnergyGageMove();
    }

    void IsKeyDownMoveGage()
    {
        _gage.getIsGage = false;
        if (_pressOnce) return;

        var P1Key = _controller.player1.GetEnumerator();
        if (P1Key.MoveNext() && Input.GetKey(P1Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1)
        {
            _gage.MoveSelectGage();
        }
        else
        if (Input.GetKeyUp(P1Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player1)
        {
            _gage.RangeSelectNow();
            _gage.getIsGage = true;
            _pressOnce = true;
            _isInit = false;
        }

        var P2Key = _controller.player2.GetEnumerator();
        if (P2Key.MoveNext() && Input.GetKey(P2Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player2)
        {
            _gage.MoveSelectGage();
        }
        else
        if ( Input.GetKeyUp(P2Key.Current) && _energyGage[0].getSelectPlayer == EnergyGage.Player.Player2)
        {
            _gage.RangeSelectNow();
            _gage.getIsGage = true;
            _pressOnce = true;
             _isInit = false;
        }

    }

    void EnergyGageMove()
    {
        if (!_pressOnce) return;
        
        if (_energyGage[0].PowerGage() == true)
        {
            Init();
        }

    }

    void Init()
    {
        int finishPowerGageCount = 0;
        foreach (var energyGage in _energyGage)
        {
            if (energyGage._getIsPowerGage == true)
            {
                finishPowerGageCount++;
            }
        }

        if (finishPowerGageCount == _energyGage.Length)
        {
            _gage.getIsGage = false;
            _gage.InitGage();
            _pressOnce = false;
            _isInit = true;
        }
    }


    public override void Action(ARModel model){}
}
