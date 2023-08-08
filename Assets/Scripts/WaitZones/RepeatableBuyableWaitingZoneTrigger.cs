
using UnityEngine;
using System;
using Taxi.Upgrades;
using Zenject;

namespace Taxi.WaitZones
{
    public class RepeatableBuyableWaitingZoneTrigger : WaitZoneTrigger
    {
        private IUpgradeCommand _upgradeCommand;

        [Inject]
        private void Init([Inject(Id = UpgradeCommandType.StackerSpeedUpgrade)] IUpgradeCommand upgradeCommand)
        {
            _upgradeCommand = upgradeCommand;
        }


        protected override Action GetSuccessAction(Collider other)
        {
            return () =>
            {
                print(_upgradeCommand);
                _upgradeCommand.Execute();
            };
        }
    }
}