using UnityEngine;
using System;
using System.Collections;

/// <summary>
///　リザルトの演出をする機能
/// </summary>
public class ResultEffector : MonoBehaviour
{

    GameObject _winEffect = null;
    GameObject _loseEffect = null;

    /// <summary>
    /// 勝利時に使用する関数
    /// </summary>
    /// <returns></returns>
    public void Win()
    {
        var effect = Instantiate(_winEffect);
        effect.transform.SetParent(transform, false);
    }

    /// <summary>
    /// 敗北時に使用する関数
    /// </summary>
    /// <returns></returns>
    public void Lose()
    {
        var effect = Instantiate(_loseEffect);
        effect.transform.SetParent(transform, false);
    }

    void Start()
    {
        const string RESULT_PATH = "Result/";

        _winEffect = Resources.Load<GameObject>(RESULT_PATH + "PaperParticle");
        _loseEffect = Resources.Load<GameObject>(RESULT_PATH + "Rain/raintest");

        if (_winEffect == null) throw new NullReferenceException("win effect null");
        if (_loseEffect == null) throw new NullReferenceException("lose effect null");
    }
}
