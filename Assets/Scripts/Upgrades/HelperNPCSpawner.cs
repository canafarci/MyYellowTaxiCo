using System.Collections;
using System.Collections.Generic;
using Taxi.NPC;
using UnityEngine;
using Zenject;

namespace Taxi.Upgrades
{
    public class HelperNPCSpawner : MonoBehaviour, IUpgradeReceiver
    {
        [SerializeField] private Transform _spawnPoint;
        private int _npcMaxCount, _currentNPCCount;

        private HatHelperNPC.Factory _npcFactory;

        [Inject]
        private void Init(HatHelperNPC.Factory npcFactory)
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
                HatHelperNPC npc = _npcFactory.Create(_spawnPoint.position);
                _currentNPCCount++;
                yield return new WaitForSeconds(1f);
            }
        }


    }
}
