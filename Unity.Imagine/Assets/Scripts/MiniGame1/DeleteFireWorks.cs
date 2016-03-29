using UnityEngine;
using System.Collections;

public class DeleteFireWorks : MonoBehaviour {

    [SerializeField]
    ParticleSystem[] _childrenParticleSystem;

    ParticleSystem _particleSystem;

	void Start ()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
	
	void Update ()
    {
        DeleteParticle();
    }

   public void DeleteParticle()
    {
        if (_particleSystem.IsAlive()) return;
        
            int finishCount = 0;
            foreach (var particleSystem in _childrenParticleSystem)
            {
                if (!particleSystem.IsAlive())
                {
                    finishCount++;
                }
            }
            if (finishCount == _childrenParticleSystem.Length)
            {
                Destroy(gameObject);
            }
        

    }
}
