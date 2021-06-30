using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
