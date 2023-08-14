using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.NPC
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private GameObject[] _followerPrefabs;
        private INPCQueue _queue;
        private RiderNPC.Factory _followerFactory;

        [Inject]
        private void Init(RiderNPC.Factory factory)
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
            int randInt = UnityEngine.Random.Range(0, _followerPrefabs.Length);
            GameObject prefab = _followerPrefabs[randInt];

            RiderNPC npc = _followerFactory.Create(prefab,
                                                _spawnTransform.position,
                                                _spawnTransform.rotation);

            _queue.AddToQueue(npc);
        }
    }
}