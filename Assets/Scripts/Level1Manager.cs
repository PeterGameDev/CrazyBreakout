using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;
    public bool ballLaunched;
    public GameObject gameOverMenu;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ballLaunched = false;
    }


    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Show gameover menu
        gameOverMenu.SetActive(true);
    }

    public void CheckWin()
    {
        int brickCount = FindObjectsOfType<BrickScript>().Length;
        if (brickCount == 0)
        {
            GameWon();
        }
    }

    public void GameWon()
    {
        Debug.Log("Game Won!");
    }
}
