
using UnityEngine;

public class GameResourceTest : MonoBehaviour {

  void Start() {
    var barrage = GameResources.instance.barrage;
    foreach (var res in barrage.CreateResource()) {
      res.transform.SetParent(transform);
    }
  }
}
