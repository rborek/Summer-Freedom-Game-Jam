using UnityEngine;
using System.Collections;

public class CornerPath : MonoBehaviour {
    public Vector3 turnDir;
    ArrayList alreadyTurned;
	// Use this for initialization
	void Start () {
        alreadyTurned = new ArrayList();
        //GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 bounds = new Vector3(1f, 1f, 1f);
        Collider[] collisions = Physics.OverlapBox(transform.position, bounds);
        for (int i = 0; i < collisions.Length; ++i)
        {
            if (collisions[i].gameObject.GetComponent<TurningScript>() != null)
            {
                GameObject toTurn = collisions[i].gameObject;
                if (!alreadyTurned.Contains(toTurn.GetInstanceID()))
                {
                    TurningScript script = toTurn.GetComponent<TurningScript>();
                    script.StartTurning(turnDir);
                    alreadyTurned.Add(toTurn.GetInstanceID());
                }
            }
        }


    }
}
