using UnityEngine;
using System.Collections;

public class SpeedBoostComponent : MonoBehaviour
{

    // Use this for initialization
    float boost = 10;
    int timeLast = 10;
    int radius = 100;
    int level = 0;
    public int cost = 100;
    public Collider[] collisions;
    public ArrayList boosted = new ArrayList();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        collisions = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < collisions.Length; i++)
        {
            MovementComponent speed = collisions[i].gameObject.GetComponent<MovementComponent>();
            if (speed != null)
            {
                if (!boosted.Contains(collisions[i]))
                {
                    speed.SetTargetSpeed(boost, timeLast);
                    boosted.Add(collisions[i]);
                }
            } 
        }
        for(int i = 0; i < boosted.Count; i++)
        {
            ArrayList escapedIndexes = new ArrayList();
            for(int j = 0; j < boosted.Count; j++)
            {
                bool escaped = true;
                for(int k = 0; k < collisions.Length; k++)
                {
                    if (boosted[j].Equals(collisions[k])) // if it escapsed
                    {
                        escaped = false;
                        break;
                    }
                }
                if (escaped)
                {
                    GameObject o = (GameObject)(boosted[j]).gameObject.GetComponent<MovementComponent>().ResetSpeed();
                    boosted.Remove(boosted[j]);
                }
            }

    }
}
