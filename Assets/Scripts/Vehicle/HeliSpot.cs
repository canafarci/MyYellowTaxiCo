using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.GameState.Unlocking;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class HeliSpot : MonoBehaviour, IVehicleEvents
    {
        private VehicleProgressionModel _progressionModel;
        private IUnlockable _unlockable;
        private const int HELI_MONEY_STACK_COUNT = 40;
        public event Action OnVehicleDeparted;
        public event Action<int> OnVehicleMoneyEarned;

        //initialization
        [Inject]
        private void Init(VehicleProgressionModel progressionModel,
                          [InjectOptional] IUnlockable unlockable)
        {
            _progressionModel = progressionModel;
            _unlockable = unlockable;
        }

        public void HandleVIPArrival()
        {
            UpdateGameState();

            _unlockable?.UnlockObject();
        }

        private void UpdateGameState()
        {
            OnVehicleDeparted?.Invoke();
            OnVehicleMoneyEarned?.Invoke(HELI_MONEY_STACK_COUNT);
            _progressionModel.HandleHeliDeparted();
        }
    }
}