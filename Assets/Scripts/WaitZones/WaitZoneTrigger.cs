using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.WaitZones
{
    public abstract class WaitZoneTrigger : MonoBehaviour
    {
        protected IWaitingEngine _waitEngine;
        private void Awake()
        {
            _waitEngine = GetComponent<IWaitingEngine>();
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

