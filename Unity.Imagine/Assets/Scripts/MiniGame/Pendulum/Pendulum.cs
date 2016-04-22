using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pendulum : AbstractGame
{
    Shield shield1P { get; set; }
    Shield shield2P { get; set; }

    GameController _controller = null;

    public static string _ballName = "Ball";

    KeyCode _key1P;
    KeyCode _key2P;

    Ball _ballObj = null;

    // Use this for initialization
    void Start()
    {
        _controller = GameObject.FindObjectOfType<GameController>();

        var ressouces = GameResources.instance.pendulum;
        var ressouce = ressouces.CreateResource().GetEnumerator();

        // PendulumResouces の中身
        // 0 : 盾
        // 1 : 青盾
        // 2 : 赤盾
        // 3 : ボール
        // 4 : プレイヤーのHPUI

        List<GameObject> list = new List<GameObject>();

        while (ressouce.MoveNext())
        {
            list.Add(ressouce.Current);
        }

        // 盾生成
        // 途中にある ressouce.MoveNext(); は上記の配列番号を変えるために書いている
        var player = GetComponentInParent<ARModel>();
        var deviceMgr = GameObject.FindObjectOfType<ARDeviceManager>();

        var newShield1P = list[2];
        newShield1P.transform.rotation = deviceMgr.player1.gameObject.transform.rotation;
        newShield1P.transform.eulerAngles = deviceMgr.player1.gameObject.transform.transform.eulerAngles;
        newShield1P.transform.parent = deviceMgr.player1.gameObject.transform;
        newShield1P.transform.position = deviceMgr.player1.gameObject.transform.position;
        newShield1P.name = list[2].name;
        newShield1P.transform.Translate(0.0f, 10.0f, 65.0f);
        newShield1P.transform.localScale = Vector3.one;
        shield1P = newShield1P.GetComponent<Shield>();
        shield1P.gameManager = gameManager;
        var par1P = deviceMgr.player1.gameObject.GetComponentInChildren<RandomBullet>().gameObject.GetComponentInChildren<CharacterData>();
        shield1P.defenseParmater = par1P.getCharacterData.defense;

        var newShield2P = list[1];
        newShield2P.transform.rotation = deviceMgr.player2.gameObject.transform.rotation;
        newShield2P.transform.eulerAngles = deviceMgr.player2.gameObject.transform.transform.eulerAngles;
        newShield2P.transform.parent = deviceMgr.player2.gameObject.transform;
        newShield2P.transform.position = deviceMgr.player2.gameObject.transform.position;
        newShield2P.name = list[1].name;
        newShield2P.transform.Translate(0.0f, 10.0f, 65.0f);
        newShield2P.transform.localScale = Vector3.one;
        shield2P = newShield2P.GetComponent<Shield>();
        shield2P.gameManager = gameManager;
        var par2P = deviceMgr.player2.gameObject.GetComponentInChildren<RandomBullet>().gameObject.GetComponentInChildren<CharacterData>();
        shield2P.defenseParmater = par2P.getCharacterData.defense;

        // ボール生成
        var ball = list[3];
        ball.transform.position = Vector3.forward * 700.0f;
        ball.name = _ballName;
        ball.GetComponent<Ball>().gameController = _controller;
        ball.GetComponent<Ball>().manager = deviceMgr;
        ball.GetComponent<Ball>().gameManager = gameManager;
        shield1P.ballObj = ball;
        shield2P.ballObj = ball;
        _ballObj = ball.GetComponent<Ball>();

        // UI生成
        ressouce.MoveNext();
        var ui = list[4];
        ui.GetComponentInChildren<PlayersHPUI>().ball = ball.GetComponent<Ball>();
        //ui.transform.parent = GameObject.Find("GameUI").transform;
        ui.GetComponentInChildren<RectTransform>().localPosition = Vector3.zero;
        ui.GetComponentInChildren<RectTransform>().localScale = Vector3.one;

        // (説明適当)
        gameRule = "飛んでくるボールをタイミングよく跳ね返して相手の盾を壊しましょう。";

        var key1P = _controller.player1.GetEnumerator();
        var key2P = _controller.player2.GetEnumerator();
        key1P.MoveNext();
        key2P.MoveNext();

        _key1P = key1P.Current;
        _key2P = key2P.Current;
    }

    public override void Action()
    {

        if (Input.GetKeyDown(_key1P)) {
            shield1P.PushOn();
        }

        if (Input.GetKeyDown(_key2P))
        {
            shield2P.PushOn();
        }

    }

    public override Transform GetWinner()
    {
        //if (player1 == null || player2 == null) { return null; }

        // 盾がなくなったら負けなので勝ちである相手の transform を返す
        if(shield1P.isDeath)
        {
            return gameManager.player2.transform;
        }
        else if (shield2P.isDeath)
        {
            return gameManager.player1.transform;
        }
        return null;
    }

    public override bool IsFinish()
    {
        // 盾がなくなったら終了
        var isFinish = shield1P.isDeath || shield2P.isDeath;
        if (isFinish)
        {
            var deviceMgr = gameManager.arManager;
            if (shield1P.isDeath)
            {
                var vector = deviceMgr.player1.transform.eulerAngles;
                deviceMgr.player1.costume.AddForce(vector.normalized * 10000);
                //gameManager.referee.textBox.text = "スピードレベル\n" + _ballObj.speedLevel + "\n2Pの勝ち!!";
            }
            else if (shield2P.isDeath)
            {
                var vector = deviceMgr.player2.transform.eulerAngles;
                deviceMgr.player2.costume.AddForce(vector.normalized * 10000);
                //gameManager.referee.textBox.text = "スピードレベル\n" + _ballObj.speedLevel + "\n1Pの勝ち!!";
            }
        }
        return isFinish;  
    }
}
