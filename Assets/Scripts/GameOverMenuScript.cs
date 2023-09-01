using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void RetartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
