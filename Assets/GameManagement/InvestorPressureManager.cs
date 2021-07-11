using System;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Utilities.Extensions;

namespace GameManagement
{
    public class InvestorPressureManager : MonoBehaviour
    {
        private float _intervalInSeconds = 1;
        [SerializeField] private float sliderMoveSpeed = 1;
        [SerializeField] private float sliderGrowthSpeed = 1;

        private float _moneyAtLastInterval;
        private Timer _moneyTimer;

        public Slider slider;
        public TextMeshProUGUI earningsChangePerSecond;
        private float _dollarsPerSecond;
        private float _highestDollarsPerSecond;

        private float _lastDollarsPerSecond = 0;
        private float _growthPerSecond = 0;
        private float _highestGrowthPerSecond = 1;
        private float _lowestGrowthPerSecond = -1;

        // Start is called before the first frame update
        private void Start()
        {
            _moneyAtLastInterval = MyStatistics.moneyEarned;
            _moneyTimer = gameObject.AddTimer(_intervalInSeconds, CalculateDollarsPerSecond);
            GetComponent<GameManager>().OnParkBreaks += (sender, args) => OnParkBreaks();
        }

        private void Update()
        {
            slider.value = Mathf.Lerp(slider.value, _growthPerSecond, sliderMoveSpeed * Time.deltaTime);
            slider.maxValue = Mathf.Lerp(slider.maxValue, _highestGrowthPerSecond * 1.25f, sliderGrowthSpeed * Time.deltaTime);
            slider.minValue = Mathf.Lerp(slider.minValue, _lowestGrowthPerSecond * 1.25f, sliderGrowthSpeed * Time.deltaTime);
        }

        private void OnParkBreaks()
        {
            Destroy(slider);
            Destroy(_moneyTimer);
        }

        private void CalculateDollarsPerSecond()
        {
            _dollarsPerSecond = (MyStatistics.moneyEarned - _moneyAtLastInterval) / _intervalInSeconds;
            _moneyAtLastInterval = MyStatistics.moneyEarned;
            _highestDollarsPerSecond = Math.Max(_highestDollarsPerSecond, _dollarsPerSecond);

            _growthPerSecond = _dollarsPerSecond - _lastDollarsPerSecond;
            _lastDollarsPerSecond = _dollarsPerSecond;
            _highestGrowthPerSecond = Math.Max(_highestGrowthPerSecond, _growthPerSecond);
            _lowestGrowthPerSecond = Math.Min(_lowestGrowthPerSecond, _growthPerSecond);

            earningsChangePerSecond.text = $"{_growthPerSecond:C}";
        }
    }
}