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
        private Dictionary<GameObject, Coroutine> _runs = new();
        private Dictionary<GameObject, WaitZoneConfigSO> _configs = new();

        public virtual void Begin(WaitZoneConfigSO config, GameObject other)
        {
            _runs[other] = StartCoroutine(Run(other));
            _configs[other] = config;
        }
        public virtual void Cancel(GameObject other)
        {
            Assert.IsTrue(_runs.ContainsKey(other) && _runs[other] != null);
            StopCoroutine(_runs[other]);
        }

        private IEnumerator Run(GameObject instigator)
        {
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
            _configs[instigator].OnSuccess();
        }
    }
}