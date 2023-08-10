using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class SpawnFollower : MonoBehaviour
    {
        [SerializeField] private GameObject _follower;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnTransform;
        private INPCQueue _queue;
        private NPCActionScheduler.Factory _followerFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Follower)] NPCActionScheduler.Factory factory)
        {
            _followerFactory = factory;
        }
        private void Awake()
        {
            _queue = GetComponent<FollowerQueue>();
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

                //if (_queue.IsFull()) { continue; }

                CreateFollower();
            }
        }

        private void CreateFollower()
        {
            RiderNPC npc = _followerFactory.Create(_spawnTransform.position, _spawnTransform.rotation).GetComponent<RiderNPC>();

            if (!_queue.QueueIsFull())
                _queue.AddToQueue(npc);
        }
    }
}