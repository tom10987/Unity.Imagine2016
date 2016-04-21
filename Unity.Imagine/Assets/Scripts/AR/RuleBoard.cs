
using UnityEngine;
using UnityEngine.UI;

public class RuleBoard : MonoBehaviour
{
  [SerializeField]
  Text _textBox = null;

  /// <summary> ゲームルールの文字列を入力 </summary>
  public void SetRuleText(string text) { _textBox.text = text; }

  /// <summary> キャンバスを削除 </summary>
  public void DeleteObject() { Destroy(gameObject); }
}
