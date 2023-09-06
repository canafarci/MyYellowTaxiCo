using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class StackerSpeedUpgradeReceiver
    {
        private UpgradeUtility _upgradeUtility;
        private HatGenerator _itemGenerator;

        [Inject]
        private void Init(UpgradeUtility upgradeUtility, HatGenerator itemGenerator)
        {
            _upgradeUtility = upgradeUtility;
            _itemGenerator = itemGenerator;
        }

        public void UpgradeStackerSpeed(int index)
        {
            float speed = _upgradeUtility.GetItemGeneratorSpeed(index);
            _itemGenerator.SetSpawnRate(speed);
        }
    }
}
