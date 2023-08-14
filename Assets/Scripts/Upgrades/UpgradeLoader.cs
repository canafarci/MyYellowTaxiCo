using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.UI;
using TaxiGame.WaitZones;
using UnityEngine;
using Zenject;

namespace TaxiGame.Upgrades
{
    public class UpgradeLoader : MonoBehaviour
    {
        [Inject]
        private void Init([Inject(Id = UpgradeCommandType.LoadUpgrade)] IUpgradeCommand loadCommand,
                          [Inject(Id = UpgradeCommandType.StackerSpeedUpgrade)] IUpgradeCommand loadStackerCommand)
        {
            loadCommand.Execute();
            loadStackerCommand.Execute();
        }
    }
}