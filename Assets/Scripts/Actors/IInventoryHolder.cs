using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Characters
{
    public interface IInventoryHolder
    {
        public Inventory GetInventory();
        public Transform GetHandTransform();
    }
}
