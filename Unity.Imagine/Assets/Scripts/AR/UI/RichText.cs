
//------------------------------------------------------------
// NOTICE:
// UnityEngine.UI.Text クラスの text プロパティに対して使用する
//
//------------------------------------------------------------

public static class RichText
{
  /// <summary>
  /// <see cref="UnityEngine.UI.Text"/> で使用する、文字列の色一覧
  /// </summary>
  public enum ColorType
  {
    white,
    black,
    red,
    blue,
    green,
    yellow,
    cyan,
  }

  /// <summary> 文字列を指定した色に変換する </summary>
  public static string ToColor(this string text, ColorType color)
  {
    return string.Format("<color={0}>{1}</color>", color.ToString(), text);
  }

  public static string ToSize(this string text, int size)
  {
    return string.Format("<size={0}>{1}</size>", size, text);
  }
}
