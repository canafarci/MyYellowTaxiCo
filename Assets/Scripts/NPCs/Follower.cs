using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
namespace Taxi.NPC
{
    public class Follower : NavMeshNPC
    {
        public Transform Target;
        protected Coroutine _followLoop;
        [SerializeField] protected FollowerCanvas _followerCanvas;

        protected override void Awake()
        {
            base.Awake();
            _followerCanvas.Initialize();
        }

        public override IEnumerator OpenDoorAndGetIn(Vector3 pos)
        {
            StopCoroutine(_followLoop);
            GameManager.Instance.References.PlayerInventory.RemoveFollower(this);
            return base.OpenDoorAndGetIn(pos);
        }
        public IEnumerator MoveToTarget(Vector3 pos)
        {
            StopCoroutine(_followLoop);
            _agent.speed = 12f;

            _agent.destination = pos;

            yield return new WaitForSeconds(GameManager.Instance.References.GameConfig.FollowerDisableDelay);

        }
        protected IEnumerator FollowLoop()
        {
            while (_agent != null)
            {
                _agent.destination = Target.position;
                yield return new WaitForSeconds(.25f);
            }
        }

        virtual public void FollowPlayer(Inventory inventory, bool isInQueue = false)
        {
            if (isInQueue)
            {
                _agent.stoppingDistance = 3f;
                _agent.radius = 0.5f;
                GetComponentInChildren<Animator>().SetBool("IsSitting", false);

                if (_followerCanvas != null)
                    _followerCanvas.Remove();
            }

            inventory.AddFollowerToList(this);
            Target = inventory.transform;
            _followLoop = StartCoroutine(FollowLoop());
        }
    }
}