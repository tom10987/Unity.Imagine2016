using UnityEngine;
using System.Collections.Generic;

public class ScoreCompare : MonoBehaviour {
    [SerializeField]
    GameObject _bullet;

    [SerializeField]
    TimeCount _timeCount = null;

    KeyAction _actionManager;

    SuddenDeath _suddenDeath;

    List<GameObject> _playerList = new List<GameObject>();

    List<Barrage> _barragelist = new List<Barrage>();

    bool _displayScore = false;

   public bool getDisplayScore { get { return _displayScore; } }

    bool _isDraw = false;

    public bool getIsDraw { get { return _isDraw; } set { _isDraw = value; } }

   public enum WinPlayer{
        Player1,
        Player2
    }

    WinPlayer _winPlayer;

    public WinPlayer getWinPlayer { get { return _winPlayer; } }

    void Start ()
    {
        _suddenDeath = FindObjectOfType<SuddenDeath>();
        //_timeCount = GameObject.Find("Time").GetComponent<TimeCount>();
        _actionManager = FindObjectOfType<KeyAction>();
        
    }
	
	void Update ()
    {
        Compare();
    }

    void Compare()
    {
        if (_timeCount.time > 0) return;
        if (_timeCount.time  == 0 )
        {
            

            _playerList = _actionManager.GetPlayers();
            foreach (var selectPlayer in _playerList)
            {
                _barragelist.Add(selectPlayer.GetComponentInChildren<Barrage>());
            }

        }
        if (_playerList.Count == 0) return;


        if (_barragelist[0]._getKeyCount > _barragelist[1]._getKeyCount)
        {
            //_playerList[0]
            _winPlayer = WinPlayer.Player1;
            if (_displayScore == false)
            {
                _barragelist[0].Bullet(_bullet);
            }
            _displayScore = true;
        }
        else
        if (_barragelist[0]._getKeyCount < _barragelist[1]._getKeyCount)
        {
            _winPlayer = WinPlayer.Player2;
            if (_displayScore == false)
            {
                _barragelist[1].Bullet(_bullet);
            }
            _displayScore = true;
        }
        else
        if(_barragelist[0]._getKeyCount == _barragelist[1]._getKeyCount)
        {
          
            
            _isDraw = true;
            _suddenDeath.getCountFinish = false;
        }
    }

}
