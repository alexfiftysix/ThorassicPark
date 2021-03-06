using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameManagement.Camera
{
    public class CameraController : MonoBehaviour
    {
        private const float Speed = 5;

        public GameObject player;
        private Phase _phase = Phase.Building;

        private Vector2 _movementDirection = Vector2.zero;
        private UnityEngine.Camera _camera;

        private float _goalOrthographicSize = 5f;
        private bool _hasZoomed = false;
        private float _zoomSpeed = 3;

        [SerializeField] private float cameraFollowSpeed = 5;
        
        private void Start()
        {
            _camera = GetComponentInChildren<UnityEngine.Camera>();
        }

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

            ZoomToGoalSize();
        }

        public void EnterEscapePhase(GameObject newPlayer)
        {
            if (_phase == Phase.Escaping) return;

            _goalOrthographicSize = 1.5f;
            player = newPlayer;
            _phase = Phase.Escaping;
            Destroy(GetComponent<MouseWheelZoom>());
        }

        private void ZoomToGoalSize()
        {
            if (_phase != Phase.Building && !_hasZoomed)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _goalOrthographicSize, Time.deltaTime * _zoomSpeed);
                if (Math.Abs(_camera.orthographicSize - _goalOrthographicSize) < 0.001)
                {
                    _hasZoomed = true;
                }
            }
        }

        public void OnMove(InputValue inputValue)
        {
            if (_phase != Phase.Building) return;

            _movementDirection = inputValue.Get<Vector2>();
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 0, -100), Time.deltaTime * cameraFollowSpeed);
        }

        private void HandleMove()
        {
            transform.position += new Vector3(_movementDirection.x * Speed * Time.deltaTime,
                _movementDirection.y * Speed * Time.deltaTime, 0);
        }
    }
}