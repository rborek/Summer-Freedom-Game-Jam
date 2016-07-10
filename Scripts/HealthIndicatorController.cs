using UnityEngine;
using System.Collections;

public class HealthIndicatorController : MonoBehaviour {
    HealthComponent personHealth;
    MeshRenderer IndicatorRend; 
	// Use this for initialization
	void Start () {
        personHealth = transform.parent.GetComponent<HealthComponent>();
        IndicatorRend = transform.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
       
        transform.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.blue, personHealth.Health()/100);
	}
}
