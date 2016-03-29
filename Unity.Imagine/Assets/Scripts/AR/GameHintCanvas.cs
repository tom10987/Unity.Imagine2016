
using UnityEngine;

public class GameHintCanvas : MonoBehaviour {

  public CanvasGroup menu { private get; set; }

  public void OnDelete() {
    menu.interactable = true;
    Destroy(gameObject);
  }
}
