
using UnityEngine;

//------------------------------------------------------------
// TIPS:
// static なヒエラルキーオブジェクトを生成
//
// Awake() メソッドの呼び出しで初期化が行われるため、
// 継承したクラスは必ず、ヒエラルキー上に配置すること
//
// また、Awake() の呼び出しが前提になるため、
// 初期化の順番に注意すること
//
//------------------------------------------------------------

public abstract class SingletonBehaviour<T> :
  MonoBehaviour where T : SingletonBehaviour<T> {

  static T _instance = null;
  public static T instance { get { return _instance; } }

  protected virtual void Awake() {
    if (IsSingle()) { return; }
    Debug.LogWarning("Exists other " + typeof(T));
    Debug.Log("removed " + gameObject.name);
  }

  protected bool IsSingle() {
    if (_instance == null) { _instance = this as T; }
    if (_instance == this) { return true; }
    Destroy(gameObject);
    return false;
  }
}
