using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    [CreateAssetMenu(fileName = "ProgressionCarSO", menuName = "TaxiGame/Vehicles/ProgressionCarsSO", order = 0)]
    public class ProgressionCarsSO : ScriptableObject
    {
        public GameObject NoChargeTaxi;
        public GameObject BrokenEngineTaxi;
        public GameObject FlatTireTaxi;
    }
}
