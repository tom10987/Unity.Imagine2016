
using UnityEngine;

public class GameEffect : MonoBehaviour
{
  [SerializeField]
  ParticleSystem _particle = null;
  public ParticleSystem particle { get { return _particle; } }

  /// <summary> パーティクル実行中なら true を返す </summary>
  public bool isPlaying { get { return _particle.isPlaying; } }

  /// <summary> パーティクルオブジェクトが残っていれば true を返す </summary>
  public bool existObject { get { return _particle.particleCount > 0; } }
}
