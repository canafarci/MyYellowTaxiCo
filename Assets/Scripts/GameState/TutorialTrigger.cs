using TaxiGame.GameState.Unlocking;
using UnityEngine;
using Zenject;

namespace TaxiGame.GameState
{
    public class TutorialTrigger : MonoBehaviour
    {
        IUnlockable _unlockable;

        [Inject]
        private void Init(IUnlockable unlockable)
        {
            _unlockable = unlockable;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Globals.PLAYER_TAG))
            {
                _unlockable.UnlockObject();
            }
        }
    }
}
