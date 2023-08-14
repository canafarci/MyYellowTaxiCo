using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class CustomerPickup : MonoBehaviour
    {
        private References _references;
        private CustomerQueue _queue;
        private Coroutine _unloadCoroutine;

        [Inject]
        private void Init(CustomerQueue queue, References references)
        {
            _references = references;
            _queue = queue;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _unloadCoroutine = StartCoroutine(UnloadLoop());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StopCoroutine(_unloadCoroutine);
            }
        }

        private IEnumerator UnloadLoop()
        {
            Inventory inventory = GameManager.Instance.References.PlayerInventory;

            while (inventory.FollowerCount < inventory.MaxFollowerSize)
            {
                yield return new WaitForSeconds(.25f);

                if (_queue.TryGetFollower(out Follower follower))
                {
                    follower.FollowPlayer(inventory, true);
                    Unlock();
                }
            }
        }

        private void Unlock()
        {
            IUnlockable unlock = GetComponent<IUnlockable>();
            if (unlock != null && !unlock.HasUnlockedBefore())
            {
                unlock.UnlockObject();
            }
        }
    }
}