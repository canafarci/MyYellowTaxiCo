using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles.Creation
{
    [CreateAssetMenu(fileName = "RegularCarsSO", menuName = "TaxiGame/Vehicles/RegularCarsSO", order = 0)]
    public class RegularCarsSO : ScriptableObject
    {
        public GameObject RegularTaxi;
        public GameObject RegularSuber;
        public GameObject RegularLimo;
    }
}
