using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

namespace TaxiGame.Animations
{
    public class PlayerAnimations : Animations
    {
        private Mover _mover;

        private float _moveAnimationSpeed;
        [Inject]
        private void Init(Mover mover)
        {
            _mover = mover;

        }
        protected override void Start()
        {
            base.Start();
            _mover.OnMoveDistanceCalculated += Mover_MoveDistanceCalculatedHandler;
            _upgradeReceiver.OnPlayerSpeedUpgrade += ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler;

        }
        private void Mover_MoveDistanceCalculatedHandler(bool isMoving)
        {
            float target = isMoving ? 1f : 0f;
            _moveAnimationSpeed = Mathf.Lerp(_moveAnimationSpeed, target, 10f * Time.deltaTime);
        }
        private void ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler(float speed)
        {
            _animator.speed = speed / Globals.PLAYER_BASE_SPEED;
        }
        private void FixedUpdate()
        {
            _animator.SetFloat(AnimationValues.MOVE_FLOAT, _moveAnimationSpeed);
        }
    }
}
