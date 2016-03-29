
using UnityEngine;

public class FinishEffect : MonoBehaviour {

  [SerializeField]
  ParticleSystem _particle = null;
  public ParticleSystem particle { get { return _particle; } }

  public bool existObject { get { return gameObject != null; } }
}
