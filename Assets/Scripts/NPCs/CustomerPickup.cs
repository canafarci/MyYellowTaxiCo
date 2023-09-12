using System.Collections;
using TaxiGame.GameState.Unlocking;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class CustomerPickup : MonoBehaviour
    {
        private CustomerQueue _queue;
        private IUnlockable _unlockable;
        private Coroutine _unloadCoroutine;

        [Inject]
        private void Init(CustomerQueue queue,
                          [InjectOptional] IUnlockable unlockable)
        {
            _queue = queue;
            _unlockable = unlockable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _unloadCoroutine = StartCoroutine(TryUnload(other));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_unloadCoroutine != null)
                    StopCoroutine(_unloadCoroutine);
            }
        }

        private IEnumerator TryUnload(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();

            while (true)
            {
                if (!inventory.HasInventoryObjectType(InventoryObjectType.Customer))
                {
                    if (_queue.TryGetCustomer(out Customer customer))
                    {
                        customer.FollowPlayer(inventory);
                        _unlockable?.UnlockObject();
                    }
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

    }
}