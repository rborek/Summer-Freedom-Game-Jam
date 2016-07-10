using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    GameObject player;
    PlayerController PC;
    GameObject selectedTower;
    GameObject[] towerList = new GameObject[2];
    public GameObject tower1;
    public GameObject tower2;
    int dayNum = 1;
    float dayTime = 0;
    public const int dayDuration = 60;
    public const int towerUpkeepCost = 10;
    public int upkeepCost = 100;
    public int gridWidth = 50;
    public int gridHeight = 50;
    public int towerCost = 100;
    int numTowers = 0;
    public Text towerText;
    public Text balance;
    public GameObject[,] towers;
    public const int startingFunds = 500;
    private int curTower = 0;
    int money = startingFunds;
    // Use this for initialization
    void Start () {
        towers = new GameObject[gridHeight, gridWidth];
        player = GameObject.Find("player");
        PC = player.GetComponent<PlayerController>();
        // fill towerList
        towerList[0] = tower1;
        towerList[1] = tower2;
        balance.text = "Balance: " + startingFunds;
        towerText.text = "Selected Tower: " + towerList[0].name;
        selectedTower = towerList[0];
	}

    void PassTime()
    {
        dayTime += Time.deltaTime;
        if (dayTime >= dayDuration)
        {
            dayTime -= dayDuration;
            money -= upkeepCost + towerUpkeepCost * numTowers;
            upkeepCost += 25; 
            dayNum++;
        }

    }
	
	// Update is called once per frame
	void Update () {
        PassTime();
        if (Input.GetMouseButtonDown(0) && PC.selectedTileTransform.GetComponent<TileComponent>() != null) // right click
        {
            if (money >= towerCost)
            {
                //money = 0;
               
                spawnTower();

            }
        
        }
        else if (Input.GetMouseButtonDown(1)
                && towers[(int)PC.selectedTileTransform.position.z, (int)PC.selectedTileTransform.position.x] != null)
        {
            Debug.Log("ssf");
            int x = (int)PC.selectedTileTransform.position.z;
            int y = (int)PC.selectedTileTransform.position.x;
            GameObject tower = towers[x, y];
            numTowers--;
            Destroy(tower);
            towers[x, y] = null;
            addMoney(towerCost / 2);
        }

        if (towers[(int)PC.selectedTileTransform.position.z, (int)PC.selectedTileTransform.position.x] != null
            && Input.GetKeyDown(KeyCode.Space))
        {
            
            int x = (int)PC.selectedTileTransform.position.z;
            int y = (int)PC.selectedTileTransform.position.x;
            if (towers[x,y].GetComponent<SpeedBoostTower>() != null)
            {
                if (money > SpeedBoostTower.upgradeCost)
                {
                    towers[x, y].GetComponent<SpeedBoostTower>().Upgrade();
                    subMoney(SpeedBoostTower.upgradeCost);
                }
            }
            else if (towers[x, y].GetComponent<HealTower>() != null)
            {
                if (money > HealTower.upgradeCost)
                {
                    towers[x, y].GetComponent<HealTower>().Upgrade();
                    subMoney(HealTower.upgradeCost);
                }
            }
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
            numTowers++;
            addMoney(-towerCost);
            balance.text = "Balance: " + money;
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
        towerText.text = "Selected Tower: " + towerList[curTower].name;
    }

    public int getMoney()
    {
        return money;
    }

    public void addMoney(int a)
    {
        Debug.Log(money);
        money += a;
        balance.text = "balance: " + money;
    }

    public void subMoney(int a)
    {
        money -= a;
        Debug.Log(money);
        balance.text = "balance: " + money;
    }

  


}
