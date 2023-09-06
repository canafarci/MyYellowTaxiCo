using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class QueueHatDropTrigger : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueueCoordinator _queue;
        private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
        private Dictionary<Collider, Inventory> _inventories = new Dictionary<Collider, Inventory>();
        private void Awake()
        {
            _queue = GetComponent<DriverQueueCoordinator>();
            _stacker = GetComponentInChildren<Stacker>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG) || other.CompareTag(Globals.HELPER_NPC_TAG))
            {
                _inventories[other] = other.GetComponent<Inventory>();
                _coroutines[other] = StartCoroutine(DropLoop(other));
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG) || other.CompareTag(Globals.HELPER_NPC_TAG))
            {
                if (_coroutines[other] != null)
                    StopCoroutine(_coroutines[other]);
            }
        }
        private IEnumerator DropLoop(Collider other)
        {
            while (CanDropHat(other))
            {
                InventoryObjectType hatType = _queue.GetHatType();
                StackableItem item = _inventories[other].PopInventoryObject(hatType) as StackableItem;
                _stacker.StackItem(item);
                yield return new WaitForSeconds(.25f);
            }
        }
        private bool CanDropHat(Collider other)
        {
            bool stackerHasSpace = _stacker.ItemStack.Count < _stacker.MaxStackSize;

            InventoryObjectType hatType = _queue.GetHatType();
            bool inventoryHasItemType = _inventories[other].HasInventoryObjectType(hatType);

            return stackerHasSpace && inventoryHasItemType;
        }
    }
}
