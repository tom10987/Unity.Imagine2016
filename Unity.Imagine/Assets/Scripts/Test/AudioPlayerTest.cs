
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayerTest : MonoBehaviour {

  [SerializeField]
  AudioPlayer _player = null;

  [SerializeField]
  string _sceneName = string.Empty;

  int _number = 5;

  public void OnNext() {
    SceneManager.LoadScene(_sceneName);
  }

  public void OnPlay() {
    _player.Play(_number);
  }

  public void OnPlus() {
    if (_number < 12) { ++_number; }
    Debug.Log("number = " + _number);
  }

  public void OnMinus() {
    if (_number > 5) { --_number; }
    Debug.Log("number = " + _number);
  }
}
