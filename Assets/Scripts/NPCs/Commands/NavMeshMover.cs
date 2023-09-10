using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Animations;
using TaxiGame.NPC.Command;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC.Command
{
    public class NavMeshMover : MonoBehaviour
    {
        protected NPCActionInvoker _invoker;
        protected NavMeshAgent _agent;
        [Inject]
        private void Init(NavMeshAgent agent, NPCActionInvoker invoker)
        {
            _agent = agent;
            _invoker = invoker;
        }

        public void Move(Transform destination)
        {
            Action animationEvent = () =>
            {
                _invoker.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            };

            INPCCommand command = new MoveNPCCommand(_agent, animationEvent, destination.position);
            _invoker.AddToActionQueue(command);
        }
        public void Wait(float duration)
        {
            INPCCommand command = new WaitNPCCommand(duration);
            _invoker.AddToActionQueue(command);
        }
    }
}
