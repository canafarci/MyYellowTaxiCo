using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.WaitZones
{
    public abstract class WaitZoneTrigger : MonoBehaviour
    {
        protected IWaitingEngine _waitEngine;

        [Inject]
        private void Init(IWaitingEngine waitEngine)
        {
            _waitEngine = waitEngine;
        }
        protected abstract Action GetSuccessAction(Collider other);
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Action successAction = GetSuccessAction(other);
                _waitEngine.Begin(successAction, other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _waitEngine.Cancel(other.gameObject);
            }
        }
    }
}

