using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    /// <summary>
    /// Stores driver objects which are in sitting state
    /// </summary>
    public class DriverLookup : MonoBehaviour
    {
        private HashSet<Driver> _driversWithoutHat = new HashSet<Driver>();
        private HashSet<Driver> _driversWithHat = new HashSet<Driver>();
        public void AddDriverToLookup(Driver driver)
        {
            _driversWithoutHat.Add(driver);
        }

        //Getters-Setters
        public HashSet<Driver> GetDriversWithoutHat() => _driversWithoutHat;
        public HashSet<Driver> GetDriversWithHat() => _driversWithHat;
    }
}
