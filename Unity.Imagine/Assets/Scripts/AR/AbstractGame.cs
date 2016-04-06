
using UnityEngine;

public abstract class AbstractGame : MonoBehaviour {

  // ゲームの処理
  public abstract void Action();

  // ゲームが終了したとき true を返す
  public abstract bool IsFinish();

  // ゲームが引き分けなら true を返す
  // TIPS: 引き分けにならないゲームは override しなくて大丈夫です
  public virtual bool IsDraw() { return false; }

  // ゲームの勝者
  public abstract Transform GetWinner();

  /// <summary> ゲームルールの説明 </summary>
  public string gameRule { get; protected set; }

  public ARModel player1 { get; protected set; }
  public ARModel player2 { get; protected set; }
}

public static class ARModelExtension {
  public static ARModel CreateGameComponent<T>(this ARModel player)
    where T : AbstractGame {
    player.GameSetup(player.gameObject.AddComponent<T>());
    return player;
  }
}

public enum Result { WinP1, WinP2, Draw, }
