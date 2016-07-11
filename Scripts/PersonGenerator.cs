using UnityEngine;
using System.Collections;

public class PersonGenerator : MonoBehaviour {
    ArrayList people;
    GameObject tileGenerator;
    public GameObject toSpawn;
    Vector3 spawnPos;
    float startSpeed = 2.5f;
    float timeToSpawnNext = 1;
    public float spawnFreq = 2;
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

                //toSpawn.AddComponent<MovementComponent>();
                // toSpawn.AddComponent<HealthComponent>();
                //toSpawn.AddComponent<BoxCollider>();
                //toSpawn.transform.position = spawnPos;
                spawnPos.y += .3f;
                Instantiate(toSpawn, spawnPos, new Quaternion(0, 0, 0, 0));
                people.Add(toSpawn);
                timeToSpawnNext += spawnFreq;
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
