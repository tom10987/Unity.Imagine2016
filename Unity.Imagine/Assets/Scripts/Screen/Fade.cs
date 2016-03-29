
using UnityEngine;
using System;
using System.Collections;

public class Fade : Effect {
  public Fade(float time) : base() { _sequenceTime = (time > 0f) ? time * 0.5f : 1f; }

  enum State { FadeOut, FadeIn, Update, }
  State _state = State.Update;
  float _sequenceTime = 0f;

  public override bool IsPlaying() { return _state != State.Update; }

  public override IEnumerator Sequence(Action action) {
    _state = State.FadeOut;
    yield return UpdateSequence(time => time < _sequenceTime, 0f, 1);
    action();
    yield return UpdateSequence(time => time > 0f, _sequenceTime, -1);
  }

  IEnumerator UpdateSequence(Func<float, bool> predicate, float time, int sign) {
    while (predicate(time)) {
      time += Time.deltaTime * sign;
      ImageUpdate(time);
      yield return null;
    }
    if (_state < State.Update) { ++_state; }
  }

  void ImageUpdate(float time) {
    sequencer.image.color = Color.black * (time / _sequenceTime);
  }
}
