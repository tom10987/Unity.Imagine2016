
using UnityEngine;

public class BarrageGame : AbstractGame {

  public override void Action() {
  }

  public override bool IsFinish() {
    return false;
  }

  public override bool IsDraw() {
    return base.IsDraw();
  }

  public override Transform GetWinner() {
    return null;
  }

  void Start() {
    gameRule = @"<color=red>ABCD</color>";
  }
}
