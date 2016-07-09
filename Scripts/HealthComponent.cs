using UnityEngine;
using System.Collections;

public class HealthComponent : MonoBehaviour {
    float maxHealth = 100;
    float curHealth = 100;

	void Start () {
	
	}

    public void Heal(float val)
    {
        curHealth += val;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    public void Damage(float val)
    {
        curHealth -= val;
        if (curHealth <= 0) {
            Destroy(gameObject);
        }

    }
	
	void Update () {
	
	}
}
