
using UnityEngine;

public class OneTimeEffect : MonoBehaviour {

  [SerializeField]
  ParticleSystem _particle = null;

  void Start() { _particle.Play(); }

  void FixedUpdate() { if (!_particle.isPlaying) { Destroy(gameObject); } }
}
