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
        public static readonly int CAR_ENTER = Animator.StringToHash("EnteringCar");
        public static readonly int REPAIR_CAR_ENGINE = Animator.StringToHash("RepairingEngine");
        public static readonly string MOVE_FLOAT = "MoveSpeed";
        //VEHICLE
        public static readonly int WOBBLE_IN = Animator.StringToHash("WobbleParkIn");
        public static readonly int WOBBLE_OUT = Animator.StringToHash("WobbleParkOut");
        public static readonly int PARK_IN = Animator.StringToHash("ParkIn");
        public static readonly int PARK_OUT = Animator.StringToHash("ParkOut");
        public static readonly int CAR_EXIT = Animator.StringToHash("CarExit");

        public const float PARK_ANIM_LENGTH = 1.83333333f;
        public const float CAR_ENTER_ANIM_LENGTH = .75f;

        public const float HELPER_NPC_BASE_SPEED = 3f;
    }
}
