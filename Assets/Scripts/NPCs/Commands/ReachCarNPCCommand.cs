using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Animations;
using UnityEngine;

namespace TaxiGame.NPC.Command
{
    public class ReachCarNPCCommand : INPCCommand
    {
        private Action _animationEvent;
        private Action _onNPCReachedCar;
        private GameObject _npcObject;

        public ReachCarNPCCommand(Action animationEvent, Action onNPCReachedCar, GameObject npcObject)
        {
            _animationEvent = animationEvent;
            _onNPCReachedCar = onNPCReachedCar;
            _npcObject = npcObject;
        }

        public IEnumerator Execute()
        {
            _animationEvent?.Invoke();
            yield return new WaitForSeconds(AnimationValues.CAR_ENTER_ANIM_LENGTH * 2f);
            _onNPCReachedCar?.Invoke();
            UnityEngine.Object.Destroy(_npcObject, 0.1f);
        }
    }
}
