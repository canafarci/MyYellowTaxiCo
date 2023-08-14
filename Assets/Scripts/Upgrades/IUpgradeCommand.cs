using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TaxiGame.Upgrades
{
    public interface IUpgradeCommand
    {
        public void Execute();
    }
}
