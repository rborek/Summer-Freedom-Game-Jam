using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PathComponent : MonoBehaviour
{
    ParticleSystem particles;
    ParticleSystem.EmitParams emitParams;
    List<HealthComponent> damaging = new List<HealthComponent>();
    float timer;
    public static float dps = 4;
    void Start()
    {
        particles = gameObject.AddComponent<ParticleSystem>();
        timer = 0;
        particles.Stop();
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(1, 1.5f, 1);
        collider.isTrigger = true;
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        emitParams.velocity = new Vector3(0, 5, 0);
        emitParams.rotation = 0;
        emitParams.startColor = Color.black;
        emitParams.startLifetime = 1;
        emitParams.startSize = 0.3f;
        emitParams.angularVelocity = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
        if (health != null)
        {
            particles.Emit(emitParams, 10);
            damaging.Add(health);
        }
    }

    void OnTriggerExit(Collider other)
    {
        HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
        if (health != null)
        {
            damaging.Remove(health);
        }
    }


    void Update()
    {
        for (int i = 0; i < damaging.Count; ++i)
        {
            damaging[i].Damage(dps * Time.deltaTime);
        }
    }
}
