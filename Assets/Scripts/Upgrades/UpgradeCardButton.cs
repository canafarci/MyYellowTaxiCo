using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Taxi.Upgrades
{
    public class UpgradeCardButton : MonoBehaviour
    {
        [SerializeField] UpgradeType _upgradeType;
        private IUpgradeCommand _upgradeCommand;
        private IUpgradeCommand _checkCanBuyCommand;

        [Inject]
        public void Construct([Inject(Id = UpgradeCommandType.ButtonUpgrade)] IUpgradeCommand upgradeCommand,
                              [Inject(Id = UpgradeCommandType.CheckCanUpgrade)] IUpgradeCommand checkCanBuyCommand)
        {
            _upgradeCommand = upgradeCommand;
            _checkCanBuyCommand = checkCanBuyCommand;
        }
        public void OnButtonClicked()
        {
            //buy the upgrade
            _upgradeCommand.Execute();
            //check next upgrade can be bought
            CheckCanBuy();
        }
        private void OnEnable()
        {
            CheckCanBuy();
        }
        private void Start()
        {
            //check if upgrade can be bought whenever player money changes
            ResourceTracker.Instance.OnPlayerMoneyChanged += PlayerMoneyChangedHandler;
        }

        private void PlayerMoneyChangedHandler(float obj)
        {
            CheckCanBuy();
        }

        private void CheckCanBuy()
        {
            _checkCanBuyCommand.Execute();
        }

        //cleanup
        private void OnDisable()
        {
            ResourceTracker.Instance.OnPlayerMoneyChanged -= PlayerMoneyChangedHandler;
        }
        public UpgradeType GetUpgradeType() => _upgradeType;
        public void SetUpgradeCommand(IUpgradeCommand upgradeCommand) => _upgradeCommand = upgradeCommand;
        public void SetCheckCanUpgradeCommand(IUpgradeCommand checkCanBuyCommand) => _checkCanBuyCommand = checkCanBuyCommand;


    }
}