using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    GameObject player;
    PlayerController PC;
    GameObject selectedTower;
    GameObject[] towerList = new GameObject[3];
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    private int curTower = 0;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        PC = player.GetComponent<PlayerController>();
        // fill towerList
        towerList[0] = tower1;
        towerList[1] = tower2;
        towerList[2] = tower3;


        selectedTower = towerList[0];
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0) && PC.selectedTileTransform != null) // right click
        {
            spawnTower();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            changeTower();
        }
	}

    void spawnTower()
    {
        Instantiate(selectedTower,
        PC.selectedTileTransform.position + new Vector3(0, selectedTower.transform.localScale.y), 
        PC.selectedTileTransform.rotation);
    }

    void changeTower()
    {
        curTower++;
        selectedTower = towerList[curTower];
    }

  


}
