using UnityEngine;

/// <summary>
/// マウス便利機能
/// </summary>
public class MouseUtility : MonoBehaviour
{
    /// <summary>
    /// 1フレーム前のPosition座標の差を取得
    /// </summary>
    public Vector3 getDeltaPos
    {
        get; private set;
    }

    Vector3 _oldPos;

    void Start()
    {
        _oldPos = Input.mousePosition;
    }

    void Update()
    {
        var currentPos = Input.mousePosition;
        getDeltaPos = currentPos - _oldPos;
        _oldPos = currentPos;
    }
}
