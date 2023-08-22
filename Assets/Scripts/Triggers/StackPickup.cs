using System.Collections;
using System.Collections.Generic;
using Ketchapp.MayoSDK;
using TaxiGame.Characters;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.Events;

public class StackPickup : MonoBehaviour
{
    private bool _secondGiveHatTutorialStarted = false;
    public bool IsHatStacker = false;
    [SerializeField] float _clearStackRate;
    [SerializeField] UnityEvent _onSecondHatTutorialUnlock;
    Stacker _stacker;
    Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
    void Awake() => _stacker = GetComponent<Stacker>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            _coroutines[other] = StartCoroutine(UnloadLoop(other));
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

    IEnumerator UnloadLoop(Collider other)
    {
        Inventory inventory = other.GetComponent<IInventoryHolder>().GetInventory();

        while (true)
        {
            yield return new WaitForSeconds(_clearStackRate);

            if (inventory.StackableItemCapacity > inventory.GetStackableItemCountInInventory()
                                            && _stacker.ItemStack.TryPop(out StackableItem item))
            {
                inventory.AddObjectToInventory(item);

                IUnlockable unlock = GetComponent<IUnlockable>();
                if (unlock != null && !unlock.HasUnlockedBefore())
                {
                    unlock.UnlockObject();
                }

                if (_secondGiveHatTutorialStarted)
                {
                    _onSecondHatTutorialUnlock.Invoke();
                    _secondGiveHatTutorialStarted = false;
                    FindObjectOfType<SecondHatTutorialTrigger>().SecondHatTutorialStarted = true;
                    FindObjectOfType<SecondHatTutorialTrigger>().GetComponent<Collider>().enabled = true;

                    var data = new Ketchapp.MayoSDK.Analytics.Data();
                    data.AddValue("ProgressionStatus", "Completed");
                    data.AddValue("Money", (int)ResourceTracker.Instance.PlayerMoney);
                    KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialStart", data);

                    var dataStart = new Ketchapp.MayoSDK.Analytics.Data();
                    dataStart.AddValue("ProgressionStatus", "Started");
                    dataStart.AddValue("Money", (int)ResourceTracker.Instance.PlayerMoney);
                    KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialGiveHatToDrivers", data);
                }
            }
        }
    }

    //Getters-Setters
    public void SetSecondGiveHatTutorialStarted() => _secondGiveHatTutorialStarted = true;
}
