using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public interface IInventoryObject
    {
        public InventoryObjectType GetObjectType();
    }

    public enum InventoryObjectType
    {
        YellowHat,
        PurpleHat,
        BlackHat,
        Tire,
        ToolBox,
        GasHandle,
        Follower
    }
}
