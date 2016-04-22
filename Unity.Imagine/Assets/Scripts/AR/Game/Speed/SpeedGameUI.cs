
using UnityEngine;
using UnityEngine.UI;

//------------------------------------------------------------
// NOTICE:
// スピードのゲーム用、UI
//
//------------------------------------------------------------

public class SpeedGameUI : MonoBehaviour
{
  [SerializeField]
  CanvasGroup _group = null;
  public CanvasGroup group { get { return _group; } }

  [SerializeField]
  Text _player1 = null;
  /// <summary> プレイヤー１のスコアボード </summary>
  public Text p1Score { get { return _player1; } }

  [SerializeField]
  Text _player2 = null;
  /// <summary> プレイヤー２のスコアボード </summary>
  public Text p2Score { get { return _player2; } }
}
