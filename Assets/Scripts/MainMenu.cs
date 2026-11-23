using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartUntrained()
    {
        // Tell the AI to start with random weights
        PlayerPrefs.SetInt("UsePreTrainedAI", 0); 
        SceneManager.LoadScene("OutdoorsScene"); // Load your game scene
    }

    public void StartPreTrained()
    {
        // Tell the AI to load the JSON file
        PlayerPrefs.SetInt("UsePreTrainedAI", 1); 
        SceneManager.LoadScene("OutdoorsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}