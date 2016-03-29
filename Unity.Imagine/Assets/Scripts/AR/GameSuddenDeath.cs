
using UnityEngine;
using UnityEngine.UI;

public class GameSuddenDeath : MonoBehaviour {

  [SerializeField]
  Image _image = null;

  void Start() { _image.enabled = false; }

  public bool isVisible { get { return _image.enabled; } }

  public void Visible() { _image.enabled = !_image.enabled; }
}
