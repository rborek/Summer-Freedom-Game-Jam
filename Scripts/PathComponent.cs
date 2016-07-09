using UnityEngine;
using System.Collections;

public class PathComponent : MonoBehaviour {
    ParticleSystem particles;
    ParticleSystem.EmitParams emitParams;
    float timer;
    void Start () {
        particles = gameObject.AddComponent<ParticleSystem>();
        timer = 0;
        particles.Stop();
    }

    void Update () {
        Collider[] hit = Physics.OverlapBox(transform.position,
            new Vector3(0.5f,1.0f,0.5f));
        for (int i = 0; i < hit.Length; ++i) { 
        
            if (hit[i].gameObject.GetComponent<TurningScript>() != null)
            {
                emitParams.velocity = new Vector3(0, 5, 0);
                emitParams.rotation = 0;
                emitParams.startColor = Color.black;
                emitParams.startLifetime = 1;
                emitParams.startSize = 0.3f;
                emitParams.angularVelocity = 0;
                particles.Emit(emitParams, 1);
                timer = 1;
                return;
            }
        }
	}
}
