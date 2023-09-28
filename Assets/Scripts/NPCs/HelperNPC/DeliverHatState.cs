using System.Collections;
using System.Collections.Generic;
using TaxiGame.Installers;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.NPC.Command;
using TaxiGame.Scripts;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class DeliverHatState : MonoBehaviour, IHelperNPCState
    {
        //Dependencies
        private HelperNPCStateMachine _stateMachine;
        private IHelperNPCState _repairCarState;
        private HelperNPCLocationReferences _locationReferences;
        private Inventory _inventory;
        private NavMeshMover _mover;

        //State variables
        private bool _hasPickedUpHats = false;

        [Inject]
        private void Init(HelperNPCStateMachine stateMachine,
                          [Inject(Id = HelperNPCStates.RepairCar)] IHelperNPCState repairCarState,
                          HelperNPCLocationReferences locationReferences,
                          Inventory inventory,
                          NavMeshMover mover)
        {
            _stateMachine = stateMachine;
            _repairCarState = repairCarState;
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
            CheckTransition();

            if (!_hasPickedUpHats && _inventory.IsInventoryFull())
            {
                _hasPickedUpHats = true;
                _mover.Move(_locationReferences.GetHatDropLocation());
            }

        }

        private void CheckTransition()
        {
            if (_hasPickedUpHats && _inventory.GetStackableItemCountInInventory() == 0)
            {
                _stateMachine.TransitionTo(_repairCarState);
            }
        }

        public void Exit()
        {
            _hasPickedUpHats = false;
        }

    }
}
