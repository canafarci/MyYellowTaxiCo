using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Items
{
    public class HatGenerator : MonoBehaviour
    {
        [SerializeField] protected float _spawnRate;
        [SerializeField] protected GameObject[] _stackableItem, _stage2Hats, _stage3Hats;
        [SerializeField] protected Transform _startTransform;
        protected Stacker _stacker;
        private bool _spawnedFirst = false;
        private int _spawnUpgradeIndex = 0;
        void Awake()
        {
            _stacker = GetComponent<Stacker>();
        }
        void Start() => StartCoroutine(SpawnLoop());
        public void SpawnItem()
        {
            if (CanSpawnItem())
            {
                StackableItem item = GameObject.Instantiate(_stackableItem[GetRandomIndex()], _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
                _stacker.StackItem(item);
            }
        }
        virtual protected IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnedFirst ? _spawnRate : 0.1f);
                _spawnedFirst = true;

                SpawnItem();

            }
        }

        private int GetRandomIndex()
        {
            if (_spawnUpgradeIndex == 0)
                return 0;
            else if (_spawnUpgradeIndex == 1)
            {
                //3x for yellow, 2x for purp
                int randInt = Random.Range(0, 5);
                if (randInt < 3)
                    return 0;
                else
                    return 1;
            }
            else
            {
                //3x for yellow, 2x for purp, 1x for black
                int randInt = Random.Range(0, 6);
                if (randInt < 3)
                    return 0;
                else if (randInt >= 3 && randInt < 5)
                    return 1;
                else
                    return 2;
            }
        }
        public void SetSecondStageHats()
        {
            _stackableItem = _stage2Hats;
            _spawnUpgradeIndex = 1;
        }
        public void SetThirdStageHats()
        {
            _stackableItem = _stage3Hats;
            _spawnUpgradeIndex = 2;
        }

        private bool CanSpawnItem()
        {
            return _stacker.MaxStackSize >= _stacker.ItemStack.Count;
        }
        public void SetSpawnRate(float rate)
        {
            _spawnRate = rate;
        }

#if UNITY_INCLUDE_TESTS
        public float GetSpawnRate() => _spawnRate;
#endif
    }
}