
using UnityEngine;

public abstract class AbstractGame : MonoBehaviour {
  public abstract void Action();
  public abstract bool IsFinish();
  public abstract Transform GetWinner();

  public string gameRule { get; protected set; }

  public ARModel player1 { get; protected set; }
  public ARModel player2 { get; protected set; }

  public static void AddComponent<T>(ARDeviceManager manager)
    where T : AbstractGame {
    manager.player1.gameObject.AddComponent<T>();
    manager.player2.gameObject.AddComponent<T>();
  }
}
