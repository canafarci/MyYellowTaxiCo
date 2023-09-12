using System.Collections;
using UnityEngine;

namespace TaxiGame.GameState.Unlocking
{
    public class SequentialUnlockable : Unlockable
    {
        [SerializeField] private SequentialUnlockable _nextUnlocker;

        public void UnlockSequentially()
        {
            if (HasUnlockedBefore())
                StartCoroutine(UnlockCoroutine());
        }
        private IEnumerator UnlockCoroutine()
        {
            yield return new WaitForEndOfFrame();

            _onUnlock.Invoke();
            _persistentUnlock.Invoke();

            if (_nextUnlocker != null)
            {
                _nextUnlocker.gameObject.SetActive(true);
                _nextUnlocker.UnlockSequentially();
            }
        }
    }
}
