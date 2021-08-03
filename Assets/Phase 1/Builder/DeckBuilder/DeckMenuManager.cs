using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Configuration.Scene;

namespace Phase_1.Builder.DeckBuilder
{
    public class DeckMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(Configuration.Configuration.Scenes[Scene.Game]);
        }

        public void Back()
        {
            SceneManager.LoadScene(Configuration.Configuration.Scenes[Scene.MainMenu]);
        }
    }
}
