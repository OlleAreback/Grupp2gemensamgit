using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeScene()
    {
        // Load the scene named "NewScene"
        SceneManager.LoadSceneAsync("StartScreen", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
