using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.NPC.Command;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class DeliverHatState : MonoBehaviour, IHelperNPCState
    {
        //Dependencies
        private HatPickupTrigger _hatPickupTrigger;
        private QueueHatDropTrigger _hatDropTrigger;
        private Inventory _inventory;
        private NavMeshMover _mover;

        //State variables
        private bool _hasPickedUpHats = false;

        [Inject]
        private void Init(HatPickupTrigger hatTrigger,
                          QueueHatDropTrigger hatDropTrigger,
                          Inventory inventory,
                          NavMeshMover mover)
        {
            _hatPickupTrigger = hatTrigger;
            _hatDropTrigger = hatDropTrigger;
            _inventory = inventory;
            _mover = mover;
        }

        private void Start()
        {
            Enter();
        }
        private void Update()
        {
            Tick();
        }
        public void Enter()
        {
            _mover.Move(_hatPickupTrigger.transform);
        }

        public void Tick()
        {
            if (!_hasPickedUpHats && _inventory.IsInventoryFull())
            {
                _hasPickedUpHats = true;
                _mover.Move(_hatDropTrigger.transform);
            }
            else if (_hasPickedUpHats && _inventory.GetStackableItemCountInInventory() == 0)
            {
                Exit();
                Enter();
            }
        }
        public void Exit()
        {
            _hasPickedUpHats = false;
        }

    }
}
