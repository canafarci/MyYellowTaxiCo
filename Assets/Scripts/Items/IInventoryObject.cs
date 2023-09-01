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
        TaxiHat,
        SuberHat,
        LimoHat,
        Tire,
        ToolBox,
        GasHandle,
        Customer,
        VIP
    }
}
