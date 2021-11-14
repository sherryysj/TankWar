using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    // map item prefabs reference
    // King, StoneWall, SteelWall, Grass, River, AirWall, Born, EnemyBorn
    public GameObject[] mapItemPrefabs;

    // map variable
    public int enemyAmount;
    private int enemyGenerated = 0;
    private float enemyBornTimer = 5f;
    private int[] enemyBornLocationX;

    void Awake()
    {
        GenerateKing();
        GenerateMap();
        GenerateAirWall();
        GeneratePlayer();
        enemyBornLocationX = new int[3] { -10, 10, 0 };
    }

    void Update()
    {
        // call generate enemy every 5 second if enemy generated not reach the highest amount
        if (enemyGenerated < enemyAmount)
        {
            if (enemyBornTimer >= 5f)
            {
                GenerateEnemy();
                enemyGenerated++;
                enemyBornTimer = 0;
            }
            else
            {
                enemyBornTimer += Time.deltaTime;
            }
        }

    }

    // generate player at game start
    private void GeneratePlayer()
    {
        Instantiate(mapItemPrefabs[6], new Vector3(-2, -8, 0), Quaternion.identity);
    }

    // generate enemy tank
    private void GenerateEnemy()
    {
        int num = Random.Range(0, 3);
        Instantiate(mapItemPrefabs[7], new Vector3(enemyBornLocationX[num], 8, 0), Quaternion.identity, transform);
    }

    // generate king and protect at game start
    private void GenerateKing()
    {
        Instantiate(mapItemPrefabs[0], new Vector3(0, -8, 0), Quaternion.identity, transform);
        Instantiate(mapItemPrefabs[1], new Vector3(-1, -8, 0), Quaternion.identity, transform);
        Instantiate(mapItemPrefabs[1], new Vector3(1, -8, 0), Quaternion.identity, transform);
        Instantiate(mapItemPrefabs[1], new Vector3(0, -7, 0), Quaternion.identity, transform);
        Instantiate(mapItemPrefabs[1], new Vector3(1, -7, 0), Quaternion.identity, transform);
        Instantiate(mapItemPrefabs[1], new Vector3(-1, -7, 0), Quaternion.identity, transform);

    }

    // generate general map items at game start
    private void GenerateMap()
    {
        // generate column by column from map bottom
        for (int y = -8; y <= 7;  y++)
        {
            // save position X value where there alrealy have an item;
            HashSet<int> usedX = new HashSet<int>();

            // item amount in each line, random generated
            int itemAmount;

            // for the two line same as king and king protect, only generate item outside the king and player born site
            if (y <= -7)
            {
                itemAmount = Random.Range(1, 13);
                for (int x = -3; x <= 3; x++)
                {
                    usedX.Add(x);
                }
            // for the line under enemy born position, not generate item around the enemy born position to avoid enemy not move
            }
            else if (y == 7)
            {
                itemAmount = Random.Range(5, 13);
                Debug.Log("Item Num: " + itemAmount);
                for (int x = -3; x <= 3; x++)
                {
                    usedX.Add(-10);
                    usedX.Add(-9);
                    usedX.Add(10);
                    usedX.Add(9);
                    usedX.Add(0);
                    usedX.Add(-1);
                    usedX.Add(1);
                }
            } else
            {
                itemAmount = Random.Range(2, 16);
            }

            for (int i = 0; i < itemAmount; i++)
            {
                // random generate X position for an item and if the position has item, find another one
                int x;
                do { 
                    x = Random.Range(-10, 11); 
                } while (usedX.Contains(x));

                int itemNumber = Random.Range(1, 5);
                Instantiate(mapItemPrefabs[itemNumber], new Vector3(x, y, 0), Quaternion.identity, transform);
                usedX.Add(x);
                
            } 
        }

    }

    // generate air walls at game start to avoid tanks go out of the map
    private void GenerateAirWall()
    {
        
        int y;
        int x = -11;
        for (y = -9; y <= 9; y++)
        {
            Instantiate(mapItemPrefabs[5], new Vector3(x, y, 0), Quaternion.identity, transform);
        }

        x = 11;
        for (y = -9; y <= 9; y++)
        {
            Instantiate(mapItemPrefabs[5], new Vector3(x, y, 0), Quaternion.identity, transform);
        }

        y = -9;
        for (x = -10; x <= 10; x++)
        {
            Instantiate(mapItemPrefabs[5], new Vector3(x, y, 0), Quaternion.identity, transform);
        }

        y = 9;
        for (x = -10; x <= 10; x++)
        {
            Instantiate(mapItemPrefabs[5], new Vector3(x, y, 0), Quaternion.identity, transform);
        }
    }

}
