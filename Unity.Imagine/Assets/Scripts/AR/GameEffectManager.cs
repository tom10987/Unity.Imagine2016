
using UnityEngine;
using System.Linq;

public class GameEffectManager : MonoBehaviour {

  [SerializeField]
  ParticleSystem _player1effect = null;
  public ParticleSystem p1effect { get { return _player1effect; } }

  [SerializeField]
  ParticleSystem _player2effect = null;
  public ParticleSystem p2effect { get { return _player2effect; } }

  [SerializeField]
  FinishEffect _paper = null;

  [SerializeField, Range(0f, 100f)]
  float _paperOffset = 10f;

  [SerializeField]
  FireWorksEffect _fire = null;

  [SerializeField]
  Vector3 _fireOffset = Vector3.zero;

  [SerializeField, Range(0f, 100f)]
  float _randomRange = 10f;

  /// <summary> 生成済み <see cref="FireWorksEffect"/> があれば true を返す </summary>
  public bool isAliveFireWorks { get { return this.GetOnlyChildren<FireWorksEffect>().Any(); } }

  /// <summary> 花火打ち上げ </summary>
  public FireWorksEffect CreateFireWorks(Transform winner) {
    var fire = Instantiate(_fire);
    fire.transform.SetParent(transform);

    var position = winner.position + _fireOffset;
    var random = Random.insideUnitSphere * _randomRange;
    fire.transform.position = position + random;
    fire.particle.Play();
    return fire;
  }

  /// <summary> 紙吹雪開始 </summary>
  public void ActivatePaper(Transform winner) {
    var paper = Instantiate(_paper);
    paper.transform.position = winner.position + Vector3.up * _paperOffset * 10f;
    paper.particle.loop = true;
    paper.particle.Play();
  }
}
