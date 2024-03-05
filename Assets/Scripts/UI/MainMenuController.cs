using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your game scene name
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits"); // Replace with your game scene name
    }

    public void ShowDirections()
    {
        SceneManager.LoadScene("Directions"); // Replace with your game scene name
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }
}
