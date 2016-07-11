using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour
{
    Vector3 startDir;
    Vector3 velocity;
    Vector3 turnDir;
    float speed = 1;
    float acceleration_time = 0;
    float timer = 0;
    float start_speed;
    float target_speed = 1;
    float lerpTime;
    Quaternion turnQuat;
    Quaternion startQuat;
    Quaternion spawnQuat;
    int lastBoostingTower;
    bool turning;
    // Use this for initialization
    void Start()
    {
        turning = false;
        velocity = new Vector3(0, 0, 2.5f);
        spawnQuat = transform.localRotation;
    }

    public void SetTargetSpeed(float val, float time, int towerID ) {
        lastBoostingTower = towerID;
        timer = 0;
        start_speed = speed;
        target_speed = val;
        acceleration_time = time;
    }

    public void ResetSpeed(int towerID) {
        if (towerID == lastBoostingTower)
        {
            timer = 0;
            start_speed = speed;
            target_speed = 1;
            acceleration_time = 1;
        }
    }

    void Update()
    {
        if (Mathf.Abs(speed - target_speed) > 0.01f) 
        {
            speed = Mathf.Lerp(start_speed, target_speed, Mathf.Min(timer / acceleration_time, 1));
        }
        timer += Time.deltaTime;
        transform.position += velocity * speed * Time.deltaTime; 
        if (turning)
        {
            lerpTime -= Time.deltaTime * 3 * speed;
            transform.rotation = Quaternion.Lerp(startQuat, turnQuat, lerpTime);
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
        startQuat = Quaternion.LookRotation(velocity.normalized);
        startQuat.y += spawnQuat.y;
        turnQuat = Quaternion.LookRotation(turnDir);
        turnQuat.y += spawnQuat.y;
        startDir = velocity;
        turning = true;
        lerpTime = 1;
    }
}
