using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taxi.WaitZones
{
    public abstract class WaitingEngine : MonoBehaviour, IWaitingEngine
    {
        [SerializeField] protected float _timeToUnlock;
        private Dictionary<GameObject, Coroutine> _runs = new Dictionary<GameObject, Coroutine>();
        private Dictionary<GameObject, Action> _configs = new Dictionary<GameObject, Action>();
        public event EventHandler<WaitEngineIterationEventArgs> OnWaitEngineIteration;
        public virtual void Begin(Action onSuccess, GameObject other)
        {
            _configs[other] = onSuccess;
            _runs[other] = StartCoroutine(Run(other));
        }
        public virtual void Cancel(GameObject other)
        {
            StopCoroutine(_runs[other]);
        }

        private IEnumerator Run(GameObject instigator)
        {
            //prevent accidental buys
            yield return new WaitForSeconds(0.5f);

            float remainingTime = _timeToUnlock;
            while (CheckCanContinue(remainingTime))
            {
                Iterate(ref remainingTime, instigator);
                yield return new WaitForSeconds(Globals.WAIT_ZONES_TIME_STEP);
            }
            OnSuccess(instigator);
        }

        protected abstract bool CheckCanContinue(float remainingTime);
        protected abstract void Iterate(ref float remainingTime, GameObject instigator);
        protected virtual void OnSuccess(GameObject instigator)
        {
            _configs[instigator]();
        }
        protected void RaiseIterationEvent(GameObject instigator, float currentValue, float maxValue)
        {
            WaitEngineIterationEventArgs eventArgs = new WaitEngineIterationEventArgs { Instigator = instigator, CurrentValue = currentValue, MaxValue = maxValue };
            OnWaitEngineIteration?.Invoke(this, eventArgs);
        }
#if UNITY_INCLUDE_TESTS
        // Getters-Setters for testing purpose
        public void SetRemainingTime(float time) => _timeToUnlock = time;
#endif
    }

    public class WaitEngineIterationEventArgs : EventArgs
    {
        public GameObject Instigator;
        public float CurrentValue;
        public float MaxValue;
    }
}