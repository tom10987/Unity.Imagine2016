using UnityEngine;

public class RandomColorSystem : MonoBehaviour
{
    ParticleSystem _partcleSystem = null;

    void Start()
    {
        _partcleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        _partcleSystem.startColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
