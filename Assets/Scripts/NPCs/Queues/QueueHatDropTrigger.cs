using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class QueueHatDropTrigger : MonoBehaviour
    {
        //Dependencies
        private HatStacker _stacker;
        private DriverQueueCoordinator _queue;
        //Variables
        private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
        private Dictionary<Collider, Inventory> _inventories = new Dictionary<Collider, Inventory>();
        //Constants
        private const float CLEAR_STACK_RATE = 0.25f;
        private void Awake()
        {
            _queue = GetComponent<DriverQueueCoordinator>();
            _stacker = GetComponentInChildren<HatStacker>();
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
                yield return new WaitForSeconds(CLEAR_STACK_RATE);
            }
        }
        private bool CanDropHat(Collider other)
        {
            bool stackerHasSpace = _stacker.GetItemStack().Count < _stacker.GetMaxStackSize();

            InventoryObjectType hatType = _queue.GetHatType();
            bool inventoryHasItemType = _inventories[other].HasInventoryObjectType(hatType);

            return stackerHasSpace && inventoryHasItemType;
        }
    }
}
