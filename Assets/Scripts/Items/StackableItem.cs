using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public class StackableItem : MonoBehaviour, IInventoryObject
    {
        public float ItemHeight;
        [SerializeField] private InventoryObjectType _hatType;

        public InventoryObjectType GetObjectType() => _hatType;
    }
}

