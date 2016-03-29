using UnityEngine;
using System.Collections.Generic;

public class BlowOff : MonoBehaviour
{

    [SerializeField]
    GameObject _victoryBullet;

    [SerializeField, Range(1f, 50f)]
    float _velocity = 15f;

    ScoreCompare _scoreCompare;

    KeyAction _actionManager;


    void Start()
    {
        _scoreCompare = FindObjectOfType<ScoreCompare>();
        _actionManager = FindObjectOfType<KeyAction>();
    }

    void Update(){}

    void OnTriggerEnter(Collider collision)
    {


        if (_scoreCompare.getDisplayScore == true)
        {

            List<GameObject> _playerList = new List<GameObject>();
            _playerList = _actionManager.GetPlayers();

            if (_victoryBullet.name == collision.name)
            {

                var direction = Vector3.up;
                var force = direction * _velocity;

                if (gameObject.transform.parent.gameObject == _playerList[0] && _scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player2)
                {

                    gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
                }
                else
                if (gameObject.transform.parent.gameObject == _playerList[1] && _scoreCompare.getWinPlayer == ScoreCompare.WinPlayer.Player1)
                {

                    gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
                }
            }

        }
    }
}
