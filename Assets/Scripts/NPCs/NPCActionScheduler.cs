using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class NPCActionScheduler : MonoBehaviour
    {
        public Enums.StackableItemType Hat;
        private Coroutine _performedActions = null;
        private Queue<IEnumerator> _actionsToPerform = new();

        public event EventHandler<OnNPCAnimationStateChangedArgs> OnNPCAnimationStateChanged;

        [Inject]
        private void Init(Vector3 spawnPos, Quaternion rotation)
        {
            transform.position = spawnPos;
            transform.rotation = rotation;
        }

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

        public void InvokeAnimationStateChangedEvent(string animationStateString, bool state)
        {
            OnNPCAnimationStateChanged?.Invoke(this, new OnNPCAnimationStateChangedArgs
            {
                AnimationStateString = animationStateString,
                State = state
            });
        }
        public class Factory : PlaceholderFactory<Vector3, Quaternion, NPCActionScheduler>
        {

        }
    }
    public enum ActionPriority
    {
        Normal,
        High
    }
    public class OnNPCAnimationStateChangedArgs : EventArgs
    {
        public string AnimationStateString;
        public bool State;
    }
}