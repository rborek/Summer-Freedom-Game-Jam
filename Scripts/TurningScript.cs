using UnityEngine;
using System.Collections;

public class TurningScript : MonoBehaviour
{
    Vector3 startDir;
    Vector3 velocity;
    Vector3 turnDir;
    float lerpTime;
    bool turning;
    // Use this for initialization
    void Start()
    {
        turning = false;
        velocity = new Vector3(0, 0, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if (turning)
        {
            lerpTime -= Time.deltaTime * 3;
            velocity = Vector3.Lerp(turnDir, startDir, lerpTime);
            if (lerpTime <= 0)
            {
                turning = false;
            }
        }
    }

    public void StartTurning(Vector3 turnDir)
    {
        this.turnDir = turnDir.normalized * velocity.magnitude;
        startDir = velocity;
        turning = true;
        lerpTime = 1;
    }
}
