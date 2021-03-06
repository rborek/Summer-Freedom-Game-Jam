﻿using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour
{

    public int gridWidth;
    public int gridHeight;
    private Vector3 startPos;
    public GameObject tile;
    GameObject restaurant;
    public int gridY = 1;
    GameObject[,] tiles;
    ArrayList corners;
    void Start()
    {
        tiles = new GameObject[gridHeight, gridWidth];
        corners = new ArrayList();
        GenerateStaticMap();
    }

    public Vector3 StartPos()
    {
        return startPos;
    }

    public void CreatePath(int zPos, int xPos)
    {
        tiles[zPos, xPos] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject tile = tiles[zPos, xPos];
        tile.transform.position = new Vector3(xPos, gridY, zPos);
       tile.GetComponent<Renderer>().enabled = false;
        tile.AddComponent<PathComponent>();
    }
    void createCorner(int z, int x, Vector3 turnDir)
    {
        GameObject corner = GameObject.CreatePrimitive(PrimitiveType.Cube);
        corner.AddComponent<CornerPath>();
        corner.GetComponent<CornerPath>().turnDir = turnDir;
        corner.GetComponent<Renderer>().material.color = Color.blue;
        corner.transform.position = new Vector3(x, gridY + 1, z);
        corners.Add(corner);
    }
    public void GenerateStaticMap()
    {
        // choose start pos
        int xPos = 46;
        int zPos = 0;
        startPos = new Vector3(xPos, gridY + 1, zPos);

        // create static path here using CreatePath
        for (int i = 0; i < 8; ++i)
        {
            CreatePath(i, 46);
        }
        createCorner(9, 46, new Vector3(-1,0,0));
        for (int i = 0; i < 19; i++)
        {
            CreatePath(8, 46 - i);
        }
        createCorner(9, 26, new Vector3(0, 0, 1));
        for (int i = 0; i < 12; i++)
        {
            CreatePath(8 + i, 27);
        }
        createCorner(21, 26, new Vector3(1, 0, 0));
        for (int i = 0; i < 12; i++)
        {
            CreatePath(20, 27 + i);
        }
        createCorner(21, 40, new Vector3(0, 0, 1));
        for (int i = 0; i < 17; i++)
        {
            CreatePath(20 + i, 39);
        }
        createCorner(37, 40, new Vector3(-1, 0, 0));
        for (int i = 0; i < 20; i++)
        {
            CreatePath(36, 39 - i);
        }
        createCorner(37, 19, new Vector3(0, 0, 1));
        for (int i = 0; i < 5; i++)
        {
            CreatePath(36 + i, 20);
        }


        for (int i = 0; i < gridHeight; ++i)
        {
            for (int j = 0; j < gridWidth; ++j)
            {
                if (tiles[i, j] == null)
                {
                    tiles[i, j] = (GameObject)Instantiate(tile, new Vector3(j, gridY, i), Quaternion.identity);
                    tiles[i, j].transform.localScale = new Vector3(1, 1, 1);
                    tiles[i, j].AddComponent<TileComponent>();
                    tiles[i, j].layer = 8;
                }
            }

        }
    }

    public void GenerateMap()
    {
        Vector3 moveDir;
        int xPos = (int)(Random.value * gridWidth);
        int zPos = 0;
        startPos = new Vector3(xPos, gridY + 1, zPos);
        moveDir = new Vector3(0, 0, 1);
        int timeToTurn = 3;
        while (zPos < gridHeight)
        {
            bool spawned = false;
            Vector3 startCycleDir = moveDir;
            Vector3 initMoveDir;
            if (tiles[zPos, xPos] == null)
            {
                tiles[zPos, xPos] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject tile = tiles[zPos, xPos];
                tile.transform.position = new Vector3(xPos, gridY, zPos);
                tile.GetComponent<Renderer>().material.color = Color.red;
                tile.AddComponent<PathComponent>();
            }
            if (Random.value > 0.1f && timeToTurn <= 0)
            {
                timeToTurn = (int)Mathf.Round(Random.value * 5) + 2;
                if (moveDir.z == 1 && Random.value > 0.2)
                {
                    if (Random.value > 0.5)
                    {
                        moveDir = new Vector3(1, 0, 0);
                    }
                    else
                    {
                        moveDir = new Vector3(-1, 0, 0);
                    }

                }
                else if (Random.value > 0.65)
                {
                    initMoveDir = moveDir;
                    moveDir.z = 1;
                    moveDir.x = 0;
                    CreateForwardCorner(xPos, zPos, moveDir, initMoveDir);
                    spawned = true;
                }
            }
            int startX = xPos;
            int startZ = zPos;
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
            else if (!moveDir.Equals(startCycleDir) && !spawned)
            {
                GameObject corner = GameObject.CreatePrimitive(PrimitiveType.Cube);
                corner.AddComponent<CornerPath>();
                corner.GetComponent<CornerPath>().turnDir = moveDir;
                corner.GetComponent<Renderer>().material.color = Color.blue;
                corner.transform.position = new Vector3(startX, gridY + 1, startZ + 1);
                corners.Add(corner);
            }

            timeToTurn--;
        }

        for (int i = 0; i < gridHeight; ++i)
        {
            for (int j = 0; j < gridWidth; ++j)
            {
                if (tiles[i, j] == null)
                {
                    tiles[i, j] = (GameObject)Instantiate(tile, new Vector3(j, gridY, i), Quaternion.identity);
                    tiles[i, j].transform.localScale = new Vector3(1, 1, 1);
                    tiles[i, j].AddComponent<TileComponent>();
                    tiles[i, j].layer = 8;
                }
            }

        }

        GameObject restaurant = GameObject.CreatePrimitive(PrimitiveType.Cube);
        restaurant.AddComponent<RestaurantComponent>();
        restaurant.transform.position = new Vector3(xPos, gridY + 2, zPos + 1.5f);
        restaurant.transform.localScale = new Vector3(4, 4, 4);
    }


    private void CreateForwardCorner(int xPos, int zPos, Vector3 moveDir, Vector3 initMoveDir)
    {
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


}
