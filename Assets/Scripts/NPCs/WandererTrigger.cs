using System.Collections;
using System.Collections.Generic;
using TaxiGame.GameState;
using TaxiGame.Items;
using TaxiGame.Vehicles;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class WandererTrigger : MonoBehaviour
    {
        private Follower _follower;
        private Collider _collider;
        private ProgressionState _progressionState;
        private Wanderer _wanderer;

        [Inject]
        private void Init(Follower follower,
                            Collider collider,
                            ProgressionState progressionState,
                            Wanderer wanderer)
        {
            _follower = follower;
            _collider = collider;
            _progressionState = progressionState;
            _wanderer = wanderer;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _progressionState.HandleVIPTriggered();
                _collider.enabled = false;
                _follower.FollowPlayer(other.transform);
                other.GetComponent<Inventory>().AddObjectToInventory(_wanderer);
            }
        }
    }
}