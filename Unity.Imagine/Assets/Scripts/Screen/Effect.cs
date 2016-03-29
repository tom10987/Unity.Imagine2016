
using System;
using System.Collections;

public abstract class Effect {
  protected static ScreenSequencer sequencer { get { return ScreenSequencer.instance; } }

  public abstract bool IsPlaying();
  public abstract IEnumerator Sequence(Action action);

  /// <summary> <see cref="UnityEngine.MonoBehaviour.StartCoroutine(IEnumerator)"/> の代替メソッド </summary>
  protected IEnumerator Coroutine(IEnumerator iterator) { while (iterator.MoveNext()) { yield return null; } }
}
