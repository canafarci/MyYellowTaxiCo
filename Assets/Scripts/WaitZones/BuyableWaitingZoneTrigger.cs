
using UnityEngine;
using System;
using Zenject;

namespace TaxiGame.WaitZones
{
    public class BuyableWaitingZoneTrigger : WaitZoneTrigger
    {
        private IUnlockable _unlockable;

        [Inject]
        private void Init(IUnlockable unlockable)
        {
            _unlockable = unlockable;
        }
        protected override Action GetSuccessAction(Collider other)
        {
            return () =>
            {
                _unlockable?.UnlockObject();
            };
        }
    }
}