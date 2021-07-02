using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void GoToDeckBuilder()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
