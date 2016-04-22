
using UnityEngine;

public enum GameType { Speed, Power, Defense, }

public static class GameMode
{
  static GameMode() { type = GameType.Speed; }
  public static GameType type { get; set; }

  /// <summary> ミニゲーム機能の初期化 </summary>
  /// <typeparam name="T"> <see cref="AbstractGame"/> の派生クラス </typeparam>
  public static AbstractGame Create<T>(GameManager manager) where T : AbstractGame
  {
    var instance = new GameObject("Game Instance").AddComponent<T>();
    instance.gameManager = manager;
    return instance;
  }
}
