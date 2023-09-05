using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC.Command
{
    public class RiderNPCController : NavMeshMover
    {
        public void MoveAndSit(Transform destination)
        {
            //defined in the NavMeshMover class
            Move(destination);
            //Create sitting command
            Action animationEvent = () =>
            {
                _invoker.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, true);
            };

            INPCCommand command = new SitNPCCommand(_agent, animationEvent, destination);
            _invoker.AddToActionQueue(command);
        }
        public void GoToVehicleSpot(Transform destination, Action onNPCReachedCar = null)
        {
            //defined in the NavMeshMover class
            Move(destination);

            Action animationEvent = () =>
            {
                _invoker.InvokeAnimationStateChangedEvent(AnimationValues.CAR_ENTER, true);
            };

            INPCCommand command = new ReachCarNPCCommand(animationEvent, onNPCReachedCar, gameObject);
            _invoker.AddToActionQueue(command);
        }
    }
}
