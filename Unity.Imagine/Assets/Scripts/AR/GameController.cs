
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {

  [SerializeField]
  KeyCode[] _player1 = { KeyCode.S, };
  public IEnumerable<KeyCode> player1 { get { return _player1; } }

  [SerializeField]
  KeyCode[] _player2 = { KeyCode.K, };
  public IEnumerable<KeyCode> player2 { get { return _player2; } }

  void Awake() {
    System.Action<KeyCode[], KeyCode> Init = (keys, key) => {
      if (keys != null) { return; }
      keys = new KeyCode[] { key, };
    };
    Init(_player1, KeyCode.S);
    Init(_player2, KeyCode.K);
  }

  /// <summary> Player1 に割り当てられたキーが入力されたら true を返す </summary>
  public bool IsPlayer1KeyDown() {
    return _player1.Any(key => Input.GetKeyDown(key));
  }

  /// <summary> Player2 に割り当てられたキーが入力されたら true を返す </summary>
  public bool IsPlayer2KeyDown() {
    return _player2.Any(key => Input.GetKeyDown(key));
  }
}
