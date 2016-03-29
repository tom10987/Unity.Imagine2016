
using UnityEngine;
using System.Linq;

public class FireWorksEffect : MonoBehaviour {

  [SerializeField]
  ParticleSystem _own = null;
  public ParticleSystem particle { get { return _own; } }

  public bool isPlaying { get { return _own.isPlaying; } }

  [SerializeField]
  ParticleSystem[] _particles = null;

  /// <summary> パーティクルが終了したら true を返す </summary>
  public bool isFinish { get { return _particles.All(p => !p.isPlaying); } }
}
