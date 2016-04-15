using UnityEngine;
using System.Collections;
using System;

public class Pendulum : AbstractGame
{
    CharacterData parameter { get; set; }
    ARDeviceManager manager { get; set; }
    GameController controller { get; set; }

    Shield shield { get; set; }

    // ボールとUIを１つだけ生成するために使う
    // １つしかいらないものを２つ生成しないため
    static bool _OneCreateFlag = false;

    public static string _ballName = "Ball";

    void Awake()
    {
        _OneCreateFlag = true;
    }

    // Use this for initialization
    void Start()
    {
        parameter = GetComponentInChildren<CharacterData>();
        manager = GetComponentInParent<ARDeviceManager>();
        controller = GetComponentInParent<GameController>();

        var ressouces = GameResources.instance.pendulum;
        var ressouce = ressouces.CreateResource().GetEnumerator();

        // PendulumResouces の中身
        // 0 : 盾
        // 1 : 青盾
        // 2 : 赤盾
        // 3 : ボール
        // 4 : プレイヤーのHPUI

        // 盾生成
        // 途中にある ressouce.MoveNext(); は上記の配列番号を変えるために書いている
        var player = GetComponentInParent<ARModel>();
        if (player1 == player)
        {
            ressouce.MoveNext();
            ressouce.MoveNext();
            ressouce.Current.transform.rotation = transform.rotation;
            ressouce.Current.transform.eulerAngles = transform.parent.transform.eulerAngles;
            ressouce.Current.transform.Translate(new Vector3(0.0f, -40.0f, 10.0f));
            ressouce.Current.transform.parent = transform.parent;
            ressouce.Current.name = ressouce.Current.name;
            shield = ressouce.Current.GetComponent<Shield>();
            shield.defenseParmater = parameter.getCharacterData.defense;
            ressouce.MoveNext();
        }
        else if (player2 == player)
        {
            ressouce.MoveNext();
            ressouce.MoveNext();
            ressouce.MoveNext();
            ressouce.Current.transform.rotation = transform.rotation;
            ressouce.Current.transform.eulerAngles = transform.parent.transform.eulerAngles;
            ressouce.Current.transform.Translate(new Vector3(0.0f, -40.0f, 10.0f));
            ressouce.Current.transform.parent = transform.parent;
            ressouce.Current.name = ressouce.Current.name;
            shield = ressouce.Current.GetComponent<Shield>();
            shield.defenseParmater = parameter.getCharacterData.defense;

        }
        else
        {
            ressouce.MoveNext();
            ressouce.Current.transform.rotation = transform.rotation;
            ressouce.Current.transform.eulerAngles = transform.parent.transform.eulerAngles;
            ressouce.Current.transform.Translate(new Vector3(0.0f, -40.0f, 10.0f));
            ressouce.Current.transform.parent = transform.parent;
            ressouce.Current.name = ressouce.Current.name;
            shield = ressouce.Current.GetComponent<Shield>();
            shield.defenseParmater = parameter.getCharacterData.defense;
            ressouce.MoveNext();
            ressouce.MoveNext();
        }

        if (_OneCreateFlag)
        {
            // ボール生成
            ressouce.MoveNext();
            var ball = ressouce.Current;
            ressouce.Current.transform.position = Vector3.forward * 700.0f;
            ressouce.Current.name = _ballName;
            ressouce.Current.GetComponent<Ball>().manager = manager;

            // UI生成
            ressouce.MoveNext();
            var ui = ressouce.Current;
            ui.GetComponent<PlayersHPUI>().ball = ball.GetComponent<Ball>();
            ui.transform.parent = GameObject.Find("GameUI").transform;
            ui.GetComponent<RectTransform>().localPosition = Vector3.zero;
            ui.GetComponent<RectTransform>().localScale = Vector3.one;
            _OneCreateFlag = false;
        }

        //foreach (var res in ressouces.CreateResource())
        //{
        //    res.transform.position = transform.position;
        //    res.transform.rotation = transform.rotation;
        //    res.transform.eulerAngles = transform.parent.transform.eulerAngles;
        //    res.transform.Translate(new Vector3(0.0f, -40.0f, 10.0f));
        //    res.transform.parent = transform.parent;
        //    res.name = res.name;
        //    shield = res.GetComponent<Shield>();
        //    
        //    break;
        //}

        // (説明適当)
        gameRule = "飛んでくるボールをタイミングよく跳ね返して相手の盾を壊しましょう。";
    }

    // Update is called once per frame
    void Update()
    {
        // デバック用に Update() で動かしている
        var a = controller.player1.GetEnumerator();
        a.MoveNext();
        if (Input.GetKeyDown(a.Current))
        {
            Action();
        }
    }

    public override void Action()
    {
        shield.PushOn();
    }

    public override Transform GetWinner()
    {
        if (player1 == null || player2 == null) { return null; }

        // 盾がなくなったら負けなので勝ちである相手の transform を返す
        if(player1.GetComponent<Pendulum>().shield == null)
        {
            return player2.transform;
        }
        else if (player2.GetComponent<Pendulum>().shield == null)
        {
            return player1.transform;
        }
        return null;
    }

    public override bool IsFinish()
    {
        // 盾がなくなったら終了
        if (shield != null) { return false; }
        return true;  
    }
}
