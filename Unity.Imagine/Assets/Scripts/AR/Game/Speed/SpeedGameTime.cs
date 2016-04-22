
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// スピードのゲームの制限時間管理
//
//------------------------------------------------------------

public class SpeedGameTime : MonoBehaviour
{
  [SerializeField, Range(5f, 15f)]
  [Tooltip("制限時間")]
  float _timeLimit = 10f;

  /// <summary> 現在の残り時間 </summary>
  public float currentTime { get; private set; }
  /// <summary> 現在の残り時間（int） </summary>
  public int currentTimeToInt { get { return Mathf.RoundToInt(currentTime); } }
  /// <summary> int の残り時間を文字列として取得 </summary>
  public string currentTimeString { get { return currentTimeToInt.ToString(); } }

  /// <summary> 残り時間がなくなった </summary>
  public bool isFinish { get { return currentTimeToInt == 0; } }

  void Start() { TimeReset(); }

  /// <summary> タイムカウンタのリセット </summary>
  public void TimeReset() { currentTime = _timeLimit; }

  /// <summary> タイムカウンタの更新 </summary>
  public void UpdateTime() { currentTime -= Time.deltaTime; }

  /// <summary> サドンデス時の残り時間に設定 </summary>
  public void SuddenDeathMode() { currentTime *= 0.5f; }
}
