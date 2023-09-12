using System.Collections;
using UnityEngine;
using Zenject;

namespace TaxiGame.GameState.Unlocking
{
    public class SequentialUnlockable : Unlockable
    {
        [SerializeField] private SequentialUnlockable _nextUnlocker;
        [SerializeField] private UnlockSequence _unlockSequence;
        private ProgressionState _progressionState;

        [Inject]
        private void Init(ProgressionState progressionState)
        {
            _progressionState = progressionState;
        }
        protected override void HandleUnlockedBefore()
        {
            base.HandleUnlockedBefore();

            if (!_progressionState.IsTutorialSequenceComplete(_unlockSequence))
            {
                StartCoroutine(SequentialUnlockCoroutine());
            }
        }

        private IEnumerator SequentialUnlockCoroutine()
        {
            yield return new WaitForEndOfFrame();

            _onUnlock.Invoke();

            if (_nextUnlocker != null)
            {
                _nextUnlocker.gameObject.SetActive(true);
            }
        }
    }

}
