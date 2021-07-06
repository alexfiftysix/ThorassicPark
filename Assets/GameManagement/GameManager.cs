using System;
using System.Collections.Generic;
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

        // [HideInInspector] public Phase phase = Phase.Building;

        private Phase _phase = Phase.Building;
        [HideInInspector] public event EventHandler OnParkBreaks;
        public Builder builder;

        // Escape point
        public EscapePoint escapePointPrefab;
        private EscapePoint _escapePoint;
        private float _escapePhaseStartTime;
        private Bounds _escapePointSpawnBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 0));
        [SerializeField] private int timeBeforeEscapeSpawnsInSeconds = 1;

        // Park Breaking
        private readonly List<Attraction> _attractions = new List<Attraction>();
        private AudioSource _breakSound;

        private void Start()
        {
            phaseText.text = "Building Phase";
            _breakSound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_phase == Phase.RunningFromDinosaurs && Time.time > _escapePhaseStartTime + timeBeforeEscapeSpawnsInSeconds)
            {
                StartEscapePhase();
            }
        }
        
        public void PlayerEscaped()
        {
            _phase = Phase.GameWon;
            MyStatistics.WonLastGame = true;
            SceneManager.LoadScene(3);
        }

        public void GameOver()
        {
            _phase = Phase.GameLost;
            MyStatistics.WonLastGame = false;
            SceneManager.LoadScene(3); // TODO: Do this better
        }

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
        }

        public void EnterRunFromDinosaursPhase()
        {
            foreach (var attraction in _attractions)
            {
                attraction.ReleaseDinosaurs();
            }

            _phase = Phase.RunningFromDinosaurs;
            _escapePhaseStartTime = Time.time;

            var player = ActivatePlayer();
            mainCamera.EnterEscapePhase(player);
            _player = player.GetComponentInChildren<PlayerController>();
            _player.manager = this;
            _player.GetComponentInChildren<MouseWheelZoom>().myCamera = mainCamera.GetComponent<Camera>();

            builder.DeselectGhost();
            Destroy(builder.gameObject);
            phaseText.text = "Escape Phase";

            OnParkBreaks?.Invoke(this, EventArgs.Empty);

            _breakSound.Play();
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
