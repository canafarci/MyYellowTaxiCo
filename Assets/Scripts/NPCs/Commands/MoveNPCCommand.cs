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
            Vector3 tarxz = new Vector3(_target.x, 0f, _target.z);
            _agent.destination = tarxz;

            while (_agent != null)
            {
                yield return new WaitForSeconds(0.25f);

                Vector3 posxz = new Vector3(_agent.transform.position.x, 0f, _agent.transform.position.z);

                if (Vector3.Distance(posxz, tarxz) <= _agent.stoppingDistance)
                    break;
            }
        }
    }
}
