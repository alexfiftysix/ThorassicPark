using UnityEngine;

namespace Utilities
{
    public static class TimeControl
    {
        public static void Pause()
        {
            Time.timeScale = 0;
        }

        public static void UnPause()
        {
            Time.timeScale = 1;
        }
    }
}