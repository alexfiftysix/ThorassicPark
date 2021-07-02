using UnityEngine;
using UnityEngine.SceneManagement;

namespace Phase_1.Builder.CardChooser
{
    public class DeckMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // TODO: Do this better
        }

        public void Back()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // TODO: Do this better
        }
    }
}
