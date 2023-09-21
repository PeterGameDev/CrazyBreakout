using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;
    public bool ballLaunched;
    public GameObject gameOverMenu;
    public GameObject gameWonMenu;
    public GameObject pauseMenu;
    public bool paused = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
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
        if (ballLaunched && Input.GetButton("Jump") && !paused)
        {
            Time.timeScale = 2.0f;
        }
        if (ballLaunched && Input.GetButtonUp("Jump") && !paused)
        {
            Time.timeScale = 1.0f;
        }
        if (Input.GetButtonDown("Cancel") && !paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }


    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Show gameover menu
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        SFXScript.Instance.playGameOverSound();
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
        Time.timeScale = 0f;
        paused = true;
        gameWonMenu.SetActive(true);
        SFXScript.Instance.playVictorySound();
        // Stop game afterward.
    }
}
