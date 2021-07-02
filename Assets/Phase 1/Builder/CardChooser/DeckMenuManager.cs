using UnityEngine;
using UnityEngine.SceneManagement;

namespace Phase_1.Builder.CardChooser
{
    public class DeckMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Back()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
