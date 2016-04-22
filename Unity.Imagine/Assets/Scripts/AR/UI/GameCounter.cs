
using UnityEngine;
using UnityEngine.UI;

public class GameCounter : MonoBehaviour {

  [SerializeField]
  Image _image = null;

  [SerializeField]
  Sprite[] _sprites = null;

  void Start() { _image.enabled = false; }

  /// <summary> 隠したり、表示したり </summary>
  public void Visible() { _image.enabled = !_image.enabled; }

  /// <summary> カウンタ更新 </summary>
  public void UpdateCount(int count) {
    var index = Mathf.Clamp(count, 0, _sprites.Length - 1);
    _image.sprite = _sprites[index];
  }
}
