using System.Collections;
using TaxiGame.Animations;
using TaxiGame.NPC.Command;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class Follower : MonoBehaviour
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private NPCActionInvoker _npc;
        protected Coroutine _followLoop;

        [Inject]
        private void Init(NPCActionInvoker npc, NavMeshAgent agent)
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