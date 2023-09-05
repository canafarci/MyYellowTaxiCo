using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Animations;
using TaxiGame.Items;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class Follower : MonoBehaviour
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private NPCActionScheduler _npc;
        protected Coroutine _followLoop;

        [Inject]
        private void Init(NPCActionScheduler npc, NavMeshAgent agent)
        {
            _agent = agent;
            _npc = npc;
        }

        public void StopFollowing()
        {
            StopCoroutine(_followLoop);
        }

        protected IEnumerator FollowLoop()
        {
            while (_agent != null)
            {
                _agent.destination = _target.position;
                yield return new WaitForSeconds(.25f);
            }
        }

        public void FollowPlayer(Transform target)
        {
            _npc.ClearAllActions();

            _target = target;
            _followLoop = StartCoroutine(FollowLoop());

            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
        }
    }
}