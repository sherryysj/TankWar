using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // player manager variable
    public int playerLives;
    private int playerScore;

    // player prefab reference
    public GameObject playerBornPrefab;

    // instance
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }
    public int PlayerScore { get => playerScore; set => playerScore = value; }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recover()
    {
        if (playerLives == 0)
        {
            // game over, return to Home Scene
        } else
        {
            playerLives--;
            Instantiate(playerBornPrefab, new Vector3(-2, -8, 0), Quaternion.identity);
        }
    }

}
