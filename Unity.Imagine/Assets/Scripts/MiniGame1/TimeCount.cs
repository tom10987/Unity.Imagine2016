
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour {

  [SerializeField]
  Text _board = null;
  public Text board { get { return _board; } }

  [SerializeField, Range(5f, 15f)]
  float _timeCount = 10;
  public float timeCount { get { return _timeCount; } }

  /// <summary> ゲームの残り時間 </summary>
  public float time { get; set; }

  /// <summary> ゲームの残り時間 </summary>
  public int timeToInt { get { return Mathf.RoundToInt(time); } }

  void Start() { TimeReset(); }

  public void TimeReset() { time = _timeCount; }

  /// <summary> 残り時間を減らす </summary>
  public void UpdateTimeCount() { if (time > 0f) time -= Time.deltaTime; }
}
