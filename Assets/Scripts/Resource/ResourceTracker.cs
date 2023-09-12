using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TaxiGame.WaitZones;
using TaxiGame.Upgrades;
using Zenject;
using TaxiGame.Items;

namespace TaxiGame.Resource
{
    public class ResourceTracker : IInitializable, IDisposable
    {
        public float PlayerMoney { get { return _currentMoney; } }
        private float _currentMoney;
        private UpgradesFacade _upgradesFacade;
        private GameConfigSO _gameConfig;

        public event Action<float> OnPlayerMoneyChanged;

        public ResourceTracker(UpgradesFacade upgradesFacade, GameConfigSO gameConfig)
        {
            _upgradesFacade = upgradesFacade;
            _gameConfig = gameConfig;
        }

        public void Initialize()
        {
            MoneyStackerTrigger.MoneyPickupHandler += OnMoneyPickup;
            WandererMoney.MoneyPickupHandler += OnMoneyPickup;
            PayMoneyProcessor.MoneyPayHandler += OnPayMoney;

            Load();
        }

        public void Dispose()
        {
            MoneyStackerTrigger.MoneyPickupHandler -= OnMoneyPickup;
            WandererMoney.MoneyPickupHandler -= OnMoneyPickup;
            PayMoneyProcessor.MoneyPayHandler -= OnPayMoney;
        }
        public void ZeroMoney()
        {
            PlayerPrefs.SetInt("Money", 0);
            _currentMoney = 0;
            OnPlayerMoneyChanged?.Invoke(_currentMoney);
        }
        public void OnPayMoney(float change)
        {
            _currentMoney -= change;
            OnMoneyChange();
        }
        public void OnCheatMoneyGain(float gain)
        {
            _currentMoney += gain;
            OnMoneyChange();
        }
        private void OnMoneyPickup()
        {
            _currentMoney += _upgradesFacade.GetIncomeModifier() * _gameConfig.MoneyPerStack;
            OnMoneyChange();
        }
        private void OnMoneyChange()
        {
            PlayerPrefs.SetInt("Money", (int)_currentMoney);
            OnPlayerMoneyChanged?.Invoke(_currentMoney);
        }
        //TODO refactor
        private void Load()
        {
            if (!PlayerPrefs.HasKey("Money"))
            {
                PlayerPrefs.SetInt("Money", _gameConfig.StartMoney);
                _currentMoney = PlayerPrefs.GetInt("Money");
            }
            else if (PlayerPrefs.GetInt("TutorialComplete") == 1)
            {
                PlayerPrefs.SetInt("Money", _gameConfig.StartMoney);
                _currentMoney = PlayerPrefs.GetInt("Money");
                PlayerPrefs.SetInt("TutorialComplete", 2);
            }
            else
            {
                _currentMoney = PlayerPrefs.GetInt("Money");
            }
            OnPlayerMoneyChanged?.Invoke(_currentMoney);
        }

    }

}

