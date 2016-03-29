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


    [SerializeField]
    private EnergyGage[] _energyGage;

    void Start(){
        _isInit = false;
    }

    void Update()
    {
        //Debug.Log(_isInit);
        //Debug.Log(_energyGage[0]._getIsPowerGage); 
        IsKeyDownMoveGage();
        EnergyGageMove();
    }

    void IsKeyDownMoveGage()
    {
        if (_pressOnce) return;

        if (Input.GetKey(keyCode))
        {
            _gage.MoveSelectGage();
        }
        else
            if (Input.GetKeyUp(keyCode))
        {
            _gage.RangeSelectNow();
            _pressOnce = true;
           // _isInit = false;
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
            //Debug.Log(energyGage._getIsPowerGage);
            if (energyGage._getIsPowerGage == true)
            {
                finishPowerGageCount++;
            }
        }

        if (finishPowerGageCount == _energyGage.Length)
        {
            _gage.InitGage();
            _pressOnce = false;
            _isInit = true;

        }
    }

    //これを書いとかないとウザイ
    public override void Action(ARModel model){}
}
