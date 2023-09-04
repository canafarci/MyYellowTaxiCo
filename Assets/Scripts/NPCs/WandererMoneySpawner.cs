using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using TaxiGame.Items;

namespace TaxiGame.NPC
{
    public class WandererMoneySpawner : MonoBehaviour
    {
        [SerializeField] GameObject _collectableMoney;
        [SerializeField] float _moneySpawnTime;
        private WandererMoney.Factory _factory;

        [Inject]
        private void Init(WandererMoney.Factory factory)
        {
            _factory = factory;
        }

        private void Start() => StartCoroutine(SpawnMoneyLoop());

        IEnumerator SpawnMoneyLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_moneySpawnTime);
                _factory.Create(_collectableMoney, transform);
            }
        }


    }
}