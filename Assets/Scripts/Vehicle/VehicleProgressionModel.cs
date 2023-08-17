using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class VehicleProgressionModel : MonoBehaviour
    {
        public Dictionary<CarSpawnerID, string> SpawnerToProgressionKeyMap { get { return _spawnerToProgressionKeyMap; } }
        private Dictionary<CarSpawnerID, string> _spawnerToProgressionKeyMap;

        private VehicleProgressionModel()
        {
            _spawnerToProgressionKeyMap = new Dictionary<CarSpawnerID, string>
            {
                { CarSpawnerID.FirstYellowSpawner, Globals.FIRST_CHARGER_TUTORIAL_COMPLETE },
                { CarSpawnerID.SecondYellowSpawner, Globals.SECOND_BROKEN_TUTORIAL_COMPLETE },
                { CarSpawnerID.ThirdYellowSpawner, Globals.THIRD_TIRE_TUTORIAL_COMPLETE }
            };
        }
    }
}
