using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
namespace Taxi.NPC
{
    public class SpawnDriver : MonoBehaviour
    {
        [SerializeField] protected GameObject _follower;
        [SerializeField] Transform _spawnTransform;
        public Enums.StackableItemType HatType;
        DriverQueue _queue;
        private void Awake()
        {
            _queue = FindObjectsOfType<DriverQueue>().First(x => x.ItemType == HatType);
        }
        void Start()
        {
            DriverSpawn(_spawnTransform);
        }
        public void DriverSpawn(Transform spawnTransform)
        {
            //spawn driver and downsize scale
            Driver driver = GameObject.Instantiate(_follower, spawnTransform.position, spawnTransform.transform.rotation).GetComponent<Driver>();
            Vector3 baseScale = driver.transform.lossyScale;
            driver.transform.localScale = Vector3.one * 0.0001f;
            driver.transform.DOScale(baseScale, 0.5f);

            //add driver to the queue
            _queue.GetIntoQueue(driver);
        }
    }
}