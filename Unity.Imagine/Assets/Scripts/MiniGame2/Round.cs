using UnityEngine;
using System.Collections;

public class Round : MonoBehaviour
{

    [SerializeField]
    int _roundCount = 3;

    //[SerializeField]
    EnergyGage[] _energyGage;


    //[SerializeField]
    ChargePlayer[] _chargePlayer;

    bool _roundFinish = false;

    public bool getRoundFinish { get { return _roundFinish; } set { _roundFinish = value; } }

    int _round;
    public int getRoundCount { get { return _round; } set { _round = value; } }


    void Start()
    {
        _energyGage = FindObjectsOfType<EnergyGage>();
        
        _round = _roundCount;
    }

    void Update(){}

    public void NextRound()
    {
		Debug.Log (_round + "round");
        int finishPowerGageCount = 0;
        foreach (var energyGage in _energyGage)
        {
            if (energyGage._getIsPowerGage == true)
            {
                finishPowerGageCount++;
            }
        }

        if (_round <= 1)
        {
            if (finishPowerGageCount == _energyGage.Length)
            {
                _roundFinish = true;
            }
            return;
        }

        if (finishPowerGageCount == _energyGage.Length)
        {
            _chargePlayer = FindObjectsOfType<ChargePlayer>();
            _round--;
            foreach (var player in _chargePlayer)
            {
                player.Init();
            }
        }
    }

}
