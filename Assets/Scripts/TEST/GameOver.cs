using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenuNew : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Debug.Log($"Loading scene: {mainMenuSceneName}");
    }
}
