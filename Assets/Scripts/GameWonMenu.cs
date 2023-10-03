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
        SceneManager.LoadScene(Level1Manager.Instance.nextSceneNumber);
    }
}
