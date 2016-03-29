
using UnityEngine;
using NyAR.MarkerSystem.Utils;

public static class ARMarkerExtension {
  public static Vector3 GetCenterPosition(this ARMarkerList.Item item) {
    var center = item.tl_center;
    return new Vector3(center.x, center.y, 0f);
  }
}
