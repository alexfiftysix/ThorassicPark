using UnityEngine;

namespace Characters.Player
{
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite[] _frames;
        private int _currentFrame;
        private float _timer;
        private SpriteRenderer _spriteRenderer;
        private const float FramesPerSecond = 8;
        private float _frameRate = 1f / FramesPerSecond;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _frameRate)
            {
                _timer -= _frameRate;
                _currentFrame = (_currentFrame + 1) % _frames.Length;
                _spriteRenderer.sprite = _frames[_currentFrame];
            }
        }
    }
}
