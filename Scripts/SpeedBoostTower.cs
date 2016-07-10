using UnityEngine;
using System.Collections;

public class SpeedBoostComponent : MonoBehaviour
{

    // Use this for initialization
    float boost = 10;
    int timeLast = 10;
    int radius = 100;
    int level = 0;
    public int cost = 100;
    public Collider[] collisions;
    public ArrayList boosted = new ArrayList();
    void Start()
    {
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    { }
    void OnTriggerEnter(Collider other)
    {
        MovementComponent movement = gameObject.GetComponent<MovementComponent>();
        if(movement != null)
        {
            movement.SetTargetSpeed(boost, timeLast);
        }
    }

    void OnTriggerExit(Collider other)
    {
        MovementComponent movement = gameObject.GetComponent<MovementComponent>();
        if (movement != null)
        {
            movement.ResetSpeed();
        }
    }
}
