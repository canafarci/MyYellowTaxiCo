using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class QueueHatDropTrigger : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueue _queue;
        private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
        private Dictionary<Collider, Inventory> _inventories = new Dictionary<Collider, Inventory>();
        private void Awake()
        {
            _queue = GetComponent<DriverQueue>();
            _stacker = GetComponentInChildren<Stacker>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
            {
                _inventories[other] = other.GetComponent<Inventory>();
                _coroutines[other] = StartCoroutine(DropLoop(other));
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
            {
                if (_coroutines[other] != null)
                    StopCoroutine(_coroutines[other]);
            }
        }
        private IEnumerator DropLoop(Collider other)
        {
            while (_inventories[other].GetItem(_queue.HatType))
            {
                StackableItem item = _inventories[other].GetItem(_queue.HatType);
                _inventories[other].RemoveItem(item);
                _stacker.StackItem(item);
                yield return new WaitForSeconds(.25f);
            }
        }
    }
}
