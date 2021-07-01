using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder;
using Phase_1.Builder.Buildings;
using Phase_1.Camera;
using Phase_2.Helipad;
using Phase_2.Player;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public CameraController mainCamera;
        public GameObject playerPrefab;
        private PlayerController _player;

        public Text phaseText;

        private Phase _phase = Phase.Building;
        public Builder builder;

        // Escape point
        public EscapePoint escapePointPrefab;
        private EscapePoint _escapePoint;
        private float _escapePhaseStartTime;
        private Bounds _escapePointSpawnBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 0));
        [SerializeField] private int timeBeforeEscapeSpawnsInSeconds = 1;

        // Park Breaking
        public Slider breakChanceSlider;
        [SerializeField] private float parkBreakInterval = 5f;
        private float _parkBreakIntervalTimePassed;
        private readonly List<Attraction> _attractions = new List<Attraction>();
        
        public void PlayerEscaped()
        {
            _phase = Phase.GameWon;
            MyStatistics.WonLastGame = true;
            SceneManager.LoadScene(2);
        }

        public void GameOver()
        {
            _phase = Phase.GameLost;
            MyStatistics.WonLastGame = false;
            SceneManager.LoadScene(2);
        }

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
            breakChanceSlider.value = _attractions.Sum(a => a.breakChancePercent);
        }
        
        private void Start()
        {
            phaseText.text = "Building Phase";
        }

        private void Update()
        {
            if (_phase == Phase.Building && Interval.HasPassed(parkBreakInterval, _parkBreakIntervalTimePassed, out _parkBreakIntervalTimePassed))
            {
                var chance = _attractions.Sum(a => a.breakChancePercent);
                if (MyRandom.Percent(chance))
                {
                    EnterRunFromDinosaursPhase();
                }
            }

            if (_phase == Phase.RunningFromDinosaurs && Time.time > _escapePhaseStartTime + timeBeforeEscapeSpawnsInSeconds)
            {
                StartEscapePhase();
            }
        }

        public void EnterRunFromDinosaursPhase()
        {
            foreach (var attraction in _attractions)
            {
                attraction.Break();
            }

            Destroy(breakChanceSlider.gameObject);
            
            _phase = Phase.RunningFromDinosaurs;
            _escapePhaseStartTime = Time.time;

            var player = ActivatePlayer();
            mainCamera.EnterEscapePhase(player);
            _player = player.GetComponent<PlayerController>();
            _player.manager = GetComponent<GameManager>();
            _player.GetComponent<MouseWheelZoom>().myCamera = mainCamera.GetComponent<Camera>();

            builder.DeselectGhost();
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
            _player.helipad = _escapePoint;

            _phase = Phase.Escaping;
        }

        private GameObject ActivatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
