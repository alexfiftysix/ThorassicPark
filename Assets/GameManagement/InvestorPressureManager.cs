using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utilities;
using Common.Utilities.Extensions;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagement
{
    public class InvestorPressureManager : MonoBehaviour
    {
        private GameManager _gameManager;

        private const float IntervalInSeconds = 1;
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
        private float _highestGrowthOverTime = 1;
        private float _lowestGrowthOverTime = -1;
        private Queue<float> _growthOverTime = new Queue<float>();
        private float _averageGrowthOverTime = 0;

        [SerializeField] private int lowGrowthMaximumTimeInSeconds = 10;
        private float _lowGrowthThreshold = -100;
        private Timer _lowGrowthTimer;
        public TextMeshProUGUI lowGrowthThresholdText;
        
        // Move on/off screen
        private float _goalXPosition = -150;

        // Start is called before the first frame update
        private void Start()
        {
            _moneyAtLastInterval = MyStatistics.grossMoneyEarned;
            _calculationTimer = gameObject.AddTimer(IntervalInSeconds, Recalculate);

            _gameManager = GetComponent<GameManager>();
            _gameManager.OnParkBreaks += (sender, args) => OnParkBreaks();

            _lowGrowthTimer = gameObject.AddTimer(lowGrowthMaximumTimeInSeconds, InvestorShutdown);
            _lowGrowthTimer.DeActivate();

            _growthOverTime = new Queue<float>(lowGrowthMaximumTimeInSeconds);
        }

        private void Update()
        {
            slider.value = Mathf.Lerp(slider.value, _averageGrowthOverTime, sliderMoveSpeed * Time.deltaTime);
            slider.maxValue = Mathf.Lerp(slider.maxValue, _highestGrowthOverTime * 1.25f, sliderGrowthSpeed * Time.deltaTime);
            slider.minValue = Mathf.Lerp(slider.minValue, _lowestGrowthOverTime * 1.25f, sliderGrowthSpeed * Time.deltaTime);

            var newX = Mathf.Lerp(slider.transform.position.x, _goalXPosition, Constants.UiMoveSpeed * Time.deltaTime);
            slider.transform.position = new Vector2(newX, slider.transform.position.y);
        }

        private void OnParkBreaks()
        {
            Destroy(_calculationTimer);
            Destroy(_lowGrowthTimer);
            ExitLowGrowthZone();
            _goalXPosition = -150;
        }

        private void Recalculate()
        {
            CalculateDollarsPerSecond();

            CalculateGrowthPerSecond();
            
            if (!_lowGrowthTimer.isActive && _averageGrowthOverTime < _lowGrowthThreshold)
            {
                EnterLowGrowthZone();
            } 
            else if (_lowGrowthTimer.isActive && _averageGrowthOverTime >= _lowGrowthThreshold)
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
            _dollarsPerSecond = (MyStatistics.grossMoneyEarned - _moneyAtLastInterval) / IntervalInSeconds;
            _moneyAtLastInterval = MyStatistics.grossMoneyEarned;
            _highestDollarsPerSecond = Math.Max(_highestDollarsPerSecond, _dollarsPerSecond);
        }

        private void CalculateGrowthPerSecond()
        {
            _growthPerSecond = _dollarsPerSecond - _lastDollarsPerSecond;
            _lastDollarsPerSecond = _dollarsPerSecond;

            _growthOverTime.Enqueue(_growthPerSecond);
            if (_growthOverTime.Count > lowGrowthMaximumTimeInSeconds) _growthOverTime.Dequeue();

            _averageGrowthOverTime = _growthOverTime.Average();
            _highestGrowthOverTime = Math.Max(_highestGrowthOverTime, _averageGrowthOverTime);
            _lowestGrowthOverTime = Math.Min(_lowestGrowthOverTime, _averageGrowthOverTime);

            growthPerSecondText.text = $"{_averageGrowthOverTime:C}";

            var growthThreshold = _averageGrowthOverTime / 3;
            growthThreshold = growthThreshold <= 0 ? -100 : growthThreshold;
            _lowGrowthThreshold = Math.Max(_lowGrowthThreshold, growthThreshold);
            if (_lowGrowthThreshold > 0)
            {
                _goalXPosition = 50;
            }
            lowGrowthThresholdText.text = $"{_lowGrowthThreshold:C}";
        }

        private void InvestorShutdown()
        {
            _gameManager.InvestorShutDown();
        }
    }
}