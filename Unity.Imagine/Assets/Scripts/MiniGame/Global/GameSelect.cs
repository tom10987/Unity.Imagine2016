using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSelect : MonoBehaviour {

    [SerializeField]
    ResultDirecter _resultDirecter = null;

    private enum GameName
    {
        RapidFire,
        Pendulum,
        Charge,
    }

    [SerializeField]
    private bool _debug = false;
    [SerializeField]
    private GameName _initGameName = GameName.Pendulum;

    private GameObject _hitParticle1P = null;
    private GameObject hitParticle1P
    {
        get
        {
            if(_hitParticle1P == null)
            {
                _hitParticle1P = Resources.Load("MiniGame/Pendulum/Effect/red") as GameObject;
            }
            return _hitParticle1P;
        }
    }

    private GameObject _hitParticle2P = null;
    private GameObject hitParticle2P
    {
        get
        {
            if (_hitParticle2P == null)
            {
                _hitParticle2P = Resources.Load("MiniGame/Pendulum/Effect/blue") as GameObject;
            }
            return _hitParticle2P;
        }
    }

    static GameName gameName { get; set; }

    public static void ChangeRapidFire()
    {
        gameName = GameName.RapidFire;
    }
    public static void ChangePendulum()
    {
        gameName = GameName.Pendulum;
    }
    public static void ChangeCharge()
    {
        gameName = GameName.Charge;
    }

    /// ////////////////////////////////////////////////////////////////////////

    private const int _PLAYER_MAX = 2;

    private Component[] _components;

    private List<GameObject> _objList = new List<GameObject>();

    private bool _createFlag = true;
    private bool _resultFlag = true;

    // Use this for initialization
    void Start () {
        _components = new Component[_PLAYER_MAX];

        if (_debug)
        {
            gameName = _initGameName;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_createFlag)
        {
            CreateGame();
        }
        else
        {
            GameUpdate();
        }
    }

    void CreateGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != _PLAYER_MAX) { return; }
        switch (gameName)
        {
            case GameName.RapidFire:
                CreateRapidFire(players);
                break;
            case GameName.Pendulum:
                CreatePendulum(players);
                break;
            case GameName.Charge:
                CreateCharge(players);
                break;
        }
        _createFlag = false;
    }

    void CreateRapidFire(GameObject[] players)
    {
        //ComponentDestroy();
        for (int i = 0; i < _PLAYER_MAX; ++i)
        {
            _components[i] = players[i].AddComponent(typeof(GameTest1));
        }
        GetComponent<KeyAction>().InitOn();
    }

    void CreatePendulum(GameObject[] players)
    {
        //ComponentDestroy();
        for (int i = 0; i < _PLAYER_MAX; ++i)
        {
            _components[i] = players[i].AddComponent(typeof(Pendulum));
        }
        //CreateShield();
        var obj = Instantiate(Resources.Load("MiniGame/Pendulum/Ball") as GameObject);
        obj.GetComponent<Ball>().SetPlayers(players);
        var pos = players[1].transform.position - players[0].transform.position;
        obj.transform.position = players[0].transform.position + pos * 0.5f;
        obj.name = "Ball";

        var canvasObj = Resources.Load("MiniGame/Pendulum/PendulumCanvas") as GameObject;
        var canvas = Instantiate(canvasObj);
        canvas.name = canvasObj.name;

        //_objList.Add(obj);
        //_objList.Add(canvas);
        _objList.Add(players[0].GetComponent<PendulumTest>().CreateShield(hitParticle1P));
        _objList.Add(players[1].GetComponent<PendulumTest>().CreateShield(hitParticle2P));
        GetComponent<KeyAction>().InitOn();
    }

    void CreateCharge(GameObject[] players)
    {

    }

    void GameUpdate()
    {
        switch (gameName)
        {
            case GameName.RapidFire:
                RapidFireUpdate();
                break;
            case GameName.Pendulum:
                PendulumUpdate();
                break;
            case GameName.Charge:
                ChargrUpdate();
                break;
        }
    }

    void RapidFireUpdate()
    {
        
    }

    void PendulumUpdate()
    {
        if (_resultFlag)
        {
            if (_objList[0] == null)
            {
                _resultDirecter.SetResult(2);
                _resultFlag = false;
            }
            else if (_objList[1] == null)
            {
                _resultDirecter.SetResult(1);
                _resultFlag = false;
            }
        }
    }

    void ChargrUpdate()
    {

    }

    void ComponentDestroy()
    {
        foreach(var obj in _objList)
        {
            if(obj == null) { continue; }
            Destroy(obj);
        }
        foreach(var component in _components)
        {
            Destroy(component);
        }
        _components = new Component[_PLAYER_MAX];
        _objList = new List<GameObject>();
    }

    //void OnGUI()
    //{
        //if (GUI.Button(new Rect(20, Screen.height - 70, 100, 50), "Game1"))
        //{
        //    ComponentDestroy();
        //    for (int i = 0; i < _PLAYER_MAX; ++i)
        //    {
        //        _components[i] = players[i].AddComponent(typeof(GameTest1));
        //    }
        //    GetComponent<KeyAction>().InitOn();
        //}

        //if (GUI.Button(new Rect(140, Screen.height - 70, 100, 50), "Game2"))
        //{
        //    ComponentDestroy();
        //    for (int i = 0; i < _PLAYER_MAX; ++i)
        //    {
        //        _components[i] = players[i].AddComponent(typeof(Pendulum));
        //    }
        //    //CreateShield();
        //    var obj = Instantiate(Resources.Load("MiniGame/Pendulum/Ball2") as GameObject);
        //    obj.GetComponent<Ball>().SetPlayers(players);
        //    var pos = players[1].transform.position - players[0].transform.position;
        //    obj.transform.position = players[0].transform.position + pos * 0.5f;
        //    obj.name = "Ball";

        //    var canvasObj = Resources.Load("MiniGame/Pendulum/PendulumCanvas") as GameObject;
        //    var canvas = Instantiate(canvasObj);
        //    canvas.name = canvasObj.name;

        //    _objList.Add(obj);
        //    _objList.Add(canvas);
        //    _objList.Add(players[0].GetComponent<Pendulum>().CreateShield());
        //    _objList.Add(players[1].GetComponent<Pendulum>().CreateShield());
        //    GetComponent<KeyAction>().InitOn();
        //}

        //if (GUI.Button(new Rect(260, Screen.height - 70, 100, 50), "Game3"))
        //{

        //}
    //}
}
