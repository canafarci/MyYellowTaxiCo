using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Items;
using TaxiGame.NPC;
using TaxiGame.Upgrades;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Animations
{

    public abstract class Animations : MonoBehaviour
    {
        protected Animator _animator;
        protected ModifierUpgradesReceiver _upgradeReceiver;
        [SerializeField] private AnimationClip _idle, _holdingIdle, _running, _holdingRunning, _walking;
        private AnimatorOverrideController _animatorOverrideController;
        private Inventory _inventory;
        private ItemUtility _itemUtility;

        [Inject]
        private void Init(Animator animator,
                          Inventory inventory,
                          ModifierUpgradesReceiver upgradeReceiver,
                          ItemUtility itemUtility)
        {
            _animator = animator;
            _inventory = inventory;
            _itemUtility = itemUtility;
            _upgradeReceiver = upgradeReceiver;

            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;
        }

        protected virtual void Start()
        {
            _inventory.OnInventoryModified += Inventory_InventoryModifiedHandler;

            BrokenEngineRepairableVehicle.OnPlayerEnteredWithToolbox += BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler;

        }

        private void BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler(object sender, OnPlayerEnteredWithToolboxArgs e)
        {
            if (e.Inventory == _inventory)
            {
                TriggerRepairAnimation();
            }
        }

        private void Inventory_InventoryModifiedHandler(object sender, InventoryModifiedArgs e)
        {
            InventoryObjectType objectType = e.InventoryObject.GetObjectType();

            if (objectType == InventoryObjectType.GasHandle)
            {
                HandleGasHandleAnimations(e.ItemCountIsZero);
            }
            else if (_itemUtility.IsStackableObject(objectType))
            {
                HandleStackableItemAnimations(e.ItemCountIsZero);
            }
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
    }

}