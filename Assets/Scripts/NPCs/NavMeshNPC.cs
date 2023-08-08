using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public abstract class NavMeshNPC : MonoBehaviour
    {
        public Enums.StackableItemType Hat;
        protected NavMeshAgent _agent;
        protected Coroutine _currentAction = null;
        public event EventHandler<OnNPCAnimationStateChangedArgs> OnNPCAnimationStateChanged;

        [Inject]
        private void Init(NavMeshAgent agent, Vector3 spawnPos, Quaternion rotation)
        {
            _agent = agent;
            transform.position = spawnPos;
            transform.rotation = rotation;
        }

        protected void StartAction(IEnumerator enumerator)
        {
            if (_currentAction != null)
                StopCoroutine(_currentAction);
            _currentAction = StartCoroutine(enumerator);
        }
        protected IEnumerator MoveToPosition(Vector3 pos)
        {
            yield return new WaitForSeconds(0.2f); //time for navmesh agent to clean up and initialize
            Vector3 tarxz = new Vector3(pos.x, 0f, pos.z);
            _agent.destination = tarxz;

            while (_agent != null)
            {
                yield return new WaitForSeconds(0.25f);

                Vector3 posxz = new Vector3(transform.position.x, 0f, transform.position.z);

                if (Vector3.Distance(posxz, tarxz) <= _agent.stoppingDistance)
                    break;
            }
        }

        protected void InvokeAnimationStateChangedEvent(string animationStateString, bool state)
        {
            OnNPCAnimationStateChanged?.Invoke(this, new OnNPCAnimationStateChangedArgs
            {
                AnimationStateString = animationStateString,
                State = state
            });
        }
        public class Factory : PlaceholderFactory<Vector3, Quaternion, NavMeshNPC>
        {

        }
    }
    public class OnNPCAnimationStateChangedArgs : EventArgs
    {
        public string AnimationStateString;
        public bool State;
    }
}