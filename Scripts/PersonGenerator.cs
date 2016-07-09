using UnityEngine;
using System.Collections;

public class PersonGenerator : MonoBehaviour {
    ArrayList people;
    GameObject tileGenerator;
    Vector3 spawnPos;
    float timeToSpawnNext = 1;
    float spawnFreq = 1;
	void Start () {
        people = new ArrayList();
        tileGenerator = GameObject.Find("TileGenerator");
        spawnPos.z -= 1;
	}
	
	void Update () {
        spawnPos = tileGenerator.GetComponent<TileGenerator>().StartPos();
        timeToSpawnNext -= Time.deltaTime;
        if (timeToSpawnNext <= 0)
        {
            GameObject toSpawn = GameObject.CreatePrimitive(PrimitiveType.Cube);
            toSpawn.AddComponent<TurningScript>();
            toSpawn.transform.position = spawnPos;
            Debug.Log(spawnPos);
            people.Add(toSpawn);
            timeToSpawnNext += Random.value * 2 + 0.5f;
        }
	}

    public ArrayList GetPeople()
    {
        return people;
    }
}
