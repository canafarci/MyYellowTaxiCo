using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace TaxiGame.Animations
{
    public class HelperAnimations : Animations
    {
        private NavMeshAgent _agent;

        [Inject]
        private void Init(NavMeshAgent agent)
        {
            _agent = agent;
        }
        protected override void Start()
        {
            base.Start();
            _upgradeReceiver.OnNPCSpeedUpgrade += ModifierUpgradesReceiver_NPCSpeedUpgradeHandler;
        }

        private void ModifierUpgradesReceiver_NPCSpeedUpgradeHandler(float speed)
        {
            _animator.speed = speed / AnimationValues.HELPER_NPC_BASE_SPEED;
        }

        private void FixedUpdate()
        {
            float speedNormalized = _agent.velocity.magnitude / _agent.speed;
            _animator.SetFloat(AnimationValues.MOVE_FLOAT, speedNormalized);
        }
    }
}

