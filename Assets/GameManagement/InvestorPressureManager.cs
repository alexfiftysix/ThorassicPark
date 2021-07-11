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
        private GameManager _gameManager;

        private float _intervalInSeconds = 1;
        [SerializeField] private float sliderMoveSpeed = 1;
        [SerializeField] private float sliderGrowthSpeed = 1;

        private float _moneyAtLastInterval;
        private Timer _calculationTimer;

        public Slider slider;
        private float _dollarsPerSecond;
        private float _highestDollarsPerSecond;

        public TextMeshProUGUI growthPerSecondText;
        private float _lastDollarsPerSecond = 0;
        private float _growthPerSecond = 0;
        private float _highestGrowthPerSecond = 1;
        private float _lowestGrowthPerSecond = -1;

        [SerializeField] private float lowGrowthMaximumTime = 5;
        [SerializeField] private float lowGrowthThreshold = 1;
        private Timer _lowGrowthTimer;

        // Start is called before the first frame update
        private void Start()
        {
            _moneyAtLastInterval = MyStatistics.moneyEarned;
            _calculationTimer = gameObject.AddTimer(_intervalInSeconds, Recalculate);

            _gameManager = GetComponent<GameManager>();
            _gameManager.OnParkBreaks += (sender, args) => OnParkBreaks();

            _lowGrowthTimer = gameObject.AddTimer(lowGrowthMaximumTime, InvestorShutdown);
            _lowGrowthTimer.DeActivate();
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
            Destroy(_calculationTimer);
            ExitLowGrowthZone();
        }

        private void Recalculate()
        {
            CalculateDollarsPerSecond();

            CalculateGrowthPerSecond();
            
            if (!_lowGrowthTimer.isActive && _growthPerSecond < lowGrowthThreshold)
            {
                EnterLowGrowthZone();
            } 
            else if (_lowGrowthTimer.isActive && _growthPerSecond >= lowGrowthThreshold)
            {
                ExitLowGrowthZone();
            }
        }

        private void EnterLowGrowthZone()
        {
            _lowGrowthTimer.Activate();

            var colours = ColorBlock.defaultColorBlock;
            colours.normalColor = Color.red;
            slider.colors = colours;
        }

        private void ExitLowGrowthZone()
        {
            _lowGrowthTimer.DeActivate();

            var colours = ColorBlock.defaultColorBlock;
            colours.normalColor = Color.white;
            slider.colors = colours;
        }

        private void CalculateDollarsPerSecond()
        {
            _dollarsPerSecond = (MyStatistics.moneyEarned - _moneyAtLastInterval) / _intervalInSeconds;
            _moneyAtLastInterval = MyStatistics.moneyEarned;
            _highestDollarsPerSecond = Math.Max(_highestDollarsPerSecond, _dollarsPerSecond);
        }

        private void CalculateGrowthPerSecond()
        {
            _growthPerSecond = _dollarsPerSecond - _lastDollarsPerSecond;
            _lastDollarsPerSecond = _dollarsPerSecond;
            _highestGrowthPerSecond = Math.Max(_highestGrowthPerSecond, _growthPerSecond);
            _lowestGrowthPerSecond = Math.Min(_lowestGrowthPerSecond, _growthPerSecond);

            growthPerSecondText.text = $"{_growthPerSecond:C}";
        }


        private void InvestorShutdown()
        {
            _gameManager.InvestorShutDown();
        }
    }
}