
using UnityEngine;
using System;
using Taxi.Upgrades;

namespace Taxi.WaitZones
{
    public class RepeatableBuyableWaitingZoneTrigger : WaitZoneTrigger
    {
        private IUpgradeCommand _upgradeCommand;
        protected override Action GetSuccessAction(Collider other)
        {
            return () =>
            {
                _upgradeCommand.Execute();
            };
        }
        public void SetUpgradeCommand(IUpgradeCommand command) => _upgradeCommand = command;
    }
}