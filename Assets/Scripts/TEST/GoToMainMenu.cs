using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    // The name of the MainMenu scene
    public string mainMenuSceneName = "MainMenu";

    // Method to be called when the button is clicked
    public void LoadMainMenu()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene(mainMenuSceneName);
        Debug.Log($"Loading scene: {mainMenuSceneName}");
    }
}
