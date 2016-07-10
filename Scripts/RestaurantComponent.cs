using UnityEngine;
using System.Collections;

public class RestaurantComponent : MonoBehaviour {
    GameManager gameManager;

	void Start () {
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        HealthComponent health = collided.GetComponent<HealthComponent>();
        if (health != null)
        { 
        
            if (health.Health() >= 50)
            {
                gameManager.addMoney((int)health.Health());

            }
            Destroy(collided);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
