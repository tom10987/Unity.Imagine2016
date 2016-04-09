using UnityEngine;
using System.Collections;
using System;

public class ChargeGameController : AbstractGame {

    bool _isStart = false;
    GageLengthChange _gageLengthChange;

    public CharacterData parameter { get; set; }

    bool _isDraw = false;

    public bool getIsDraw { get { return _isDraw; } }

    //public AR

    public override void Action()
    {
        if(_isStart == false)
        {
            _gageLengthChange = FindObjectOfType<GageLengthChange>();
           gameObject.AddComponent<ChargePlayer>();
            //ARModelExtension.CreateGameComponent<ChargeGameController>(player1);
            //ARModelExtension.CreateGameComponent<ChargeGameController>(player2);
            parameter = GetComponentInChildren<CharacterData>();
            _gageLengthChange.Parameter = parameter.getCharacterData.attack;
            _gageLengthChange.StatusGageLengthChange();
            _isStart = true;
        }

            
    }

    public override bool IsFinish()
    {
       // if ()
            return true;

        return false;
    }

    public override bool IsDraw()
    {
        return true;
    }

    public override Transform GetWinner()
    {

        if (player1 == null || player2 == null) { return null; }

        if (player1.GetComponent<ChargePlayer>().getTotalScore > player2.GetComponent<ChargePlayer>().getTotalScore)
        {
            return player1.transform;
        }
        else
   if (player1.GetComponent<ChargePlayer>().getTotalScore < player2.GetComponent<ChargePlayer>().getTotalScore)
        {
            return player2.transform;
        }
        else
   if (player1.GetComponent<ChargePlayer>().getTotalScore == player2.GetComponent<ChargePlayer>().getTotalScore)
        {
            IsDraw();
        }

        return null;
    }
}
