using UnityEngine;
using System.Collections;

public class SpeedBoostTower : MonoBehaviour
{

    // Use this for initialization
    float boost = 3;
    float timeLast = 0.5f;
    int radius = 5;
    int level = 0;
    public int cost = 100;
    public static int upgradeCost = 50;
    public Collider[] collisions;
    public ArrayList boosted = new ArrayList();
    void Start()
    {
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SphereCollider>().radius = radius;
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    { }
    void OnTriggerEnter(Collider other)
    {
       
        MovementComponent movement = other.gameObject.GetComponent<MovementComponent>();
        if(movement != null)
        {
            Debug.Log("working");
            movement.SetTargetSpeed(boost, timeLast, gameObject.GetInstanceID());
        }
    }

    void OnTriggerExit(Collider other)
    {
        MovementComponent movement = other.gameObject.GetComponent<MovementComponent>();
        if (movement != null)
        {
            movement.ResetSpeed(gameObject.GetInstanceID());
        }
    }
    public void Upgrade()
    {
        boost += 0.5f;
        level += 1;
    }
}
