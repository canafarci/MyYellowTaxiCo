using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.UI;
using Taxi.WaitZones;
using UnityEngine;
using Zenject;

namespace Taxi.Upgrades
{
    public class UpgradeLoader : MonoBehaviour
    {
        [Inject]
        private void Init([Inject(Id = Enums.UpgradeCommandType.LoadUpgrade)] IUpgradeCommand loadCommand)
        {
            loadCommand.Execute();
        }
    }
}