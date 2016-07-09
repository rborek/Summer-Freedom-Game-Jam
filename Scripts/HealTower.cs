using UnityEngine;
using System.Collections;

public class HealTower : MonoBehaviour
{
    // Use this for initialization
    float power = 10;
    int radius = 100;
    int level = 0;
    public int cost = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < collisions.Length; i++)
        {
            HealthComponent health = collisions[i].gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.Heal(power * Time.deltaTime);
            }
        }

    }

    public void upgrade()
    {

    }
}
