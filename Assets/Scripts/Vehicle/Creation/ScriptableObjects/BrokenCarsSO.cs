using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    [CreateAssetMenu(fileName = "BrokenVehiclesSO", menuName = "TaxiGame/Vehicles/BrokenCarsSO", order = 0)]
    public class BrokenCarsSO : ScriptableObject
    {
        public GameObject[] BrokenTaxis;
        public GameObject[] BrokenSubers;
        public GameObject[] BrokenLimos;
    }
}
