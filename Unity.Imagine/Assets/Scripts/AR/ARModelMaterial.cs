
using UnityEngine;

public class ARModelMaterial : MonoBehaviour {

  [SerializeField]
  Material _p1body = null;
  public Material p1body { get { return _p1body; } }

  [SerializeField]
  Material _p1clip = null;
  public Material p1clip { get { return _p1clip; } }

  [SerializeField]
  Material _p2body = null;
  public Material p2body { get { return _p2body; } }

  [SerializeField]
  Material _p2clip = null;
  public Material p2clip { get { return _p2clip; } }
}
