using System.Collections;
using System.Collections.Generic;
using TaxiGame.Installers;
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
        private HelperNPCLocationReferences _locationReferences;
        private Inventory _inventory;
        private NavMeshMover _mover;

        //State variables
        private bool _hasPickedUpHats = false;

        [Inject]
        private void Init(HelperNPCLocationReferences locationReferences,
                          Inventory inventory,
                          NavMeshMover mover)
        {
            _locationReferences = locationReferences;
            _inventory = inventory;
            _mover = mover;
        }

        public void Enter()
        {
            _mover.Move(_locationReferences.GetHatPickupLocation());
        }

        public void Tick()
        {
            if (!_hasPickedUpHats && _inventory.IsInventoryFull())
            {
                _hasPickedUpHats = true;
                _mover.Move(_locationReferences.GetHatDropLocation());
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
