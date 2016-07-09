using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Transform playerLocation;
    public static int playerSpeed = 50;
    Ray ray;
    RaycastHit hit;
    GameObject highlight;
    MeshRenderer hlRenderer;
	// Use this for initialization
	void Start () {
        playerLocation = transform;
        highlight = GameObject.Find("highlight");
        hlRenderer = highlight.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.direction);
        Debug.DrawRay(transform.position, ray.direction, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            highlight.transform.position = hit.transform.position;
            hlRenderer.enabled = true;
            highlight.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            hlRenderer.enabled = false;
        }
	}

    void movePlayer()
    {
        if(Input.GetKey(KeyCode.W))
        {
            playerLocation.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerLocation.position += Vector3.back * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerLocation.position += Vector3.left * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerLocation.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
    }
}
