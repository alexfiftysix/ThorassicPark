using System.Collections.Generic;
using System.ComponentModel;
using GameManagement;
using Phase_1.Builder.Buildings.ArrowPen;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;
using Utilities.Extensions;

namespace Phase_1.Builder.Buildings
{
    public abstract class Attraction : MonoBehaviour, IPointerClickHandler
    {
        public ViewRadius viewRadius;
        public MoneyBag moneyBag;
        public OverlapCheck overlapCheck;

        // Ghost building
        public SpriteRenderer spriteRenderer;
        public Material ghostMaterial;
        private Material _defaultMaterial;
        private static readonly int GhostShaderColor = Shader.PropertyToID("Color_c9794d5cc0484bfb99bcbf82f83078e6");
        public bool isGhost;

        // Break
        public List<GameObject> walls;
        public bool isBroken = false;

        // Monsters
        public GameObject monster;
        public int monsterCount = 3;
        
        // Damage
        public Material damagedMaterial;
        [SerializeField] private float timeToBecomeDamaged = 5;
        private Timer _damagedTimer;
        private bool _isDamaged;
        [SerializeField] private float timeToBreak = 3;
        private Timer _breakTimer;
        private GameManager _gameManager;

        // Prestige
        [SerializeField] public float prestige = 1;

        // Build
        private AudioSource _audioSource;
        
        // Money
        private const float MoneyIntervalInSeconds = 1;
        [SerializeField] private float moneyPerVisitorPerSecond = 1;
        private Timer _moneyTimer;

        public int cost = 1;

        protected virtual void Awake()
        {
            _defaultMaterial = spriteRenderer.material;
            _audioSource = GetComponent<AudioSource>();
        }

        protected virtual void Start()
        {
            _moneyTimer = gameObject.AddTimer(MoneyIntervalInSeconds, AddMoney);
        }
        
        protected virtual void AddMoney()
        {
            moneyBag.AddMoney(viewRadius.VisitorCount / moneyPerVisitorPerSecond);
        }

        public virtual void Build(MoneyBag newMoneyBag, GameManager gameManager)
        {
            moneyBag = newMoneyBag;
            _gameManager = gameManager;
            UnGhostify();
            for (var i = 0; i < monsterCount; i++)
            {
                Instantiate(monster, transform.position - Vector3.forward, Quaternion.identity);
            }

            StartDamagedTimer();
            _audioSource.Play();
        }

        protected virtual void Break()
        {
            _gameManager.EnterRunFromDinosaursPhase();
            Destroy(_moneyTimer);
        }

        public virtual void ReleaseDinosaurs()
        {
            spriteRenderer.material = _defaultMaterial;
            Destroy(viewRadius.gameObject);
            Destroy(_breakTimer);
            Destroy(_damagedTimer);
            foreach (var wall in walls)
            {
                Destroy(wall);
            }
        }

        public virtual bool CanBePlaced()
        {
            return !overlapCheck.HasOverlap();
        }

        public virtual void Ghostify()
        {
            spriteRenderer.material = ghostMaterial;
            isGhost = true;
            SetWallColliders(false);
        }

        private void StartDamagedTimer()
        {
            _damagedTimer = gameObject.AddTimer(timeToBecomeDamaged, () =>
            {
                spriteRenderer.material = damagedMaterial;
                _isDamaged = true;
                Destroy(_damagedTimer);
                StartBreakTimer();
            });
        }

        private void StartBreakTimer()
        {
            _breakTimer = gameObject.AddTimer(timeToBreak, () =>
            {
                Break();
                Destroy(_breakTimer);
            });
        }

        private void Repair()
        {
            _isDamaged = false;
            spriteRenderer.material = _defaultMaterial;
            Destroy(_breakTimer);
            StartDamagedTimer();
        }

        private void UnGhostify()
        {
            spriteRenderer.material = _defaultMaterial;
            isGhost = false;
            SetWallColliders(true);
        }

        public virtual void SetColor(Color newColor)
        {
            spriteRenderer.material.SetColor(GhostShaderColor, newColor);
        }

        private void SetWallColliders(bool newState)
        {
            foreach (var wall in walls)
            {
                wall.GetComponent<Collider2D>().enabled = newState;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isDamaged)
            {
                Repair();
            }
        }
    }
}