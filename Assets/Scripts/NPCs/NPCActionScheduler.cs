using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class NPCActionScheduler : MonoBehaviour
    {
        public InventoryObjectType Hat;
        private Coroutine _performedActions = null;
        private Queue<IEnumerator> _actionsToPerform = new();

        public event EventHandler<OnNPCAnimationStateChangedArgs> OnNPCAnimationStateChanged;
        public void AddToActionQueue(IEnumerator actionToPerform)
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
                yield return _actionsToPerform.Dequeue();
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
    public enum ActionPriority
    {
        Normal,
        High
    }
    public class OnNPCAnimationStateChangedArgs : EventArgs
    {
        public int AnimationStateHash;
        public bool State;
    }
}