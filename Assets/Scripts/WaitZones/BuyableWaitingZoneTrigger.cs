﻿using UnityEngine;
using System;
using Zenject;
using TaxiGame.GameState.Unlocking;

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