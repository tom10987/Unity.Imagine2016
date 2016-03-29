
using UnityEngine;
using UnityEngine.UI;

// TIPS: ARModel クラスから呼び出す
public class Score : MonoBehaviour {

  [SerializeField]
  Text _scoreText = null;

  int _count = 0;
  public int count { get { return _count; } }

  /// <summary> スコアを増やす </summary>
  public void CountUp() { _scoreText.text = (++_count).ToString(); }

  /// <summary> スコア表示を隠す </summary>
  public void HideCount() { _scoreText.text = "???"; }
}
