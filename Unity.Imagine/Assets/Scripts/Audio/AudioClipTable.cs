
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// NOTICE:
// AudioClip の管理を行う
//
// Register() メソッドによって登録された SourceObject は
// シーンが変わっても削除されない
//
//------------------------------------------------------------

public class AudioClipTable : SingletonBehaviour<AudioClipTable> {

  [SerializeField]
  AudioClip[] _clips = null;

  /// <summary> 登録された <see cref="AudioClip"/> を全て取得 </summary>
  public IEnumerable<AudioClip> clips { get { return _clips; } }

  /// <summary> 指定した ID の <see cref="AudioClip"/> を取得 </summary>
  public AudioClip GetClip(int index) { return _clips[Clamp(index)]; }

  // TIPS: index を _clips の範囲内に収まるように補正する
  int Clamp(int index) { return Mathf.Clamp(index, 0, _clips.Length - 1); }

  /// <summary> 管理下にある <see cref="SourceObject"/> を全て取得 </summary>
  public IEnumerable<SourceObject> GetSourceObjects() {
    var sources = this.GetOnlyChildren<SourceObject>();
    foreach (var source in sources) { yield return source; }
  }

  /// <summary> 未使用の <see cref="SourceObject"/> を取得 </summary>
  public SourceObject GetSourceObject() {
    return GetSourceObjects().FirstOrDefault(src => !src.IsPlayingWithLoop());
  }

  /// <summary> 未使用の <see cref="SourceObject"/> があれば true を返す </summary>
  public bool ExistFreeObject() {
    return GetSourceObjects().Any(src => !src.IsPlayingWithLoop());
  }

  /// <summary> 未使用の <see cref="SourceObject"/> を全て削除する </summary>
  public void DestroyFreeObjects() {
    var sources = GetSourceObjects().Where(src => !src.IsPlayingWithLoop());
    foreach (var source in sources) { Destroy(source.gameObject); }
  }

  void Start() { DontDestroyOnLoad(gameObject); }
}
