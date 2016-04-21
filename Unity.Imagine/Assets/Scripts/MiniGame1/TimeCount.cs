
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
  /// <summary> レフェリーのボードを登録 </summary>
  public Text board { get; set; }

  [SerializeField, Range(5f, 15f)]
  float _timeLimit = 10;
  public float timeLimit { get { return _timeLimit; } }

  /// <summary> ゲームの残り時間 </summary>
  public float time { get; set; }

  /// <summary> ゲームの残り時間（int 型に変換） </summary>
  public int timeToInt { get { return Mathf.RoundToInt(time); } }

  void Start() { TimeReset(); }

  public void TimeReset() { time = _timeLimit; }

  /// <summary> 残り時間を減らす </summary>
  public void UpdateTimeCount() { if (time > 0f) time -= Time.deltaTime; }
}
