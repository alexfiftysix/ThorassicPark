using System;
using Phase_1.Builder.Buildings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Phase_1.Builder
{
    public class Builder : MonoBehaviour
    {
        private GameObject _ghostBuilding;
        private int _money = 5;

        public Building[] buildings = new Building[5];
        public UnityEngine.Camera mainCamera;
        public Text moneyText;

        public void SetGhostBuilding(int index)
        {
            if (!(_ghostBuilding is null))
            {
                Destroy(_ghostBuilding);
            }

            var ghostPlan = buildings[index];
            if (_money < ghostPlan.GetComponent<Building>().GetCost()) return;

            _ghostBuilding = Instantiate(ghostPlan.gameObject, Vector3.zero, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            var newPosition = GetGridMouseWorldPosition();
            if (_ghostBuilding == null) return;
            _ghostBuilding.transform.position = newPosition;
        }

        void OnBuild()
        {
            // TODO: Remove that transparent shader (if it's added)
            var cost = _ghostBuilding.GetComponent<Building>().GetCost();
            if (_money < cost)
            {
                // TODO: Make the user know what's happened
                return;
            }

            var placementPosition = GetGridMouseWorldPosition();
            var building = Instantiate(_ghostBuilding, placementPosition, Quaternion.identity);
            building.GetComponent<Building>().Build();
            Destroy(_ghostBuilding);

            _money -= cost;
            SetMoneyText(_money);
        }

        private Vector3 GetGridMouseWorldPosition(int z = -1)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            var roundedX = Convert.ToInt32(Math.Round(mouseWorldPos.x));
            var roundedY = Convert.ToInt32(Math.Round(mouseWorldPos.y));
            return new Vector3(roundedX, roundedY, z);
        }

        private void SetMoneyText(int money)
        {
            moneyText.text = $"${_money}";
        }
    }
}