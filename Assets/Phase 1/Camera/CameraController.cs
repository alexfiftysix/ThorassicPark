using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_1.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private const float Speed = 5;

        public GameObject player;
        private Phase _phase = Phase.Building;

        private Vector2 _movementDirection = Vector2.zero;

        void Update()
        {
            if (_phase == Phase.Building)
            {
                HandleMove();
            }
            else
            {
                FollowPlayer();
            }
        }

        public void EnterEscapePhase(GameObject newPlayer)
        {
            if (_phase == Phase.Escaping) return;

            GetComponent<UnityEngine.Camera>().orthographicSize = 1.5f;
        
            player = newPlayer;
            _phase = Phase.Escaping;
        }

        private void OnMove(InputValue inputValue)
        {
            if (_phase == Phase.Escaping) return;

            _movementDirection = inputValue.Get<Vector2>(); 
        }

        private void FollowPlayer()
        {
            transform.position = player.transform.position + new Vector3(0, 0, -100);
        }
    
        private void HandleMove()
        {
            transform.position += new Vector3(_movementDirection.x * Speed * Time.deltaTime, _movementDirection.y * Speed * Time.deltaTime, 0);
        }
    }
}
