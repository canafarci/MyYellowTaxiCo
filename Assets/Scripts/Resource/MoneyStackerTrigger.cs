using System.Collections;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource
{
    public class MoneyStackerTrigger : MonoBehaviour
    {
        private IUnlockable _unlockable;
        private MoneyStacker _stacker;
        [Inject]
        private void Init([InjectOptional] IUnlockable unlockable, MoneyStacker stacker)
        {
            _unlockable = unlockable;
            _stacker = stacker;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG))
            {
                StartCoroutine(Pickup());
                _unlockable?.UnlockObject();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG))
                StopAllCoroutines();
        }
        IEnumerator Pickup()
        {
            while (true)
            {
                _stacker.StartEmptyStack();
                yield return new WaitForSeconds(0.55f);
            }
        }
    }
}