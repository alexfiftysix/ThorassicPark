using System;
using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_1.Camera
{
    public class CameraController : MonoBehaviour
    {
        private const float Speed = 5;

        public GameObject player;
        private Phase _phase = Phase.Building;

        private Vector2 _movementDirection = Vector2.zero;
        private UnityEngine.Camera _camera;

        private float _zoomSpeed = 10f;
        private float _minZoom = 4f;
        private float _maxZoom = 15f;
        private float _goalOrthographicSize = 5f;
        private bool _hasZoomed = false;

        [SerializeField] private float cameraFollowSpeed = 5;
        
        private void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
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
        }

        private void ZoomToGoalSize()
        {
            if (_phase != Phase.Building && !_hasZoomed)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _goalOrthographicSize, Time.deltaTime);
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

        public void OnZoom(InputValue inputValue)
        {
            if (_phase != Phase.Building) return;

            var val = inputValue.Get<Vector2>().y / 60 * -1;
            var goalSize = Math.Max(Math.Min(_camera.orthographicSize + val, _maxZoom), _minZoom);
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, goalSize, Time.deltaTime * _zoomSpeed);
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