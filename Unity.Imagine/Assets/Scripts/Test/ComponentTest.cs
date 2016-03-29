
using UnityEngine;

public class ComponentTest : MonoBehaviour {

  void Start() {
    var component = gameObject.AddComponent<AudioSource>();
    Destroy(component);
  }
}
