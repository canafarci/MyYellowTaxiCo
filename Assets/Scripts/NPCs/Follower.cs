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
    public class Follower : MonoBehaviour, IInventoryObject
    {
        public Transform Target;
        protected Coroutine _followLoop;
        [SerializeField] protected FollowerCanvas _followerCanvas;
        private NavMeshAgent _agent;
        private NPCActionScheduler _npc;

        [Inject]
        private void Init(NPCActionScheduler npc, NavMeshAgent agent)
        {
            _agent = agent;
            _npc = npc;

        }

        private void Awake()
        {
            _followerCanvas.Initialize();
        }

        //TODO REFACTOR
        public IEnumerator OpenDoorAndGetIn(Vector3 pos)
        {
            StopCoroutine(_followLoop);
            GameManager.Instance.References.PlayerInventory.RemoveObjectFromInventory(this);
            yield return null;
            //return base.OpenDoorAndGetIn(pos);
        }

        protected IEnumerator FollowLoop()
        {
            while (_agent != null)
            {
                _agent.destination = Target.position;
                yield return new WaitForSeconds(.25f);
            }
        }

        public void FollowPlayer(Inventory inventory, bool isInQueue = false)
        {
            // if (isInQueue)
            // {
            //     _agent.stoppingDistance = 3f;
            //     _agent.radius = 0.5f;
            //     GetComponentInChildren<Animator>().SetBool("IsSitting", false);

            // }
            if (_followerCanvas != null)
                _followerCanvas.Remove();
            inventory.AddObjectToInventory(this);
            Target = inventory.transform;
            _npc.InvokeAnimationStateChangedEvent(AnimationValues.IS_SITTING, false);
            _followLoop = StartCoroutine(FollowLoop());
        }

        public InventoryObjectType GetObjectType() => InventoryObjectType.Follower;

    }
}