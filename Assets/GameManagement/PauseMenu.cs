using UnityEngine;

namespace GameManagement
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenu;

        public void OnPause()
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GameIsPaused = false;
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameIsPaused = true;
        }

        public void QuitGame()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
