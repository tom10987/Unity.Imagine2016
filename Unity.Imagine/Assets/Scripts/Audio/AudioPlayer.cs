
using UnityEngine;
using System.Collections;

//------------------------------------------------------------
// NOTICE:
// SourceObject が管理している AudioSource に対して再生、停止の命令を行う
//
//------------------------------------------------------------
// TIPS:
// 1: manageMode (SourceManageMode) について
//
// Additive は SourceObject の所有権を AudioManager 側に委ねます
// Control は AudioSource の追加を行いません
// 追加、管理どちらも行う場合は、Full を指定してください
//
// 2: autoRelease (SourceObject.AutoRelease()) について
//
// 再生を完了したときに、自動で SourceObject を削除します
// 
// ループ中の場合、再生中の AudioSource を手動で取得したうえで停止するか、
// ループ設定を解除するまで SourceObject の削除が行われないことに注意
//
//------------------------------------------------------------

public class AudioPlayer : MonoBehaviour
{

  /// <summary> <see cref="SourceObject"/> の管理方法の一覧 </summary>
  public enum SourceManageMode
  {
    /// <summary> <see cref="SourceObject"/> の管理を手動で行う </summary>
    Manual,
    /// <summary> <see cref="SourceObject"/> に
    /// 再生可能な <see cref="AudioSource"/> がなければ自動で追加する
    /// <para> 管理は自動化しない </para></summary>
    Additive,
    /// <summary> 生成した <see cref="SourceObject"/> の管理を放棄
    /// <para> <see cref="AudioSource"/> の自動追加はしない </para></summary>
    Release,
    /// <summary> <see cref="Additive"/>、<see cref="Release"/> の全てを実行 </summary>
    FullAuto,
  }

  [SerializeField]
  [Tooltip("SourceObject の管理方法を指定")]
  SourceManageMode _manageMode = SourceManageMode.Manual;

  /// <summary> <see cref="SourceObject"/> の管理方法を指定 </summary>
  public SourceManageMode manageMode
  {
    get { return _manageMode; }
    set { _manageMode = value; }
  }

  bool isAdditive { get { return ((int)_manageMode % 2) > 0; } }
  bool isRelease { get { return _manageMode > SourceManageMode.Additive; } }

  [SerializeField]
  [Tooltip("再生が終了した SourceObject を自動的に開放する")]
  bool _autoRelease = false;

  /// <summary> 再生終了時に自動で
  /// <see cref="SourceObject"/> を解放するか指定 </summary>
  public bool autoRelease
  {
    get { return _autoRelease; }
    set { _autoRelease = value; AutoRelease(); }
  }

  AudioClipTable table { get { return AudioClipTable.instance; } }

  // TIPS: コルーチン動作中のフラグ
  bool _isAutoRelease = false;

  // TIPS: リンクされた SourceObject のインスタンス
  SourceObject _sourceObject = null;

  /// <summary> 自身に関連付けられた <see cref="SourceObject"/> の
  /// 所有権が自身にあれば true を返す </summary>
  public bool IsOwnership() { return _sourceObject.transform.parent == transform; }

  /// <summary> ループ以外で、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlaying() { return _sourceObject.IsPlaying(); }

  /// <summary> ループ中も含めて、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlayingWithLoop() { return _sourceObject.IsPlayingWithLoop(); }

  /// <summary> 再生していない <see cref="AudioSource"/> があれば true を返す </summary>
  public bool ExistStopSource() { return _sourceObject.ExistStopSource(); }

  /// <summary> ループ設定の <see cref="AudioSource"/> があれば true を返す </summary>
  public bool ExistLoopSource() { return _sourceObject.ExistLoopSource(); }

  // TIPS: Bind() 用、SourceObject 取得メソッド
  SourceObject GetObject()
  {
    var source = table.GetSourceObject();
    return (source == null) ? SourceObject.Create() : source;
  }

  /// <summary> <see cref="SourceObject"/> を割り当てる </summary>
  public void Bind()
  {
    if (_sourceObject != null) { UnBind(); }
    var source = GetObject();
    source.transform.SetParent(isRelease ? table.transform : transform);
    _sourceObject = source;
  }

  /// <summary> <see cref="SourceObject"/> を解放する </summary>
  public void UnBind()
  {
    Destroy(_sourceObject.gameObject);
    _sourceObject = null;
  }

  /// <summary> リンクされた <see cref="SourceObject"/> に
  /// <see cref="AudioSource"/> を追加、取得する </summary>
  public AudioSource AddSource() { return _sourceObject.AddSource(); }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する </summary>
  /// <param name="volume"> 音量を指定 (0.0 ~ 1.0) </param>
  /// <param name="isLoop"> true = ループ再生を許可 </param>
  public void Play(int index, float volume, bool isLoop)
  {
    if (_sourceObject == null) { Bind(); }

    // TIPS: AudioSource の取得を試みる
    AudioSource source = null;
    var success = _sourceObject.GetSource(out source);
    if (!success && isAdditive) { source = AddSource(); }

    // TIPS: AudioSource が取得できなければスキップ
    if (source == null) { return; }

    source.clip = table.GetClip(index);
    source.volume = volume;
    source.loop = isLoop;
    source.Play();

    AutoRelease();
  }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する
  /// （音量指定可能、ループなし） </summary>
  /// <param name="volume"> 音量を指定 (0.0 ~ 1.0) </param>
  public void Play(int index, float volume) { Play(index, volume, false); }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する
  /// （音量最大、ループ指定可能）</summary>
  /// <param name="isLoop"> true = ループ再生を許可 </param>
  public void Play(int index, bool isLoop) { Play(index, 1f, isLoop); }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を使って再生する
  /// （音量最大、ループなし）</summary>
  public void Play(int index) { Play(index, 1f, false); }

  /// <summary> 登録済みの <see cref="AudioClip"/> を使って全て再生する </summary>
  public void AllPlay() { _sourceObject.AllPlay(); }

  /// <summary> 再生中の <see cref="AudioSource"/> を全て停止する </summary>
  public void Stop() { _sourceObject.AllStop(); }

  /// <summary> ループ再生以外の <see cref="AudioSource"/> を停止する </summary>
  public void StopSE() { _sourceObject.StopWithoutLoop(); }

  void AutoRelease() { if (!_isAutoRelease) StartCoroutine(RefreshSource()); }

  // TIPS: 未使用になった AudioSource を自動的に開放する
  IEnumerator RefreshSource()
  {
    _isAutoRelease = true;

    while (_autoRelease)
    {
      if (_sourceObject.ExistStopSource()) { _sourceObject.Refresh(); }
      yield return null;
    }

    _isAutoRelease = false;
  }
}
