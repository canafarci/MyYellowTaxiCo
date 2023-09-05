using System.Collections;
using System.Collections.Generic;
using TaxiGame.Animations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class NavMeshMover : MonoBehaviour
    {
        protected NPCActionScheduler _scheduler;
        private NavMeshAgent _agent;

        [Inject]
        private void Init(NavMeshAgent agent, NPCActionScheduler scheduler)
        {
            _agent = agent;
            _scheduler = scheduler;
        }

        public void Move(Transform destination)
        {
            _scheduler.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            _scheduler.AddToActionQueue(MoveToPosition(destination.position));
        }
        public void Wait(float duration)
        {
            _scheduler.AddToActionQueue(WaitForDuration(duration));
        }

        protected IEnumerator WaitForDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
        protected IEnumerator MoveToPosition(Vector3 pos)
        {
            yield return new WaitForSeconds(0.2f); //time for navmesh agent to clean up and initialize
            Vector3 tarxz = new Vector3(pos.x, 0f, pos.z);
            _agent.destination = tarxz;

            while (_agent != null)
            {
                yield return new WaitForSeconds(0.25f);

                Vector3 posxz = new Vector3(transform.position.x, 0f, transform.position.z);

                if (Vector3.Distance(posxz, tarxz) <= _agent.stoppingDistance)
                    break;
            }
        }


    }
}
