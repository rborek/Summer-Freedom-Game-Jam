using UnityEngine;
using System.Collections;

public class CornerPath : MonoBehaviour
{
    public Vector3 turnDir;
    ArrayList alreadyTurned;
    BoxCollider box;
    // Use this for initialization
    void Start()
    {
        alreadyTurned = new ArrayList();
        box = gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        box.isTrigger = true;
        box.size = new Vector3(2f, 2f, 2f);
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject toTurn = other.gameObject;
        MovementComponent move = toTurn.GetComponent<MovementComponent>();
        if (move != null && !alreadyTurned.Contains(toTurn.GetInstanceID()))
        {
            move.StartTurning(turnDir);
            alreadyTurned.Add(toTurn.GetInstanceID());
        }
    }


    // Update is called once per frame
    void Update()
    {


    }
}
