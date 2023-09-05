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
        private Action _animationEvent;
        private Transform _destination;

        public SitNPCCommand(NavMeshAgent agent, Action animationEvent, Transform destination)
        {
            _agent = agent;
            _animationEvent = animationEvent;
            _destination = destination;
        }

        public IEnumerator Execute()
        {
            yield return GetToExactPosition(_destination).WaitForCompletion();
            _animationEvent?.Invoke();
        }

        private Tween GetToExactPosition(Transform destination)
        {
            Tween move = _agent.transform.DOMove(destination.position, .5f);
            _agent.transform.DORotate(destination.rotation.eulerAngles, .5f);
            return move;
        }
    }
}
