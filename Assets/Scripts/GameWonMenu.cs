using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonMenu : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void NextScene()
    {
        GameManager.Instance.currentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
