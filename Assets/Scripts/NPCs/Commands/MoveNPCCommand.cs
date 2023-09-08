using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace TaxiGame.NPC.Command
{
    public class MoveNPCCommand : INPCCommand
    {
        private NavMeshAgent _agent;
        private Action _animationEvent;
        private Vector3 _target;

        public MoveNPCCommand(NavMeshAgent agent, Action animationEvent, Vector3 target)
        {
            _agent = agent;
            _animationEvent = animationEvent;
            _target = target;
        }
        public IEnumerator Execute()
        {
            _animationEvent?.Invoke();

            yield return new WaitForSeconds(0.2f); //time for navmesh agent to clean up and initialize

            Vector3 targetPositionWithoutY = new Vector3(_target.x, 0f, _target.z);
            _agent.destination = targetPositionWithoutY;

            Func<bool> agentReachedDestination = HasAgentReachedDestination(targetPositionWithoutY);

            yield return new WaitUntil(agentReachedDestination);
        }

        private Func<bool> HasAgentReachedDestination(Vector3 targetPositionWithoutY)
        {
            return () => Vector3.Distance(GetAgentPositionWithoutY(), targetPositionWithoutY) <= _agent.stoppingDistance;
        }

        private Vector3 GetAgentPositionWithoutY()
        {
            return new Vector3(_agent.transform.position.x, 0f, _agent.transform.position.z);
        }
    }
}
