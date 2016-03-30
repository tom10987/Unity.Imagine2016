
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameController : SingletonBehaviour<GameController> {

  [SerializeField]
  KeyCode[] _player1 = { KeyCode.S, };
  public IEnumerable<KeyCode> player1 { get { return _player1; } }

  [SerializeField]
  KeyCode[] _player2 = { KeyCode.K, };
  public IEnumerable<KeyCode> player2 { get { return _player2; } }

  public enum Key { Player1, Player2, }

  void Start() {
    System.Action<KeyCode[], KeyCode> Init = (keys, key) => {
      if (keys != null) { return; }
      keys = new KeyCode[] { key, };
    };
    Init(_player1, KeyCode.S);
    Init(_player2, KeyCode.K);
  }
}

public static class KeyCodeExtension {

  /// <summary> キーが押されたら true を返す </summary>
  public static bool IsPush(this IEnumerable<KeyCode> player) {
    return player.Any(key => Input.GetKeyDown(key));
  }

  /// <summary> キーが押され続けている間 true を返す </summary>
  public static bool IsPress(this IEnumerable<KeyCode> player) {
    return player.Any(key => Input.GetKey(key));
  }

  /// <summary> キーが離されたら true を返す </summary>
  public static bool IsPull(this IEnumerable<KeyCode> player) {
    return player.Any(key => Input.GetKeyUp(key));
  }
}
