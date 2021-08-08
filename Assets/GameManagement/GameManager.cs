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
using Scene = Configuration.Scene;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public CameraController mainCamera;
        public GameObject playerPrefab;
        private Player _player;

        public Text phaseText;
        public Phase phase = Phase.Building;
        [HideInInspector] public bool parkIsBroken;
        public event EventHandler OnParkBreaks;
        public Builder builder;

        // Escape point
        public EscapePoint escapePointPrefab;
        private EscapePoint _escapePoint;
        private float _escapePhaseStartTime;
        private Bounds _escapePointSpawnBounds = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 0));
        [SerializeField] private int timeBeforeEscapeSpawnsInSeconds = 1;

        // Park Breaking
        public readonly List<Attraction> attractions = new List<Attraction>();
        private AudioSource _breakSound;

        // Prestige
        [HideInInspector] public float prestige = 0;

        private void Start()
        {
            phaseText.text = "Building Phase";
            _breakSound = GetComponent<AudioSource>();
            mainCamera = FindObjectOfType<CameraController>();
            MyStatistics.Reset();
            OnParkBreaks += (sender, args) => parkIsBroken = true;
        }

        private void Update()
        {
            if (phase == Phase.RunningFromDinosaurs &&
                Time.time > _escapePhaseStartTime + timeBeforeEscapeSpawnsInSeconds)
            {
                StartEscapePhase();
            }
        }

        public void PlayerEscaped()
        {
            phase = Phase.GameWon;
            MyStatistics.wonLastGame = true;
            SceneManager.LoadScene(3);
        }

        public void GameOver()
        {
            phase = Phase.GameLost;
            MyStatistics.wonLastGame = false;
            SceneManager.LoadScene(Configuration.Configuration.Scenes[Scene.PostGame]);
        }

        public void AddAttraction(Attraction attraction)
        {
            attractions.Add(attraction);
            prestige += attraction.prestige;
        }

        public void EnterRunFromDinosaursPhase()
        {
            foreach (var attraction in attractions)
            {
                attraction.ReleaseDinosaurs();
            }

            phase = Phase.RunningFromDinosaurs;
            _escapePhaseStartTime = Time.time;

            var player = ActivatePlayer();
            mainCamera.EnterEscapePhase(player);
            _player = player.GetComponentInChildren<Player>();
            _player.manager = this;
            _player.GetComponentInChildren<MouseWheelZoom>().myCamera = mainCamera.GetComponent<Camera>();

            phaseText.text = "Escape Phase";

            OnParkBreaks?.Invoke(this, EventArgs.Empty);

            _breakSound.Play();
        }

        /// <summary>
        /// Used if growth is too low for too long.
        /// </summary>
        public void InvestorShutDown()
        {
            Debug.Log("Investor Shut Down!");
            EnterRunFromDinosaursPhase();
        }

        public void VisitorEscaped()
        {
            MyStatistics.visitorsSaved++;
        }

        private void StartEscapePhase()
        {
            var escapeX = MyRandom.Int(_escapePointSpawnBounds.min.x, _escapePointSpawnBounds.max.x);
            var escapeY = MyRandom.Int(_escapePointSpawnBounds.min.y, _escapePointSpawnBounds.max.y);
            var escapePointLocation = new Vector3(escapeX, escapeY, -1);
            _escapePoint = Instantiate(escapePointPrefab, escapePointLocation, Quaternion.identity);
            _escapePoint.manager = this;
            _player.helipad = _escapePoint;

            phase = Phase.Escaping;
        }

        private GameObject ActivatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}