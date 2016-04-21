
using UnityEngine;

//------------------------------------------------------------
// NOTICE:
// static インスタンスとして管理する
//
//------------------------------------------------------------
// TIPS:
// Awake() の呼び出しで初期化する前提のため、
// 初期化の順番に注意してください
//
// 独自の初期化が必要な場合、派生クラスで Awake() を override してください
//
// また、Awake() を override した場合で、インスタンスの管理方法が変わる場合、
// Release() も override してください
// override していない状態で呼び出した場合は想定していません
//
//------------------------------------------------------------

public abstract class SingletonBehaviour<T> :
  MonoBehaviour where T : SingletonBehaviour<T>
{
  static T _instance = null;
  public static T instance { get { return _instance; } }

  /// <summary> インスタンスを解放する </summary>
  public virtual void Release()
  {
    Destroy(gameObject);
    _instance = null;
  }

  protected virtual void Awake()
  {
    if (IsSingle()) { DontDestroyOnLoad(gameObject); return; }
    Debug.LogWarning("Exists other " + typeof(T));
    Debug.Log("removed " + gameObject.name);
  }

  protected bool IsSingle()
  {
    if (_instance == null) { _instance = this as T; }
    if (_instance == this) { return true; }
    Destroy(gameObject);
    return false;
  }
}
