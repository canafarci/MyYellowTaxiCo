using TaxiGame.GameState.Unlocking;
using UnityEngine;

namespace TaxiGame.GameState
{
    public class TutorialTrigger : MonoBehaviour
    {
        IUnlockable _unlocker;

        private void Awake()
        {
            _unlocker = GetComponent<IUnlockable>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _unlocker?.UnlockObject();
            }
        }
    }
}
