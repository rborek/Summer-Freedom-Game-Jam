using UnityEngine;
using System.Collections;

public class PersonGenerator : MonoBehaviour {
    ArrayList people;
    GameObject tileGenerator;
    Vector3 spawnPos;
    float startSpeed = 2.5f;
    float timeToSpawnNext = 1;
    bool running = true;
	void Start () {
        people = new ArrayList();
        tileGenerator = GameObject.Find("TileGenerator");
        spawnPos.z -= 1;
	}
	
	void Update () {
        if (running)
        {
            spawnPos = tileGenerator.GetComponent<TileGenerator>().StartPos();
            timeToSpawnNext -= Time.deltaTime;
            if (timeToSpawnNext <= 0)
            {
                GameObject toSpawn = GameObject.CreatePrimitive(PrimitiveType.Cube);
                toSpawn.AddComponent<MovementComponent>();
                toSpawn.AddComponent<HealthComponent>();
                toSpawn.AddComponent<BoxCollider>();
                toSpawn.transform.position = spawnPos;
                people.Add(toSpawn);
                timeToSpawnNext += Random.value * 2 + 10.5f;
            }
        }
	}

    public void StartGenerating()
    {
        running = true;
        timeToSpawnNext = 1;
    }

    public void StopGenerating()
    {
        running = false;
    }

    public ArrayList GetPeople()
    {
        return people;
    }
}
