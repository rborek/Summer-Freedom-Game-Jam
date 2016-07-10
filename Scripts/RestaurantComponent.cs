using UnityEngine;
using System.Collections;

public class RestaurantComponent : MonoBehaviour {
    GameObject gameManager;

	void Start () {
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        HealthComponent health = collided.GetComponent<HealthComponent>();
        if (health != null)
        {
            if (Random.value < health.Health())
            {
                // add money

            }
            Destroy(collided);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
