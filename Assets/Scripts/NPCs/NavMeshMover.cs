using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class NavMeshMover : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NPCActionScheduler _npc;

        [Inject]
        private void Create(Vector3 spawnPos, Quaternion rotation)
        {
            transform.position = spawnPos;
            transform.rotation = rotation;
        }

        [Inject]
        private void Init(NavMeshAgent agent, NPCActionScheduler npc)
        {
            _agent = agent;
            _npc = npc;
        }

        public void Move(Vector3 pos)
        {
            _npc.AddToActionQueue(MoveToPosition(pos));
        }
        public IEnumerator GetMoveAction(Vector3 pos)
        {
            return MoveToPosition(pos);
        }

        private IEnumerator MoveToPosition(Vector3 pos)
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

        public class Factory : PlaceholderFactory<Vector3, Quaternion, NavMeshMover>
        {
        }
    }
}
