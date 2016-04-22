
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// 生成されたら残り続けるゲーム用のエフェクト
//
//------------------------------------------------------------

public class EndlessEffect : MonoBehaviour
{
  [SerializeField]
  ParticleSystem _particle = null;
  public ParticleSystem particle { get { return _particle; } }
}
