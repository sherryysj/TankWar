using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public static int numOfPlayer;

    public void StartGame(int num)
    {
        Debug.Log("Click me");
        numOfPlayer = num;
        SceneManager.LoadScene("Game");

    }
}
