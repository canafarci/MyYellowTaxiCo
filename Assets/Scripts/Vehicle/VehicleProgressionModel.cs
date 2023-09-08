using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using TaxiGame.Vehicles.Creation;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles
{
    public class VehicleProgressionModel
    {
        private Dictionary<CarSpawnerID, string> _spawnerToProgressionKeyMap;
        private GameProgressModel _gameProgressionModel;

        [Inject]
        private void Init(GameProgressModel progressionModel)
        {
            _gameProgressionModel = progressionModel;
        }

        private VehicleProgressionModel()
        {
            _spawnerToProgressionKeyMap = new Dictionary<CarSpawnerID, string>
            {
                { CarSpawnerID.FirstYellowSpawner, Globals.FIRST_CHARGER_TUTORIAL_COMPLETE },
                { CarSpawnerID.SecondYellowSpawner, Globals.SECOND_BROKEN_TUTORIAL_COMPLETE },
                { CarSpawnerID.ThirdYellowSpawner, Globals.THIRD_TIRE_TUTORIAL_COMPLETE }
            };
        }

        public bool ShouldSpawnSpecialProgressionCar(CarSpawnerID carSpawner)
        {
            if (_spawnerToProgressionKeyMap.ContainsKey(carSpawner))
            {
                string progressionKey = _spawnerToProgressionKeyMap[carSpawner];
                return !PlayerPrefs.HasKey(progressionKey);
            }
            //Suber and Limo car IDs can be checked as well
            //They do not have progression cars
            else
            {
                return false;
            }
        }

        public void HandleLowGasCarRepaired()
        {
            if (!PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
            {
                _gameProgressionModel.OnFirstCharge();

                PlayerPrefs.SetInt(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE, 1);
            }
        }

        public void HandleBrokenEngineRepaired()
        {
            if (!PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
            {
                _gameProgressionModel.OnSecondRepair();

                PlayerPrefs.SetInt(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE, 1);
            }
        }

        public void HandleFlatTireRepaired()
        {
            if (!PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
            {
                _gameProgressionModel.OnThirdRepair();

                PlayerPrefs.SetInt(Globals.THIRD_TIRE_TUTORIAL_COMPLETE, 1);
            }
        }

        public void HandleCustomerDropped()
        {
            if (!PlayerPrefs.HasKey(Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE))
            {
                _gameProgressionModel.OnFirstCustomerDelivered();
                PlayerPrefs.SetInt(Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE, 1);
            }
        }
        public void HandleVIPTriggered()
        {
            if (!IsVIPTutorialComplete())
            {
                _gameProgressionModel.VIPTriggered();
            }
        }

        public void HandleVIPSpawned(Wanderer wanderer)
        {
            if (!IsVIPTutorialComplete())
                _gameProgressionModel.OnFirstWandererSpawned(wanderer);
        }

        public void HandleHeliDeparted()
        {
            if (!PlayerPrefs.HasKey(Globals.FIFTH_VIP_TUTORIAL_COMPLETE))
            {
                PlayerPrefs.SetInt(Globals.FIFTH_VIP_TUTORIAL_COMPLETE, 1);
            }
        }

        public bool IsVIPTutorialComplete()
        {
            return PlayerPrefs.HasKey(Globals.FIFTH_VIP_TUTORIAL_COMPLETE);
        }
    }
}
