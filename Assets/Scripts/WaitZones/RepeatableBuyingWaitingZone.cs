using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Taxi.WaitZones
{
    [RequireComponent(typeof(BuyableWaitingZoneVisual))]
    public class RepeatableBuyingWaitingZone : WaitingEngine
    {
        [SerializeField] private UpgradeData _upgradeData;
        [SerializeField] private ItemGenerator _itemGenerator;
        [SerializeField] float _moneyToUnlock;
        private float _remainingMoney;
        private BuyableWaitingZoneVisual _visual;
        private PayMoneyProcessor _payCalculator;
        private int _currentUpgradeIndex;
        private void Awake()
        {
            _payCalculator = GetComponent<PayMoneyProcessor>();
        }
        void Start()
        {
            Initialize(LoadUpgradeIndex());
        }
        private int LoadUpgradeIndex()
        {
            if (PlayerPrefs.HasKey(Globals.STACKER_UPGRADE_KEY))
                _currentUpgradeIndex = PlayerPrefs.GetInt(Globals.STACKER_UPGRADE_KEY);
            else
                PlayerPrefs.SetInt(Globals.STACKER_UPGRADE_KEY, 1);
            return _currentUpgradeIndex;
        }

        private void ResetLoop()
        {
            Initialize(_currentUpgradeIndex);
        }
        public override void Begin(WaitZoneConfigSO config, GameObject other)
        {
            base.Begin(config, other);
        }
        protected override void OnSuccess(GameObject instigator)
        {
            _currentUpgradeIndex++;
            PlayerPrefs.SetInt(Globals.STACKER_UPGRADE_KEY, _currentUpgradeIndex);
            SetStackerSpeed(_currentUpgradeIndex);

            ResetLoop();
            base.OnSuccess(instigator);
        }
        private void SetStackerSpeed(int index)
        {
            if (_currentUpgradeIndex < _upgradeData.StackSpeeds.Length)
                _itemGenerator.SetSpawnRate(_upgradeData.StackSpeeds[_currentUpgradeIndex].SpawnRate);
        }
        private void Initialize(int currentIndex)
        {
            SetStackerSpeed(currentIndex);

            if (GetIndexIsAtMaxLength())
                return;

            _visual = GetComponent<BuyableWaitingZoneVisual>();
            _visual.Initialize(_moneyToUnlock);
            _visual.SetLevelText(_currentUpgradeIndex);


            _remainingMoney = _upgradeData.StackSpeeds[_currentUpgradeIndex + 1].Cost;
        }
        private bool GetIndexIsAtMaxLength()
        {
            bool retVal = _currentUpgradeIndex >= _upgradeData.StackSpeeds.Length - 1;
            if (retVal)
                gameObject.SetActive(false);

            return retVal;
        }

        protected override bool CheckCanContinue(float remainingTime)
        {
            return remainingTime > 0f && _remainingMoney > 0f;
        }

        protected override void Iterate(ref float remainingTime, GameObject instigator)
        {
            bool isSuccessful = _payCalculator.ProcessPay(ref remainingTime, ref _remainingMoney);

            if (!isSuccessful)
            {
                Cancel(instigator);
            }

            _visual.UpdateVisual(_remainingMoney, _moneyToUnlock);
        }
    }

}

