using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicle
{
    public class VehicleModel : MonoBehaviour
    {
        [SerializeField] private bool _isBroken;
        [SerializeField] private int _moneyToGain;

        public int GetMoneyStackCount() => _moneyToGain;
        public bool IsCarBroken() => _isBroken;
    }
}
