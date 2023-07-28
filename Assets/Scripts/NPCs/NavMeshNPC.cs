using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
namespace Taxi.NPC
{
    public abstract class NavMeshNPC : MonoBehaviour
    {
        public Enums.StackableItemType Hat;
        protected NavMeshAgent _agent;
        protected Coroutine _currentAction = null;
        virtual protected void Awake() => _agent = GetComponent<NavMeshAgent>();
        protected void StartAction(IEnumerator enumerator)
        {
            if (_currentAction != null)
                StopCoroutine(_currentAction);
            _currentAction = StartCoroutine(enumerator);
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