using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyAction : MonoBehaviour {

    [SerializeField]
    private KeyCode[] _key = { KeyCode.A, KeyCode.D };

    private ActionManager[] _actionMgr = null;

    private int initCount { get; set; }
    private const int INIT_COUNT_FRAME = 1;

    public bool isGameStart { get; set; }

    // Use this for initialization
    void Start () {
        Init();
        initCount = -1;
        isGameStart = false;
    }
	
	// Update is called once per frame
	void Update () {
        Init();
        KeysAction();
    }

    public void InitOn()
    {
        initCount = INIT_COUNT_FRAME;
    }

    void Init()
    {
        if(_actionMgr != null) { return; }
        //if (initCount != 0)
        //{
        //    --initCount;
        //    return;
        //}
        //--initCount;
        _actionMgr = GetComponentsInChildren<ActionManager>();

        // 両端のオブジェクトの要素番号を取り出して1P、2P決める処理
        if (_actionMgr.Length < 2) { return; }
        float max_x = -10000;
        int max_index = -1;
        float min_x = 10000;
        int min_index = -1;
        for (int i = 0; i < _actionMgr.Length; ++i)
        {
            if (!_actionMgr[i].isRendered) { continue; }
            if (_actionMgr[i].transform.position.x > max_x)
            {
                max_x = _actionMgr[i].transform.position.x;
                max_index = i;
            }
            if (_actionMgr[i].transform.position.x < min_x)
            {
                min_x = _actionMgr[i].transform.position.x;
                min_index = i;
            }
        }

        // 見つからなかった場合抜ける
        if(max_index == min_index || max_index == -1 || min_index == -1) {
            _actionMgr = null;
            return;
        }

        // 両端の要素番号が分かったので _actionMgr を再生成
        _actionMgr = new ActionManager[] { _actionMgr[min_index], _actionMgr[max_index] };

        // 1P、2Pの操作するキーを決める
        for (int i = 0; i < _actionMgr.Length; ++i)
        {
            _actionMgr[i].keyCode = _key[i];
        }

        // お互いの GameObject を受け取れるようにする
        _actionMgr[0].Enemy = _actionMgr[1].transform.gameObject;
        _actionMgr[1].Enemy = _actionMgr[0].transform.gameObject;

        // この色変えは確認用です。
        //_actionMgr[0].GetComponent<MeshRenderer>().material.color = Color.red;
        //_actionMgr[1].GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void KeysAction()
    {
        if (_actionMgr == null || _actionMgr.Length < 2) { return; }
        foreach (var action in _actionMgr)
        {
            if (!action.isRendered) {
                isGameStart = false;
                return;
            }
        }
        isGameStart = (isGameStart == false) ? true : false;
    /*
        foreach (var action in _actionMgr)
        {
            //action.Action();
        }
    */
    }

    public List<GameObject> GetPlayers()
    {
        if(_actionMgr == null) { return null; }
        List<GameObject> list = new List<GameObject>();
        foreach (var action in _actionMgr)
        {
            list.Add(action.gameObject);
        }
        return list;
    }
}
