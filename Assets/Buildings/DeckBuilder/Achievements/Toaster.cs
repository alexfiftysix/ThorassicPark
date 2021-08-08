using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Buildings.DeckBuilder.Achievements
{
    /// The Toaster takes a message, and pops up a message box to the user
    /// Toasts may be timed, at which point they'll pop away again
    /// Only so many toasts can be shown at any one time
    public class Toaster : MonoBehaviour
    {
        public int capacity = 3;
        public TextMeshProUGUI toastText;
        private Queue<string> _toasts;

        // Start is called before the first frame update
        private void Start()
        {
            _toasts = new Queue<string>(capacity);
            toastText.text = string.Empty;
        }

        public void Add(string message)
        {
            _toasts.Enqueue(message);
            if (_toasts.Count > capacity)
            {
                _toasts.Dequeue();
            }
            
            Redraw();
        }

        private void Redraw()
        {
            toastText.text = string.Join("\n\r", _toasts);
        }
    }
}
