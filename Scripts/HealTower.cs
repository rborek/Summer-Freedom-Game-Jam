using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealTower : MonoBehaviour
{
    // Use this for initialization
    float power = 30;
    int radius = 5;
    int level = 0;
    public int cost = 100;
    public static int upgradeCost = 50;
    public List<HealthComponent> beingHealed = new List<HealthComponent>();
    void Start()
    {
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        gameObject.GetComponent<SphereCollider>().radius = radius;
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        for(int k= 0; k < beingHealed.Count; k++)
        {
            beingHealed[k].Heal(power * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
        if (health != null)
        {
            beingHealed.Add(other.gameObject.GetComponent<HealthComponent>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
        if (health != null)
        {
            beingHealed.Remove(other.gameObject.GetComponent<HealthComponent>());
            Debug.Log(beingHealed.Count);
        }
    }

    public void Upgrade()
    {
        power += 10;
        level += 1;
    }
}
