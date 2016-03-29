
using System;

public static class SystemExtention {

  // TIPS: 変換処理
  static T Parse<T>(string value, bool ignoreCase) { return (T)Enum.Parse(typeof(T), value, ignoreCase); }

  /// <summary> 列挙型に変換 </summary>
  /// <typeparam name="T"> 列挙型を指定 </typeparam>
  /// <param name="ignoreCase"> true = 文字列中の大文字と小文字を区別する </param>
  public static T EnumParse<T>(this string value, bool ignoreCase) { return Parse<T>(value, ignoreCase); }

  /// <summary> 列挙型に変換（大文字と小文字を区別しない） </summary>
  /// <typeparam name="T"> 列挙型を指定 </typeparam>
  public static T EnumParse<T>(this string value) { return Parse<T>(value, false); }
}
