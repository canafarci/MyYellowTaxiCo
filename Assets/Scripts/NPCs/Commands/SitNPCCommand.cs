using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace TaxiGame.NPC.Command
{
    public class SitNPCCommand : INPCCommand
    {
        private NavMeshAgent _agent;
        private Transform _destination;
        private Action _animationEvent;
        //this action can be used to add driver to driverlookup, it is created from DriverQueueCoordinator
        private Action _onFinished;

        public SitNPCCommand(NavMeshAgent agent,
                             Transform destination,
                             Action animationEvent,
                             Action onFinished = null)
        {
            _agent = agent;
            _destination = destination;
            _animationEvent = animationEvent;
            _onFinished = onFinished;
        }

        public IEnumerator Execute()
        {
            yield return GetToExactPosition(_destination).WaitForCompletion();
            _animationEvent?.Invoke();

            if (_onFinished != null)
            {
                //delay before changing lookup state
                yield return new WaitForSeconds(0.5f);
                _onFinished.Invoke();
            }
        }

        private Tween GetToExactPosition(Transform destination)
        {
            Tween move = _agent.transform.DOMove(destination.position, .5f);
            _agent.transform.DORotate(destination.rotation.eulerAngles, .5f);
            return move;
        }
    }
}
