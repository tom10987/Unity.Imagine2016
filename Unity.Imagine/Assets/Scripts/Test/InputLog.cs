
using UnityEngine;
using UnityEngine.UI;

public class InputLog : MonoBehaviour {

  [SerializeField]
  Text _log = null;

  int _count = 0;

  public void OnPush() { _log.text = (++_count).ToString(); }
}
