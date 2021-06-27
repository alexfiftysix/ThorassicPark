using System;
using GameManagement;
using Phase_1.Builder.Buildings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_1.Builder
{
    public class Builder : MonoBehaviour
    {
        private GameObject _ghostBuilding;
        public MoneyBag moneyBag;

        public Attraction[] buildings = new Attraction[5];
        public UnityEngine.Camera mainCamera;
        public GameManager gameManager;

        public void SetGhostBuilding(int index)
        {
            if (!(_ghostBuilding is null))
            {
                Destroy(_ghostBuilding);
            }

            var ghostPlan = buildings[index];
            if (moneyBag.Withdraw(ghostPlan.GetComponent<Attraction>().GetCost()))
            {
                _ghostBuilding = Instantiate(ghostPlan.gameObject, Vector3.zero, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            var newPosition = GetGridMouseWorldPosition();
            if (_ghostBuilding is null) return;
            _ghostBuilding.transform.position = newPosition;
        }

        void OnBuild()
        {
            // TODO: Remove that transparent shader (if it's added)
            var placementPosition = GetGridMouseWorldPosition();
            var building = Instantiate(_ghostBuilding, placementPosition, Quaternion.identity);
            var attraction = building.GetComponent<Attraction>(); 
            attraction.Build(moneyBag);
            gameManager.AddAttraction(attraction);

            Destroy(_ghostBuilding);
            _ghostBuilding = null;
        }
        
        public void OnDeselectGhost()
        {
            if (_ghostBuilding is null) return;

            moneyBag.AddMoney(_ghostBuilding.GetComponent<Attraction>().GetCost());
            Destroy(_ghostBuilding);
            _ghostBuilding = null;
        }

        private Vector3 GetGridMouseWorldPosition(int z = -1)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            var roundedX = Convert.ToInt32(Math.Round(mouseWorldPos.x));
            var roundedY = Convert.ToInt32(Math.Round(mouseWorldPos.y));
            return new Vector3(roundedX, roundedY, z);
        }
    }
}