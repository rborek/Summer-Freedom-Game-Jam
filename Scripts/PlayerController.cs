using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Transform playerLocation;
    public static int playerSpeed = 50;
    public int onSpeed = 1;
    public int maxRange = 10;
    public Transform selectedTileTransform; 
    Ray ray;
    RaycastHit hit;
    GameObject highlight;
    Light hLight;
    Renderer highlightRenderer;
	// Use this for initialization
	void Start () {
        playerLocation = transform;
        highlight = GameObject.Find("highlight");
        hLight = highlight.GetComponent<Light>();
        highlightRenderer = highlight.GetComponent<Renderer>();
        
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        castRays();
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

    void castRays()
    {
        selectedTileTransform = hit.transform;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 8))
        {
            highlight.transform.position = hit.transform.position;
            // highlightRenderer.material.color = Color.red;
           
            //highlightRenderer.enabled = true; 
            if(hLight.range < maxRange)
                hLight.range += onSpeed;
        }
        else
        {
            hLight.range = 0;
            //highlightRenderer.enabled = false;
        }
    }
}
