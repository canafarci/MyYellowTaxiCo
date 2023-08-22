using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public class ItemUtility
    {
        private readonly InventoryObjectType[] _stackableItemTypes = new InventoryObjectType[]
                                                        {
                                                            InventoryObjectType.YellowHat,
                                                            InventoryObjectType.PurpleHat,
                                                            InventoryObjectType.BlackHat,
                                                            InventoryObjectType.RepairBox,
                                                            InventoryObjectType.Tire
                                                        };

        private readonly InventoryObjectType[] _allItemTypes = new InventoryObjectType[]
                                                        {
                                                            InventoryObjectType.YellowHat,
                                                            InventoryObjectType.PurpleHat,
                                                            InventoryObjectType.BlackHat,
                                                            InventoryObjectType.RepairBox,
                                                            InventoryObjectType.Tire,
                                                            InventoryObjectType.GasHandle,
                                                            InventoryObjectType.Follower
                                                        };

        public bool IsStackableObject(InventoryObjectType objectType)
        {
            bool isStackableObject = false;

            foreach (InventoryObjectType objType in _stackableItemTypes)
            {
                if (objectType == objType)
                    isStackableObject = true;
            }

            return isStackableObject;
        }

        public InventoryObjectType[] GetStackableItemTypes() => _stackableItemTypes;
        public InventoryObjectType[] GetAllInventoryObjectTypes() => _allItemTypes;
    }
}
