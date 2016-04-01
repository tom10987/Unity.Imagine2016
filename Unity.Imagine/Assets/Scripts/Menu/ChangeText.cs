using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class ChangeText : MonoBehaviour
{

    private List<String> _text = new List<string>();
   
    [SerializeField]

    void Start()
    {
        _text.Add("ゲームをえらべるよ\n3つのゲームからえらんでね");
        _text.Add("ひたすらタップ！！\nたまをうちあうゲームだよ\nはやいCuboがとくいだよ!");
        _text.Add("このゲームは\nまだあそべないよ");
        _text.Add("このゲームは\nまだあそべないよ");
        _text.Add("ゲームをじどうでえらぶよ\nでもいまはつかえないよ");
        _text.Add("ゲームをえらんだときに\nどんなCuboが\nつよいのかみれるよ");
        _text.Add("ごめんね！！\nぼくはえらべないよ");
        _text.Add("このゲームに\nきまったよ！！");
        ChangeExplanationText(0);
    }


    public void ChangeExplanationText(int _selectNum)
    {
        this.GetComponent<Text>().text = _text[_selectNum];
    }

    public void ResetExplanationText()
    {
        this.GetComponent<Text>().text = _text[0];
    }
}
