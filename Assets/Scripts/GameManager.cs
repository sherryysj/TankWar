using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // player manager variable
    public int playerLives;
    private int playerScore;
    private bool isKingDead;
    public int roundAmount;
    private GameObject currentRound; // map creator of current round
    public int enemyAmount;
    private int enemyKilled;
    private int gameRound = 0;

    // player prefab reference
    public GameObject playerBornPrefab;

    // map creator prefab reference
    public GameObject mapCreatorPrefab;

    // UI reference
    public Text playerLivesText;
    public Text playerScoreText;
    public Text gameRoundText;
    public Text enemiesLeftText;
    public GameObject gameOverUI;

    // instance
    private static GameManager instance;

    public static GameManager Instance { get => instance; set => instance = value; }
    public int PlayerScore { get => playerScore; set => playerScore = value; }
    public bool IsKingDead { get => isKingDead; set => isKingDead = value; }
    public int EnemyKilled { get => enemyKilled; set => enemyKilled = value; }
    public GameObject CurrentRound { get => currentRound; set => currentRound = value; }

    void Awake()
    {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (isKingDead)
        {
            GameOver();
        }
        if (enemyKilled == enemyAmount)
        {
            Destroy(currentRound);
            GenerateNewRound();
        }
        playerLivesText.text = playerLives.ToString();
        playerScoreText.text = playerScore.ToString();
        gameRoundText.text = gameRound.ToString();
        enemiesLeftText.text = (enemyAmount - enemyKilled).ToString();
}

    private void GenerateNewRound()
    {
        currentRound = Instantiate(mapCreatorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        gameOverUI.SetActive(false);
        roundAmount--;
        gameRound++;
        enemyKilled = 0;
    }

    public void Recover()
    {
        if (playerLives == 0)
        {
            GameOver();
        } else
        {
            playerLives--;
            Instantiate(playerBornPrefab, new Vector3(-2, -8, 0), Quaternion.identity);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

}
