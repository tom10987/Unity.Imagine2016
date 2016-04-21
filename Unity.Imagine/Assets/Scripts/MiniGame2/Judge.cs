using UnityEngine;
using System.Collections;

public class Judge : MonoBehaviour
{
    [SerializeField]
    ChargePlayer[] _player;

    bool _isDraw = false;

    public bool getIsDraw { get { return _isDraw; } }

    Transform _winnerPlayer;

    public Transform getWinnerPlayer { get { return _winnerPlayer; } }

    void Start()
    {

    }

    void Update()
    {

    }

  public  void Winner()
    {
        //if (_player[0].getTotalScorePlayer1 > _player[1].getTotalScore)
        //{
        //    _winnerPlayer = gameObject.transform;
        //}
        //else
        //if (_player[0].getTotalScore < _player[1].getTotalScore)
        //{
        //    _winnerPlayer = gameObject.transform;
        //}
        //else
        //if (_player[0].getTotalScore == _player[1].getTotalScore)
        //{
        //    _isDraw = true;
        //}
    }

}
