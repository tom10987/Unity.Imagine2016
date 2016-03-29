
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public enum GameScene {
  Title,
  Menu,
  Create,
  Printer,
  Game,

  Max, None = -1,
}

public static class SceneExtension {

  /// <summary> シーンを切り替える </summary>
  public static void ChangeScene(this GameScene scene) {
    SceneManager.LoadScene(scene.ToString());
  }

  /// <summary> 実行中のシーンを全て取得 </summary>
  public static IEnumerable<Scene> GetAllActiveScenes() {
    var count = SceneManager.sceneCount;
    for (var i = 0; i < count; ++i) { yield return SceneManager.GetSceneAt(i); }
  }

  static int max { get { return (int)GameScene.Max; } }

  // TIPS: GameScene の一覧を文字列として取得
  static IEnumerable<string> GetAllGameScenes() {
    return System.Enum.GetNames(typeof(GameScene)).Take(max);
  }

  /// <summary> <see cref="Scene"/> を <see cref="GameScene"/> に変換する </summary>
  public static GameScene ToGameScene(this Scene scene) {
    var result = GetAllGameScenes().FirstOrDefault(name => name == scene.name);
    if (result == null) { result = GameScene.None.ToString(); }
    return result.EnumParse<GameScene>();
  }
}
