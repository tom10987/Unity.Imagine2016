
using UnityEngine;
using System.Collections.Generic;

//------------------------------------------------------------
// NOTICE:
// 各ミニゲームのリソースを登録、管理する
//
//------------------------------------------------------------
// TIPS:
// 空の GameObject にコンポーネントとして追加してください
//
// ARSystem プレハブに入らない、
// ミニゲーム特有のプレハブなどをまとめて登録してください
//
//------------------------------------------------------------

public class GameResource : MonoBehaviour
{
  [SerializeField]
  GameObject[] _objects = null;
  /// <summary> オブジェクトの一覧を取得 </summary>
  public IEnumerable<GameObject> objects { get { return _objects; } }
}

public static class GameResourceExtension
{
  /// <summary> 登録されているインスタンスを全て生成 <para>
  /// <see cref="Object.Instantiate{T}"/> によって
  /// 生成したオブジェクトそのものを返す </para></summary>
  public static IEnumerable<GameObject> CreateResource(this GameResource resource)
  {
    var parent = new GameObject("_GameResources");
    var resources = Object.Instantiate(resource);
    foreach (var res in resources.objects)
    {
      var instance = Object.Instantiate(res);
      instance.transform.SetParent(parent.transform);
      yield return instance;
    }
    Object.Destroy(resources.gameObject);
    GameResources.instance.Release();
  }

  /// <summary> 配列としてリソースを生成する </summary>
  public static GameObject[] CreateResourceArray(this GameResource resource)
  {
    var list = new List<GameObject>();
    foreach (var res in resource.CreateResource()) { list.Add(res); }
    return list.ToArray();
  }
}
