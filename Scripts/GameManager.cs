using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    GameObject player;
    PlayerController PC;
    GameObject selectedTower;
    GameObject[] towerList = new GameObject[3];
    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public int gridWidth = 50;
    public int gridHeight = 50;
    public Text towerText;
    public GameObject[,] towers;
    public const int startingFunds = 500;
    private int curTower = 0;
    private static int money = startingFunds;
    // Use this for initialization
    void Start () {
        towers = new GameObject[gridHeight, gridWidth];
        player = GameObject.Find("player");
        PC = player.GetComponent<PlayerController>();
        // fill towerList
        towerList[0] = tower1;
        towerList[1] = tower2;
        towerList[2] = tower3;

        towerText.text = "Selected Tower: " + towerList[0].name;
        selectedTower = towerList[0];
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0) && PC.selectedTileTransform.GetComponent<TileComponent>() != null) // right click
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
        int x = (int)PC.selectedTileTransform.position.x;
        int z = (int)PC.selectedTileTransform.position.z;
        if (towers[z, x] == null)
        {
            towers[z, x] = (GameObject)Instantiate(selectedTower,
            PC.selectedTileTransform.position + new Vector3(0, selectedTower.transform.localScale.y),
            PC.selectedTileTransform.rotation);
        }
    }

    void changeTower()
    {
        curTower++;
        curTower %= towerList.Length;
        selectedTower = towerList[curTower];
    }

    public int getMoney()
    {
        return money;
    }

    public void addMoney(int a)
    {
        money += a;
    }

    public void subMoney(int a)
    {
        money -= a;
    }

  


}
