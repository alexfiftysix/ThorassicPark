using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder.Achievements
{
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
