using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnTransform;
        private INPCQueue _queue;
        private NavMeshMover.Factory _followerFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Follower)] NavMeshMover.Factory factory)
        {
            _followerFactory = factory;
        }
        private void Awake()
        {
            _queue = GetComponent<CustomerQueue>();
        }

        private void Start()
        {
            CreateFollower();
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnRate);

                if (!_queue.QueueIsFull())
                    CreateFollower();
            }
        }

        private void CreateFollower()
        {
            RiderNPC npc = _followerFactory.Create(_spawnTransform.position, _spawnTransform.rotation).GetComponent<RiderNPC>();

            _queue.AddToQueue(npc);
        }
    }
}