using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.NPC;
using TaxiGame.Visuals;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items.Visual
{
    public class HatStackerVisual : MonoBehaviour
    {
        //Dependencies
        private StackPositionCalculator _positionCalculator;
        private HatPickupTrigger _hatPickupTrigger;
        private DriverHatDistributor _driverHatDistributor;
        private HatStacker _hatStacker;
        private TweeningService _tweeningService;

        [Inject]
        private void Init([InjectOptional] HatPickupTrigger hatPickupTrigger,
                          [InjectOptional] DriverHatDistributor driverHatDistributor,
                          HatStacker stacker,
                          StackPositionCalculator positionCalculator,
                          TweeningService tweenService)
        {
            _hatPickupTrigger = hatPickupTrigger;
            _driverHatDistributor = driverHatDistributor;
            _hatStacker = stacker;
            _positionCalculator = positionCalculator;
            _tweeningService = tweenService;
        }

        private void Start()
        {
            _hatStacker.OnHatStacked += HatStacker_HatStackedHandler;

            if (_hatPickupTrigger != null)
            {
                _hatPickupTrigger.OnHatPickedUp += HatPickupTrigger_HatPickedUpHandler;
            }
            if (_driverHatDistributor != null)
            {
                _driverHatDistributor.OnHatDistributed += DriverHatDistributor_HatDistributedHandler;
            }
        }

        private void DriverHatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
            StartCoroutine(HatPoppedFromStack());
        }

        private void HatPickupTrigger_HatPickedUpHandler()
        {
            StartCoroutine(HatPoppedFromStack());
        }

        private IEnumerator HatPoppedFromStack()
        {
            yield return new WaitForEndOfFrame();
        }

        private void HatStacker_HatStackedHandler(object sender, OnHatStackedArgs e)
        {
            e.Item.transform.SetParent(transform);

            Sequence stackSequence = GenerateStackSequence(_hatStacker.GetItemStack(), e.Item);
            stackSequence.onComplete = () => e.OnStackCompleteDelegate();
        }

        private Sequence GenerateStackSequence(Stack<StackableItem> hatStack, StackableItem item)
        {
            Sequence moveSequence = GenerateMoveSequence(hatStack, item);

            Sequence endSequence = _tweeningService.GenerateChangeScaleSequence(item.transform, 1.2f, 1f, 0.25f);

            Sequence totalSequence = DOTween.Sequence();
            totalSequence.Append(moveSequence);
            totalSequence.Append(endSequence);

            return totalSequence;
        }

        private Sequence GenerateMoveSequence(Stack<StackableItem> hatStack, StackableItem item)
        {
            Vector3 endPos = _positionCalculator.CalculatePosition(hatStack, item);

            Sequence moveSequence = _tweeningService.GenerateMoveSequenceWithRandomRotation(item.transform, endPos, 0.5f);

            return moveSequence;
        }
    }
}
