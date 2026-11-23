using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    void Update()
    {
        // Press Escape to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Hide cursor
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze game
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None; // Show cursor
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Unfreeze before changing scene
        SceneManager.LoadScene("MainMenu");
    }
}