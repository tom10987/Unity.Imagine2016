
using UnityEngine;
using UnityEngine.UI;

public class GameFinish : MonoBehaviour {

  [SerializeField]
  Image _player1 = null;

  [SerializeField]
  Image _player2 = null;

  [SerializeField]
  Sprite _victory = null;

  [SerializeField]
  Sprite _defeat = null;

  enum Result { P1Win, P2Win, Draw, }

  void Start() {
    // TIPS: 起動時に隠す
    _player1.enabled = false;
    _player2.enabled = false;
  }

  /// <summary> <see cref="Image"/> を表示する </summary>
  public void ActivateImage(int p1score, int p2score) {
    var result = GetResult(p1score, p2score);
    if (result == Result.Draw) { return; }

    _player1.enabled = true;
    _player2.enabled = true;

    _player1.sprite = (result == Result.P1Win ? _victory : _defeat);
    _player2.sprite = (result == Result.P1Win ? _defeat : _victory);
  }

  Result GetResult(int p1, int p2) {
    return p1 > p2 ? Result.P1Win : p1 < p2 ? Result.P2Win : Result.Draw;
  }
}
