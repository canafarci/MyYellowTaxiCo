using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.GameState.Unlocking;
using TaxiGame.Items;
using UnityEngine;
using Zenject;
#if UNITY_ANDROID
using Ketchapp.MayoSDK;
#endif

namespace TaxiGame.Items
{
    public class HatPickupTrigger : MonoBehaviour
    {
        //Dependencies
        private HatStacker _hatStacker;
        private IUnlockable _unlockable;
        //Variables
        private Dictionary<Collider, Coroutine> _coroutines = new Dictionary<Collider, Coroutine>();
        //Constants
        private const float CLEAR_STACK_RATE = 0.25f;
        //Events
        //Subscribed from HatStackerVisual
        public event Action OnHatPickedUp;


        [Inject]
        private void Init(HatStacker stacker, IUnlockable unlockable)
        {
            _hatStacker = stacker;
            _unlockable = unlockable;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG) || other.CompareTag(Globals.HELPER_NPC_TAG))
            {
                _coroutines[other] = StartCoroutine(TryGiveHat(other));
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

        private IEnumerator TryGiveHat(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();

            //loop while instigator is in the trigger
            while (true)
            {
                yield return new WaitForSeconds(CLEAR_STACK_RATE);

                if (CanPickUpHat(inventory, out StackableItem item))
                {
                    inventory.AddObjectToInventory(item);

                    _unlockable?.UnlockObject();
                    OnHatPickedUp?.Invoke();
                }
            }
        }

        private bool CanPickUpHat(Inventory inventory, out StackableItem item)
        {
            item = null;
            return !_hatStacker.IsStackingHat()
                && !inventory.IsInventoryFull()
                && _hatStacker.GetItemStack().TryPop(out item);
        }
    }
}