
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//------------------------------------------------------------
// TIPS:
// Component クラスの外部拡張
// Component からの派生クラス（MonoBehaviour を継承したクラス含む）しか取り出せない
//
//------------------------------------------------------------

static class ComponentExtension {

  /// <summary> <see cref="Component.GetComponentsInChildren{T}()"/>
  /// で取得できる配列から親オブジェクト以外を取り出す </summary>
  public static IEnumerable<T> GetAllChildren<T>(this Component parent) where T : Component {
    var children = parent.GetComponentsInChildren<T>();
    return children.Where(child => child.transform != parent.transform);
  }

  /// <summary> <see cref="Component.GetComponentsInChildren{T}()"/>
  /// で取得できる配列から、直下にある子オブジェクトのみを取り出す </summary>
  public static IEnumerable<T> GetOnlyChildren<T>(this Component parent) where T : Component {
    var children = parent.GetComponentsInChildren<T>();
    return children.Where(child => child.transform.parent == parent.transform);
  }
}
