using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
namespace Taxi.NPC
{
    public class SpawnFollower : MonoBehaviour
    {
        [SerializeField] private GameObject _follower;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Transform _spawnTransform;
        private FollowerQueue _queue;
        private bool _hasSpawnedFirst = false;
        private void Awake()
        {
            _queue = GetComponent<FollowerQueue>();
        }

        private void Start() => StartCoroutine(SpawnLoop());

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_hasSpawnedFirst ? _spawnRate : 0f);

                if (_queue.IsFull()) { continue; }

                Vector3 noise = transform.forward * UnityEngine.Random.Range(0.01f, 0.05f);

                Follower follower = GameObject.Instantiate(_follower,
                                                            _spawnTransform.position + noise,
                                                            _follower.transform.rotation).GetComponent<Follower>();

                _queue.AddToQueue(follower);
                _hasSpawnedFirst = true;
            }
        }

    }
}