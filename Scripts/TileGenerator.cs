using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour
{

    public int gridWidth;
    public int gridHeight;
    private int startXPos;
    public int gridY = 1;
    GameObject[,] tiles;
    ArrayList corners;

    void Start()
    {
        tiles = new GameObject[gridHeight, gridWidth];
        corners = new ArrayList();
        GenerateMap();
    }

    public int StartX()
    {
        return startXPos;
    }

    public void GenerateMap()
    {
        Vector3 moveDir;
        int xPos = (int)(Random.value * gridWidth);
        startXPos = xPos;
        int zPos = 0;
        moveDir = new Vector3(0, 0, 1);
        int timeToTurn = 3;
        while (zPos < gridHeight)
        {
            Vector3 initMoveDir;
            if (tiles[zPos, xPos] == null)
            {
                tiles[zPos, xPos] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject tile = tiles[zPos, xPos];
                tile.transform.position = new Vector3(xPos, gridY, zPos);
                tile.GetComponent<Renderer>().material.color = Color.red;
            }
            if (Random.value > 0.3f && timeToTurn <= 0)
            {
                timeToTurn = 3;
                if (moveDir.z == 1 && Random.value > 0.5)
                {
                    if (Random.value > 0.5)
                    {
                        moveDir = new Vector3(1, 0, 0);
                    } else
                    {
                        moveDir = new Vector3(-1, 0, 0);
                    }
                    GameObject corner = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    corner.AddComponent<CornerPath>();
                    corner.GetComponent<CornerPath>().turnDir = moveDir;
                    corner.GetComponent<Renderer>().material.color = Color.blue;
                    corner.transform.position = new Vector3(xPos, gridY + 1, zPos + 1);
                    corners.Add(corner);
                }
                else if (Random.value > 0.4)
                {
                    initMoveDir = moveDir;
                    moveDir.z = 1;
                    moveDir.x = 0;
                    CreateForwardCorner(xPos, zPos, moveDir, initMoveDir);
                }
            }

            initMoveDir = moveDir;
            zPos += (int)(Mathf.Round(moveDir.z));
            xPos += (int)(Mathf.Round(moveDir.x));
            if (xPos >= gridWidth)
            {
                
                xPos = gridWidth - 1;
                moveDir = new Vector3(0, 0, 1);
                CreateForwardCorner(xPos, zPos, moveDir, initMoveDir);

            }
            else if (xPos < 0)
            {
                xPos = 0;
                moveDir = new Vector3(0, 0, 1);
                CreateForwardCorner(xPos, zPos, moveDir, initMoveDir);
            }
            timeToTurn--;
        }

    }

    private void CreateForwardCorner(int xPos, int zPos, Vector3 moveDir, Vector3 initMoveDir) {
        GameObject corner = GameObject.CreatePrimitive(PrimitiveType.Cube);
        corner.AddComponent<CornerPath>();
        corner.GetComponent<CornerPath>().turnDir = moveDir;
        corner.GetComponent<Renderer>().material.color = Color.blue;
        corner.transform.position = new Vector3(xPos + initMoveDir.x, gridY + 1, zPos);
        corners.Add(corner);
    }



    private bool CanGoX(int xPos, ref Vector3 moveDir)
    {
    
        return true;
    }



    void Update()
    {

    }
}
