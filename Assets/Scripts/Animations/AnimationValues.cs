using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.Animations
{
    public class AnimationValues
    {
        public readonly int MoveEmptyHash = Animator.StringToHash("MoveEmptyHands");
        public readonly int IdleHash = Animator.StringToHash("Idle");
        public const string IS_SITTING = "IsSitting";
        public const string ENTERING_CAR = "EnteringCar";
    }
}
