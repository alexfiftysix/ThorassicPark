using System;
using Buildings.DeckBuilder;
using Common.Utilities;
using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Buildings.Builder
{
    public class Builder : MonoBehaviour
    {
        private GameObject _ghostBuilding;
        private Attraction _ghostAttraction;
        public MoneyBag moneyBag;

        public UnityEngine.Camera mainCamera;
        public GameManager gameManager;

        public Canvas handCanvas;
        private const float HandCanvasHiddenYPosition = -100;
        private float _handCanvasX;
        private float _handCanvasZ;
        private Hand _hand;

        private bool _parkIsBroken = false;

        // ghost overlaps
        private bool _ghostCanBePlaced;

        private void Start()
        {
            _hand = FindObjectOfType<Hand>();
            FindObjectOfType<GameManager>().OnParkBreaks += (sender, args) => OnParkBreak();

            _handCanvasX = handCanvas.transform.position.x;
            _handCanvasZ = handCanvas.transform.position.z;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (_parkIsBroken)
            {
                var newY = Mathf.Lerp(handCanvas.transform.position.y, HandCanvasHiddenYPosition, Constants.UiMoveSpeed * Time.deltaTime);
                handCanvas.transform.position = new Vector3(_handCanvasX, newY, _handCanvasZ);
            }

            if (_ghostBuilding is null) return;

            var newPosition = GetGridMouseWorldPosition();
            _ghostBuilding.transform.position = newPosition;

            var isOverlapping = _ghostAttraction.CanBePlaced();
            if (isOverlapping != _ghostCanBePlaced)
            {
                _ghostCanBePlaced = isOverlapping;
                _ghostAttraction.SetColor(_ghostCanBePlaced
                    ? GhostConstants.CanPlace
                    : GhostConstants.CanNotPlace
                );
            }
        }

        public void SetGhostBuilding(int index)
        {
            if (_parkIsBroken) return;
            
            if (!(_ghostBuilding is null))
            {
                DeselectGhost();
            }

            var ghostPlan = _hand.GetAttraction(index);
            if (moneyBag.Withdraw(ghostPlan.GetComponent<Attraction>().cost))
            {
                _ghostBuilding = Instantiate(ghostPlan.gameObject, Vector3.zero, Quaternion.identity);
                _ghostAttraction = _ghostBuilding.GetComponent<Attraction>();
                _ghostAttraction.Ghostify();
                _ghostCanBePlaced = true;
            }
        }

        public void OnBuild()
        {
            if (_ghostBuilding is null || _parkIsBroken || !_ghostCanBePlaced) return;

            _ghostAttraction.Build(moneyBag, gameManager);
            gameManager.AddAttraction(_ghostAttraction);

            _ghostBuilding = null;
            _ghostAttraction = null;
        }

        public void DeselectGhost()
        {
            if (_ghostBuilding is null) return;

            moneyBag.AddMoney(_ghostAttraction.cost);
            Destroy(_ghostBuilding);
            _ghostBuilding = null;
            _ghostAttraction = null;
        }

        private Vector3 GetGridMouseWorldPosition(int z = 0)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            var roundedX = Convert.ToInt32(Math.Round(mouseWorldPos.x));
            var roundedY = Convert.ToInt32(Math.Round(mouseWorldPos.y));
            return new Vector3(roundedX, roundedY, z);
        }

        private void OnParkBreak()
        {
            DeselectGhost();
            _parkIsBroken = true;
        }
    }
}