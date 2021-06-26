using System;
using Phase_1.Builder;
using Phase_1.Camera;
using Phase_2.Helipad;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Extensions;
using Random = System.Random;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public CameraController mainCamera;
        public GameObject playerPrefab;
        public Text phaseText;
        public Text winText;

        private DateTime _startTime;
        private Phase _phase = Phase.Building;
        public float buildingTimeInSeconds = 2;
        public Builder builder;

        // Escape point
        public EscapePoint escapePointPrefab;
        private EscapePoint _escapePoint;
        private float _escapePhaseStartTime;
        private Bounds _escapePointSpawnBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 0));
        [SerializeField] private int timeBeforeEscapeSpawnsInSeconds = 1;

        public void PlayerEscaped()
        {
            winText.text = "You won!";
            _phase = Phase.GameWon;
        }

        public void GameOver()
        {
            winText.text = "You died";
            _phase = Phase.GameLost;
        }
        
        private void Start()
        {
            _startTime = DateTime.Now;
            phaseText.text = "Building Phase";
            winText.text = "";
        }

        private void Update()
        {
            if (_phase == Phase.Building 
                && DateTime.Now > _startTime + TimeSpan.FromSeconds(buildingTimeInSeconds))
            {
                EnterWaitingPhase();
            }

            if (_phase == Phase.WaitingForEscape
                && Time.time > _escapePhaseStartTime + timeBeforeEscapeSpawnsInSeconds)
            {
                StartEscapePhase();
            }
        }
        
        private void EnterWaitingPhase()
        {
            _phase = Phase.WaitingForEscape;
            _escapePhaseStartTime = Time.time;

            var player = ActivatePlayer();
            mainCamera.EnterEscapePhase(player);

            Destroy(builder.gameObject);
            phaseText.text = "Escape Phase";
        }

        private void StartEscapePhase()
        {
            var escapeX = MyRandom.RandomInt(_escapePointSpawnBounds.min.x, _escapePointSpawnBounds.max.x);
            var escapeY = MyRandom.RandomInt(_escapePointSpawnBounds.min.y, _escapePointSpawnBounds.max.y);
            var escapePointLocation = new Vector3(escapeX, escapeY, -1);
            _escapePoint = Instantiate(escapePointPrefab, escapePointLocation, Quaternion.identity);
            _escapePoint.manager = this;

            _phase = Phase.Escaping;
        }

        private GameObject ActivatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
