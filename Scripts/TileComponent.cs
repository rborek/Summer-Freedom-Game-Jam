using UnityEngine;
using System.Collections;

public class TileComponent : MonoBehaviour {
    ParticleSystem particles;
    void Start () {
        particles = gameObject.AddComponent<ParticleSystem>();
        particles.startColor = Color.black;
        particles.Stop();
    }

    void Update () {
        Collider[] hit = Physics.OverlapBox(transform.position,
            new Vector3(0.5f,1.0f,0.5f));
        for (int i = 0; i < hit.Length; ++i)
        {
            if (hit[i].gameObject.GetComponent<TurningScript>() != null)
            {
                particles.Play();
                return;
            }
        }
        particles.Stop();
	}
}
