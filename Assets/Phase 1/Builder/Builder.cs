using System;
using System.Linq;
using GameManagement;
using Phase_1.Builder.Buildings;
using Phase_1.Builder.DeckBuilder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_1.Builder
{
    public class Builder : MonoBehaviour
    {
        private GameObject _ghostBuilding;
        private Attraction _ghostAttraction;
        public MoneyBag moneyBag;

        public Attraction[] buildings;
        public UnityEngine.Camera mainCamera;
        public GameManager gameManager;

        // ghost overlaps
        private bool _ghostCanBePlaced;

        void Start()
        {
            buildings = ChosenCards.Attractions.Select(c => c.attraction).ToArray();
        }
        
        // Update is called once per frame
        void Update()
        {
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
            if (!(_ghostBuilding is null))
            {
                DeselectGhost();
            }

            var ghostPlan = buildings[index];
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
            if (_ghostBuilding is null) return;
            if (!_ghostCanBePlaced) return;

            _ghostAttraction.Build(moneyBag);
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
    }
}