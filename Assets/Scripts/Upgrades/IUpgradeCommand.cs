using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Taxi.Upgrades
{
    public interface IUpgradeCommand
    {
        public void Execute();
    }
}
