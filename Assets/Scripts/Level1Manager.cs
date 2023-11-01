using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject gameOverMenu;
    public GameObject gameWonMenu;
    public GameObject pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        // read spawn points from saved json file
        spawnObjects();
        GameManager.Instance.ballLaunched = false;
        GameManager.Instance.paused = false;
    }

    private void Update()
    {
        // simple time manipulation test
        if (GameManager.Instance.ballLaunched && Input.GetButton("Jump") && GameManager.Instance.paused)
        {
            Time.timeScale = 2.0f;
        }
        if (GameManager.Instance.ballLaunched && Input.GetButtonUp("Jump") && !GameManager.Instance.paused)
        {
            Time.timeScale = 1.0f;
        }
        if (Input.GetButtonDown("Cancel") && !GameManager.Instance.paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.paused = true;
        }
    }

    public void spawnObjects()
    {
        MyDataType.SpawnList spawnList;
        string path = Application.dataPath + "/Levels/Level" + GameManager.Instance.currentLevel + ".json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            spawnList = JsonUtility.FromJson<MyDataType.SpawnList>(jsonString);
            Debug.Log(jsonString);
        }
        else
        {
            return;
        }
        foreach (Vector3 spawnPoint in spawnList.spawnPoints)
        {
            if(objects.Count != 0)
            {
                Instantiate(objects[0], spawnPoint, objects[0].transform.rotation);
            }
        }
    }

    public void GameOver()  
    {
        Debug.Log("Game Over!");
        // Show gameover menu
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.paused = true;
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
        GameManager.Instance.paused = true;
        gameWonMenu.SetActive(true);
        SFXScript.Instance.playVictorySound();
        // Stop game afterward.
    }
}
