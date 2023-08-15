using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TaxiGame.WaitZones
{
    public class WaitToSpawnItemZone : WaitingEngine
    {
        public event EventHandler<OnChangeSliderActivationEventArgs> OnChangeSliderActivation;
        public override void Begin(Action successAction, GameObject instigator)
        {
            base.Begin(successAction, instigator);
            OnChangeSliderActivationEventArgs args = new OnChangeSliderActivationEventArgs { Instigator = instigator, Active = true };
            OnChangeSliderActivation?.Invoke(this, args);
        }
        protected override bool CheckCanContinue(float remainingTime)
        {
            return remainingTime > 0f;
        }
        protected override void Iterate(ref float remainingTime, GameObject instigator)
        {
            remainingTime -= Globals.TIME_STEP;

            RaiseIterationEvent(instigator, remainingTime, _timeToUnlock);
        }
        public override void Cancel(GameObject instigator)
        {
            base.Cancel(instigator);
            OnChangeSliderActivationEventArgs args = new OnChangeSliderActivationEventArgs { Instigator = instigator, Active = false };
            OnChangeSliderActivation?.Invoke(this, args);
        }
    }

    public class OnChangeSliderActivationEventArgs : EventArgs
    {
        public GameObject Instigator;
        public bool Active;
    }
}