using UnityEngine;
using System.Collections;

public class GamePlayManager : MonoBehaviour {

    public bool gamePlayFlag { get; set; }
    public void GamePlayOn()
    {
        gamePlayFlag = true;
    }

    void Start()
    {
        gamePlayFlag = false;
    }
}
