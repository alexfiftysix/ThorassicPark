using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder;
using Phase_1.Builder.Buildings;
using Phase_1.Camera;
using Phase_2.Helipad;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public CameraController mainCamera;
        public GameObject playerPrefab;
        public Text phaseText;
        public Text winText;

        private Phase _phase = Phase.Building;
        public Builder builder;

        // Escape point
        public EscapePoint escapePointPrefab;
        private EscapePoint _escapePoint;
        private float _escapePhaseStartTime;
        private Bounds _escapePointSpawnBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 0));
        [SerializeField] private int timeBeforeEscapeSpawnsInSeconds = 1;

        // Park Breaking
        [SerializeField] private float parkBreakInterval = 5f;
        private float _parkBreakIntervalTimePassed;
        private readonly List<Attraction> _attractions = new List<Attraction>();
        
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

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
        }
        
        private void Start()
        {
            phaseText.text = "Building Phase";
            winText.text = "";
        }

        private void Update()
        {
            if (_phase == Phase.Building && Interval.HasPassed(parkBreakInterval, _parkBreakIntervalTimePassed, out _parkBreakIntervalTimePassed))
            {
                var chance = _attractions.Sum(a => a.breakChancePercent);
                Debug.Log($"TICK {chance}");
                if (MyRandom.Percent(chance))
                {
                    Debug.Log("DINO ESCAPE");
                    EnterWaitingPhase();
                }
            }

            if (_phase == Phase.WaitingForEscape && Time.time > _escapePhaseStartTime + timeBeforeEscapeSpawnsInSeconds)
            {
                StartEscapePhase();
            }
        }
        
        

        private void EnterWaitingPhase()
        {
            foreach (var attraction in _attractions)
            {
                attraction.Break();
            }
            
            _phase = Phase.WaitingForEscape;
            _escapePhaseStartTime = Time.time;

            var player = ActivatePlayer();
            mainCamera.EnterEscapePhase(player);

            Destroy(builder.gameObject);
            phaseText.text = "Escape Phase";
        }

        private void StartEscapePhase()
        {
            var escapeX = MyRandom.Int(_escapePointSpawnBounds.min.x, _escapePointSpawnBounds.max.x);
            var escapeY = MyRandom.Int(_escapePointSpawnBounds.min.y, _escapePointSpawnBounds.max.y);
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
