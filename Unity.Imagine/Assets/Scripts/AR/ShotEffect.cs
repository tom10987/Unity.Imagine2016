
using UnityEngine;

public class ShotEffect : MonoBehaviour {

  [SerializeField]
  ParticleSystem _particle = null;

  void Start() { _particle.Play(); }

  void FixedUpdate() { if (!_particle.isPlaying) { Destroy(gameObject); } }
}
