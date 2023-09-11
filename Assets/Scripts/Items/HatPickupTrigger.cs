using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.Resource;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
#if UNITY_ANDROID
using Ketchapp.MayoSDK;
#endif

public class HatPickupTrigger : MonoBehaviour
{
    //TODO REFACTOR
    private bool _secondGiveHatTutorialStarted = false;
    [SerializeField] float _clearStackRate;
    [SerializeField] UnityEvent _onSecondHatTutorialUnlock;
    private HatStacker _hatStacker;
    Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
    private ResourceTracker _resourceTracker;
    [Inject]
    private void Init(ResourceTracker tracker, HatStacker stacker)
    {
        _resourceTracker = tracker;
        _hatStacker = stacker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Globals.PLAYER_TAG) || other.CompareTag(Globals.HELPER_NPC_TAG))
        {
            _coroutines[other] = StartCoroutine(UnloadLoop(other));
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

    IEnumerator UnloadLoop(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        while (true)
        {
            yield return new WaitForSeconds(_clearStackRate);

            if (!inventory.IsInventoryFull() && _hatStacker.ItemStack.TryPop(out StackableItem item))
            {
                inventory.AddObjectToInventory(item);

                IUnlockable unlock = GetComponent<IUnlockable>();
                unlock?.UnlockObject();

                if (_secondGiveHatTutorialStarted)
                {
                    _onSecondHatTutorialUnlock.Invoke();
                    _secondGiveHatTutorialStarted = false;
                    FindObjectOfType<SecondHatTutorialTrigger>().SecondHatTutorialStarted = true;
                    FindObjectOfType<SecondHatTutorialTrigger>().GetComponent<Collider>().enabled = true;
#if UNITY_ANDROID
                    var data = new Ketchapp.MayoSDK.Analytics.Data();
                    data.AddValue("ProgressionStatus", "Completed");
                    data.AddValue("Money", (int)_resourceTracker.PlayerMoney);
                    KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialStart", data);

                    var dataStart = new Ketchapp.MayoSDK.Analytics.Data();
                    dataStart.AddValue("ProgressionStatus", "Started");
                    dataStart.AddValue("Money", (int)_resourceTracker.PlayerMoney);
                    KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialGiveHatToDrivers", data);
#endif
                }
            }
        }
    }

    //Getters-Setters
    public void SetSecondGiveHatTutorialStarted() => _secondGiveHatTutorialStarted = true;
}
