using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Animations
{
    public class AnimationValues
    {
        // CHARACTERS
        public static readonly int MoveEmptyHash = Animator.StringToHash("MoveEmptyHands");
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int IS_SITTING = Animator.StringToHash("IsSitting");
        public static readonly int ENTERING_CAR = Animator.StringToHash("EnteringCar");
        //VEHICLE
        public static readonly int WOBBLE_IN = Animator.StringToHash("WobbleParkIn");
        public static readonly int PARK_IN = Animator.StringToHash("ParkIn");
    }
}
