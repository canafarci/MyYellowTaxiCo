using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TaxiGame.WaitZones
{
    public class WaitToSpawnItemZone : WaitingEngine
    {
        //Referenced in WaitToSpawnItemZoneVisual class
        public event EventHandler<OnChangeSliderActivationEventArgs> OnChangeSliderActivation;
        private void Start()
        {
            _activationDelay = 0f;
        }
        public override void Begin(Action successAction, GameObject instigator)
        {
            base.Begin(successAction, instigator);
            OnChangeSliderActivation?.Invoke(this, new OnChangeSliderActivationEventArgs
            {
                Instigator = instigator,
                Active = true
            });
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

        protected override void OnSuccess(GameObject instigator)
        {
            base.OnSuccess(instigator);
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