
using UnityEngine;

public class ResultLight : MonoBehaviour {

  [SerializeField]
  Light _light = null;

  void Start() { _light.gameObject.SetActive(false); }

  public void LightUp() { _light.gameObject.SetActive(true); }
}
