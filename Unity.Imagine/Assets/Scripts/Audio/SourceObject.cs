
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// NOTICE:
// 自身の GameObject に追加された AudioSource コンポーネントの管理を行う
//
//------------------------------------------------------------
// TIPS:
// 基本的に AudioManager.CreateSource() を使用してインスタンスを生成します
//
// GameObject に直接スクリプトを追加したり、
// SourceObject.Create() でもインスタンス生成は可能ですが、
// AudioManager には登録されないため、独自に管理する必要があります
//
// 逆に、AudioManager を必要としない場合は上記の方法で
// インスタンス生成、インスタンスの操作が可能です
//
// 外部から AudioManager の管理下に置きたい場合は、
// AudioManager.RegisterSource() の引数にインスタンスを渡してください
//
//------------------------------------------------------------

public class SourceObject : MonoBehaviour {

  /// <summary> 新規の <see cref="SourceObject"/> を生成する </summary>
  public static SourceObject Create() {
    var source = new GameObject("Source").AddComponent<SourceObject>();
    source.AddSource();
    return source;
  }

  /// <summary> 新規の <see cref="AudioSource"/> を追加、取得する </summary>
  public AudioSource AddSource() {
    var source = gameObject.AddComponent<AudioSource>();
    source.playOnAwake = false;
    return source;
  }

  /// <summary> 自身に追加された <see cref="AudioSource"/> を全て取得 </summary>
  public IEnumerable<AudioSource> GetSources() {
    var sources = GetComponents<AudioSource>();
    foreach (var source in sources) { yield return source; }
  }

  /// <summary> 再生中ではない <see cref="AudioSource"/> を取得 </summary>
  public AudioSource GetSource() {
    return GetSources().FirstOrDefault(source => !source.isPlaying);
  }

  /// <summary> <see cref="GetSource()"/> で取得に成功すれば true を返す </summary>
  public bool GetSource(out AudioSource source) {
    source = GetSource();
    return source != null;
  }

  /// <summary> 再生中でない <see cref="AudioSource"/> を全て削除 </summary>
  public void Refresh() {
    var sources = GetSources().Where(source => !source.isPlaying);
    foreach (var source in sources) { Destroy(source); }
  }

  /// <summary> <see cref="AudioSource"/> を全て削除 </summary>
  public void Release() {
    foreach (var source in GetSources()) { source.Stop(); Destroy(source); }
  }

  /// <summary> ループ再生中ではない、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlaying() {
    var sources = GetSources().Where(source => !source.loop);
    return sources.Any(source => source.isPlaying);
  }

  /// <summary> ループ中も含めて、１つでも再生中の
  /// <see cref="AudioSource"/> があれば true を返す </summary>
  public bool IsPlayingWithLoop() {
    return GetSources().Any(source => source.isPlaying);
  }

  /// <summary> <see cref="AudioSource"/> が存在すれば true を返す </summary>
  public bool ExistSource() { return GetSources().Any(); }

  /// <summary> 再生中でない <see cref="AudioSource"/> があれば true を返す </summary>
  public bool ExistStopSource() { return GetSources().Any(src => !src.isPlaying); }

  /// <summary> ループ設定の <see cref="AudioSource"/> があれば true を返す </summary>
  public bool ExistLoopSource() { return GetSources().Any(source => source.loop); }

  /// <summary> ループ設定の <see cref="AudioSource"/> を全て取得 </summary>
  public IEnumerable<AudioSource> GetLoopSources() {
    return GetSources().Where(source => source.loop);
  }

  /// <summary> <see cref="AudioClip"/> が登録された
  /// <see cref="AudioSource"/> を全て同時に再生する </summary>
  public void AllPlay() {
    var sources = GetSources().Where(source => source.clip != null);
    foreach (var source in sources) { source.Play(); }
  }

  /// <summary> <see cref="AudioClip"/> が登録された
  /// <see cref="AudioSource"/> を全て同時に停止する </summary>
  public void AllStop() {
    var sources = GetSources().Where(source => source.clip != null);
    foreach (var source in sources) { source.Stop(); }
  }
}
