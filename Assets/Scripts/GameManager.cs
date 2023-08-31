using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool ballLaunched;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ballLaunched = false;
    }


    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

    public void CheckWin()
    {
        int brickCount = FindObjectsOfType<BrickScript>().Length;
        if(brickCount == 0 ) 
        {
            GameWon();
        }
    }

    public void GameWon()
    {
        Debug.Log("Game Won!");
    }
}
