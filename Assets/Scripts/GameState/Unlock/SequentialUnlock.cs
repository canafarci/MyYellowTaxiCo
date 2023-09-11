using System.Collections;
using UnityEngine;

namespace TaxiGame.GameState.Unlocking
{
    public class SequentialUnlock : UnlockBase
    {
        [SerializeField] bool _isFirst;
        [SerializeField] SequentialUnlock _nextUnlocker;
        private void Start()
        {
            if (_isFirst && HasUnlockedBefore())
            {
                UnlockSequentially();
            }
            else if (!HasUnlockedBefore())
            {
                SendAnalyticsDataForProgressionStart();
            }
        }

        public void UnlockSequentially()
        {
            if (HasUnlockedBefore())
                StartCoroutine(UnlockCoroutine());
        }
        private IEnumerator UnlockCoroutine()
        {
            yield return new WaitForEndOfFrame();
            UnlockObject();
            if (_nextUnlocker != null)
            {
                _nextUnlocker.gameObject.SetActive(true);
                _nextUnlocker.UnlockSequentially();
            }
            if (_isFirst)
                gameObject.SetActive(false);
        }
    }
}
