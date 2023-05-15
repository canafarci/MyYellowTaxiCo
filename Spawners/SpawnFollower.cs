using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class SpawnFollower : MonoBehaviour
{
    [SerializeField] protected GameObject[] _follower;
    [SerializeField] float _spawnRate;
    [SerializeField] Transform _spawnTransform;
    FollowerQueue _queue;
    bool _hasSpawnedFirst = false;
    private void Awake()
    {
        _queue = GetComponent<FollowerQueue>();
    }

    private void Start() => StartCoroutine(SpawnLoop());

    IEnumerator SpawnLoop()
    {
        for (int i = 0; i < Mathf.Infinity; i++)
        {
            yield return new WaitForSeconds(_hasSpawnedFirst ? _spawnRate : 0f);

            if (_queue.IsFull()) { continue; }

            Vector3 noise = transform.forward * UnityEngine.Random.Range(0.01f, 0.05f);

            GameObject prefab = _follower[UnityEngine.Random.Range(0, _follower.Length)];

            Follower follower = GameObject.Instantiate(prefab,
                                                        _spawnTransform.position + noise,
                                                        prefab.transform.rotation).GetComponent<Follower>();

            _queue.AddToQueue(follower);
            _hasSpawnedFirst = true;
        }
    }

}

