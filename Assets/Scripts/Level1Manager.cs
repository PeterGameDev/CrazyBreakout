using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;
    public bool ballLaunched;
    public GameObject gameOverMenu;
    public GameObject gameWonMenu;
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

    private void Update()
    {
        // simple time manipulation test
        if (ballLaunched && Input.GetButton("Jump"))
        {
            Time.timeScale = 2.0f;
        }
        if (ballLaunched && Input.GetButtonUp("Jump"))
        {
            Time.timeScale = 1.0f;
        }
    }


    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Show gameover menu
        gameOverMenu.SetActive(true);
        // Stop game afterward.
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
        gameWonMenu.SetActive(true);
        // Stop game afterward.
    }
}
