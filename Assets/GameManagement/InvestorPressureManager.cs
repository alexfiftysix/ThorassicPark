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
        [SerializeField] private float intervalInSeconds = 5;
        [SerializeField] private float sliderMoveSpeed = 1;
        [SerializeField] private float sliderGrowthSpeed = 1;

        private int _moneyAtLastInterval;
        private Timer _moneyTimer;

        public Slider slider;
        public TextMeshProUGUI dollarsPerSecondText;
        private float _dollarsPerSecond;
        private float _sliderMaxValue;

        // Start is called before the first frame update
        private void Start()
        {
            _moneyAtLastInterval = MyStatistics.moneyEarned;
            _moneyTimer = gameObject.AddTimer(intervalInSeconds, CalculateDollarsPerSecond);
            GetComponent<GameManager>().OnParkBreaks += (sender, args) => OnParkBreaks();
        }

        private void Update()
        {
            slider.value = Mathf.Lerp(slider.value, _dollarsPerSecond, sliderMoveSpeed * Time.deltaTime);
            slider.maxValue = Mathf.Lerp(slider.maxValue, _sliderMaxValue, sliderGrowthSpeed * Time.deltaTime);
        }

        private void OnParkBreaks()
        {
            Destroy(slider);
            Destroy(_moneyTimer);
        }

        private void CalculateDollarsPerSecond()
        {
            _dollarsPerSecond = (MyStatistics.moneyEarned - _moneyAtLastInterval) / intervalInSeconds;
            dollarsPerSecondText.text = $"${_dollarsPerSecond}/s";
            if (_dollarsPerSecond > slider.maxValue)
            {
                _sliderMaxValue = _dollarsPerSecond;
            }

            _moneyAtLastInterval = MyStatistics.moneyEarned;
        }
    }
}