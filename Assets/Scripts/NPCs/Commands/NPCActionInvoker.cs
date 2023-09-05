using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using TaxiGame.NPC.Command;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class NPCActionInvoker : MonoBehaviour
    {
        public InventoryObjectType Hat;
        private Coroutine _performedActions = null;
        private Queue<INPCCommand> _actionsToPerform = new();

        public event EventHandler<OnNPCAnimationStateChangedArgs> OnNPCAnimationStateChanged;
        public void ClearAllActions()
        {
            if (_performedActions != null)
            {
                StopCoroutine(_performedActions);
            }
            _performedActions = null;
            _actionsToPerform.Clear();
        }
        public void AddToActionQueue(INPCCommand actionToPerform)
        {
            _actionsToPerform.Enqueue(actionToPerform);

            if (_performedActions == null)
            {
                _performedActions = StartCoroutine(PerformActions());
            }
        }

        private IEnumerator PerformActions()
        {
            while (_actionsToPerform.Count > 0)
            {
                yield return _actionsToPerform.Dequeue().Execute();
            }

            _performedActions = null;
        }

        public void InvokeAnimationStateChangedEvent(int animationStateHash, bool state)
        {
            OnNPCAnimationStateChanged?.Invoke(this, new OnNPCAnimationStateChangedArgs
            {
                AnimationStateHash = animationStateHash,
                State = state
            });
        }
    }
    public class OnNPCAnimationStateChangedArgs : EventArgs
    {
        public int AnimationStateHash;
        public bool State;
    }
}