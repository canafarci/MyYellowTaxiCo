using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class HelperNPCSpawner : MonoBehaviour, IUpgradeReceiver
    {
        [SerializeField] private Transform _spawnPoint;
        private int _npcMaxCount, _currentNPCCount;
        private HatHelperNPC.Factory _npcFactory;

        [Inject]
        private void Init([Inject(Id = NPCType.Helper)] HatHelperNPC.Factory npcFactory)
        {
            _npcFactory = npcFactory;
        }
        public void ReceiveUpgradeCommand(UpgradeType upgradeType, int index)
        {
            if (upgradeType != UpgradeType.HelperNPCCount)
                Debug.LogError("Wrong type of Command received for this receiver!");

            _npcMaxCount = index;

            StartCoroutine(SpawnNPCs());
        }

        private IEnumerator SpawnNPCs()
        {
            while (_currentNPCCount < _npcMaxCount)
            {
                _npcFactory.Create(_spawnPoint.position, _spawnPoint.rotation);
                _currentNPCCount++;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
