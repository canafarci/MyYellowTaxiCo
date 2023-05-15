using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerPickup : MonoBehaviour
{
    FollowerQueue _queue;
    Coroutine _unloadCoroutine;
    void Awake() => _queue = GetComponent<FollowerQueue>();

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

    IEnumerator UnloadLoop()
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;

        while (true)
        {
            if (inventory.MaxFollowerSize <= inventory.FollowerCount)
            {
                yield return new WaitForSeconds(.25f);
                continue;
            }

            Follower follower;

            follower = _queue.TryUnload();
            if (follower != null)
            {
                follower.FollowPlayer(inventory, true);
                Unlock unlock = GetComponent<Unlock>();
                if (unlock != null && !unlock.HasUnlockedBefore)
                {
                    unlock.UnlockObject();
                }
            }

            yield return new WaitForSeconds(.25f);
        }
    }
}
