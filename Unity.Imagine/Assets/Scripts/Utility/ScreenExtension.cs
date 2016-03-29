
using UnityEngine;

static class ScreenExtension {

  /// <summary> 画面サイズを返す </summary>
  public static Vector3 size {
    // TIPS: GameObject の Transform が Vector3 を使うため、Vector3 で返す
    get {
      var width = (float)Screen.width;
      var height = (float)Screen.height;
      return new Vector3(width, height);
    }
  }

  /// <summary> 画面中央の座標を返す </summary>
  public static Vector3 center { get { return size * 0.5f; } }

  /// <summary> 画面比率を返す </summary>
  public static Vector3 aspect {
    get {
      var aspect = Camera.main.aspect;
      return new Vector3(aspect, 1f);
    }
  }

  /// <summary> 正投影カメラの描画サイズを反映させた aspect を返す </summary>
  public static Vector3 orthoAspect {
    get {
      return Camera.main.orthographic ?
        aspect * Camera.main.orthographicSize : aspect;
    }
  }
}
