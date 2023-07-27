using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Taxi.NPC
{
    public class WandererMoneySpawner : MonoBehaviour
    {
        [SerializeField] GameObject _collectableMoney;
        [SerializeField] float _moneySpawnTime;

        private void Start() => StartCoroutine(SpawnMoneyLoop());

        IEnumerator SpawnMoneyLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_moneySpawnTime);
                Transform spawnedItem = GameObject.Instantiate(_collectableMoney, transform).transform;
                Tween(spawnedItem);

            }
        }

        void Tween(Transform trans)
        {
            trans.parent = null;
            Vector3[] path = {  transform.position,
                            new Vector3(transform.position.x, 2f, transform.position.z),
                            new Vector3(transform.position.x + GetNoise(), 0f, transform.position.z + GetNoise())
                        };

            trans.DOPath(path, 1f, PathType.CatmullRom);
            trans.DORotate(new Vector3(0f, Random.Range(0f, 360f), 0f), 1f);
        }

        float GetNoise()
        {
            return Random.Range(0f, 1f);
        }
    }
}