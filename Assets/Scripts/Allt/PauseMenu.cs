using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool InSettings = false;
    public GameObject pauseMenuUI;
    public GameObject cursorLock;
    public GameObject settingsMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !InSettings)
            {
                Resume();
            }
            else if (GameIsPaused && InSettings)
            {
                InSettings = false;
                settingsMenuUI.SetActive(false);
                Pause();
            }
            else if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        cursorLock.GetComponent<CursorLock>().CursorLockEnable();
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        cursorLock.GetComponent<CursorLock>().CursorLockDisable();
    }

    public void InSettingsMenu()
    {
        InSettings = true;
        Debug.Log("Settings are now activated");
    }
}