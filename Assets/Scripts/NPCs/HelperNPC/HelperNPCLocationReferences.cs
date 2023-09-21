using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class HelperNPCLocationReferences : MonoBehaviour
    {
        [SerializeField] private Transform _hatPickupLocation;
        [SerializeField] private Transform _hatDropLocation;
        //Getters-Setters
        public Transform GetHatPickupLocation() => _hatPickupLocation;
        public Transform GetHatDropLocation() => _hatDropLocation;
    }
}
