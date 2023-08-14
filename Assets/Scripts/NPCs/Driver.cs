using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Taxi.Animations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.NPC
{
    public class Driver : RiderNPC
    {
        [SerializeField] private Transform _hatTransform;
        private bool _hasHat = false;
        //Getter-Setters
        public Transform GetHatTransform() => _hatTransform;
        public void SetHasHat(bool hasHat) => _hasHat = hasHat;
        public bool HasHat() => _hasHat;
    }
}