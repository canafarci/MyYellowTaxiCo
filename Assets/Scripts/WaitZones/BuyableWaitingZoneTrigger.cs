
using UnityEngine;
using System;

namespace Taxi.WaitZones
{
    public class BuyableWaitingZoneTrigger : WaitZoneTrigger
    {
        protected override Action GetSuccessAction(Collider other)
        {
            return () =>
            {
                IUnlockable unlockable = GetComponent<IUnlockable>();
                unlockable?.UnlockObject();
            };
        }
    }
}