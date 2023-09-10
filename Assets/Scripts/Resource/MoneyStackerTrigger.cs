using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource
{
    public class MoneyStackerTrigger : MonoBehaviour
    {
        //Dependencies
        private IUnlockable _unlockable;
        private MoneyStacker _stacker;
        //Variable
        private Coroutine _moneyPickupCoroutine = null;
        //Events
        //Subcribed from ResourceTracker and MoneyGainedVisual
        public static event Action MoneyPickupHandler;
        //Subscribed from MoneyStackerVisual
        public EventHandler<OnMoneyPickedUpFromStackArgs> OnMoneyPickedUpFromStack;


        [Inject]
        private void Init([InjectOptional] IUnlockable unlockable, MoneyStacker stacker)
        {
            _unlockable = unlockable;
            _stacker = stacker;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG) && _moneyPickupCoroutine == null)
            {
                _moneyPickupCoroutine = StartCoroutine(Pickup(other.transform));
                _unlockable?.UnlockObject();
            }
        }
        private IEnumerator Pickup(Transform instigator)
        {
            while (_stacker.GetMoneyCountInStack() > 0)
            {
                _stacker.DecrementMoneyCountInStack();

                OnMoneyPickedUpFromStack?.Invoke(this, new OnMoneyPickedUpFromStackArgs { Target = instigator });
                MoneyPickupHandler?.Invoke();

                yield return new WaitForSeconds(Globals.TIME_STEP);
            }

            _moneyPickupCoroutine = null;
        }
    }
    public class OnMoneyPickedUpFromStackArgs : EventArgs
    {
        public Transform Target;
    }
}