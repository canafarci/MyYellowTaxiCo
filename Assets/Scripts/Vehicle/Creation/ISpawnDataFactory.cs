using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    public interface ISpawnDataFactory
    {
        public SpawnedCarData CreateCarSpawnData(CarSpawnerID carSpawnerID);
    }
}
