
using UnityEngine;
using UnityEngine.UI;
using System;

//------------------------------------------------------------
// TIPS:
// スクリプトの適用方法について
// 下記の構造を持つ GameObject を作成、スクリプトを追加してください
//
// Canvas
// -> Image
//
// スクリプトを追加するのは Canvas、Image のどちらでもかまいません
// 追加したスクリプトの Image に "Canvas の Image" を登録してください
//
//------------------------------------------------------------

/// <summary> 実行したい処理を与えて画面遷移を行う </summary>
public class ScreenSequencer : SingletonBehaviour<ScreenSequencer> {

  [SerializeField]
  [Tooltip("演出に使用する Canvas の Image を登録")]
  Image _image = null;
  public Image image { get { return _image; } }

  /// <summary> 再生したい <see cref="Effect"/> クラスを new で設定 </summary>
  public Effect effect { private get; set; }

  /// <summary> <see cref="Effect"/> が登録されていれば true を返す </summary>
  public bool isEffectExist { get { return effect != null; } }

  /// <summary> 演出実行中なら true を返す </summary>
  public bool isEffectPlaying { get { return (isEffectExist ? effect.IsPlaying() : false); } }

  void Start() { DontDestroyOnLoad(gameObject); }

  /// <summary> 画面遷移を開始 </summary>
  public void SequenceStart(Action action) {
    if (effect.IsPlaying()) { return; }
    StartCoroutine(effect.Sequence(action));
  }

  /// <summary> 実行したい <see cref="Effect"/> を指定、画面遷移を開始 </summary>
  public void SequenceStart(Action action, Effect effect) {
    this.effect = effect;
    SequenceStart(action);
  }
}
