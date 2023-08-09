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
        private bool _hasSpawnedFirst = false;

        NavMeshNPC.Factory _followerFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Follower)] NavMeshNPC.Factory factory)
        {
            _followerFactory = factory;
        }
        private void Awake()
        {
            _queue = GetComponent<FollowerQueue>();
        }

        private void Start()
        {

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnRate);

                //if (_queue.IsFull()) { continue; }

                Follower follower = (Follower)_followerFactory.Create(_spawnTransform.position, _spawnTransform.rotation);

                _queue.AddToQueue(follower);
            }
        }

    }
}