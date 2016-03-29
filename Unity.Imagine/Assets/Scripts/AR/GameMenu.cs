
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

  [SerializeField]
  Button _start = null;
  public Button start { get { return _start; } }

  [SerializeField]
  Button _back = null;
  public Button back { get { return _back; } }

  [SerializeField]
  Button _hint = null;
  public Button hint { get { return _hint; } }

  [SerializeField]
  CanvasGroup _group = null;
  public CanvasGroup group { get { return _group; } }

  [SerializeField]
  Score _player1 = null;
  public Score player1 { get { return _player1; } }

  [SerializeField]
  Score _player2 = null;
  public Score player2 { get { return _player2; } }

  [SerializeField]
  GameHintCanvas _hintCanvas = null;

  public void OnCreateHintCanvas() {
    var canvas = Instantiate(_hintCanvas);
    canvas.menu = _group;
    _group.interactable = false;
  }
}
