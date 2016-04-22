
using UnityEngine;
using System.Collections;

//------------------------------------------------------------
// NOTICE:
// １度だけ再生、完了したら自身を削除するエフェクト
//
//------------------------------------------------------------

public class OneTimeEffect : MonoBehaviour
{
  [SerializeField]
  ParticleSystem _particle = null;
  /// <summary> パーティクル実行中かどうか </summary>
  public bool isPlaying { get { return _particle.isPlaying; } }

  void Start() { StartCoroutine(PlayEffect()); }

  // TIPS: エフェクト実行
  IEnumerator PlayEffect()
  {
    _particle.Play();
    while (_particle.isPlaying) { yield return null; }

    // TIPS: エフェクト再生が終わったら自身の GameObject を削除
    Destroy(gameObject);
  }
}
