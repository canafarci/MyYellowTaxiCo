using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

namespace TaxiGame.Animations
{

    public class Animations : MonoBehaviour
    {
        [SerializeField] private AnimationClip _idle, _holdingIdle, _running, _holdingRunning, _walking;
        private AnimatorOverrideController _animatorOverrideController;
        private Animator _animator;
        private Inventory _inventory;
        private Mover _mover;
        private ModifierUpgradesReceiver _upgradeReceiver;

        private float _moveAnimationSpeed;

        [Inject]
        private void Init(Animator animator, Inventory inventory, ModifierUpgradesReceiver upgradeReceiver, Mover mover)
        {
            _animator = animator;
            _inventory = inventory;
            _mover = mover;
            _upgradeReceiver = upgradeReceiver;

            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;

        }

        private void Start()
        {
            _upgradeReceiver.OnPlayerSpeedUpgrade += ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler;
            _inventory.OnInventoryModified += Inventory_InventoryModifiedHandler;
            _mover.OnMoveDistanceCalculated += Mover_MoveDistanceCalculatedHandler;
        }

        private void Mover_MoveDistanceCalculatedHandler(float distance)
        {
            _moveAnimationSpeed = Mathf.Lerp(_moveAnimationSpeed, distance, 10f * Time.deltaTime);
        }


        private void ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler(float speed)
        {
            _animator.speed = speed / 7f;
        }


        private void Inventory_InventoryModifiedHandler(object sender, InventoryModifiedArgs e)
        {
            if (e.InventoryObject.GetObjectType() == InventoryObjectType.GasHandle)
            {
                HandleGasHandleAnimations(e.ItemCountIsZero);
            }
            else if (e.InventoryObject.GetObjectType() == InventoryObjectType.ToolBox)
            {
                HandleToolboxAnimations(e.ItemCountIsZero);
            }
            else if (e.InventoryObject.GetObjectType() != InventoryObjectType.Follower) // remaining types are all StackableItem types
            {
                HandleStackableItemAnimations(e.ItemCountIsZero);
            }
        }

        private void HandleToolboxAnimations(bool itemCountIsZero)
        {
            if (itemCountIsZero)
                TriggerRepairAnimation();
            else
                SetHoldingAnimations();
        }


        private void HandleStackableItemAnimations(bool itemCountIsZero)
        {
            if (itemCountIsZero)
                ResetWalkingAnimations();
            else
                SetHoldingAnimations();
        }

        private void HandleGasHandleAnimations(bool itemCountIsZero)
        {
            if (itemCountIsZero)
                ResetWalkingAnimations();
            else
                SetWalkingWithHandle();
        }

        private void TriggerRepairAnimation()
        {
            _animator.SetTrigger(AnimationValues.REPAIR_CAR_ENGINE);
            ResetWalkingAnimations();
        }

        private void SetHoldingAnimations()
        {
            _animatorOverrideController["Running"] = _holdingRunning;
            _animatorOverrideController["IDLE"] = _holdingIdle;
        }
        private void SetWalkingWithHandle()
        {
            _animatorOverrideController["Running"] = _walking;
            _animatorOverrideController["IDLE"] = _idle;

        }
        public void ResetWalkingAnimations()
        {
            _animatorOverrideController["Running"] = _running;
            _animatorOverrideController["IDLE"] = _idle;
        }

        private void FixedUpdate()
        {
            _animator.SetFloat(AnimationValues.MOVE_FLOAT, _moveAnimationSpeed);
        }
    }

}