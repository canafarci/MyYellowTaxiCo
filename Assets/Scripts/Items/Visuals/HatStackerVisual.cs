using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Visuals;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items.Visual
{
    public class HatStackerVisual : MonoBehaviour
    {
        private StackPositionCalculator _positionCalculator;
        private HatStacker _hatStacker;
        private TweeningService _tweeningService;

        [Inject]
        private void Init(StackPositionCalculator positionCalculator,
                          HatStacker stacker,
                          TweeningService tweenService)
        {
            _positionCalculator = positionCalculator;
            _hatStacker = stacker;
            _tweeningService = tweenService;
        }

        private void Start()
        {
            _hatStacker.OnHatStacked += HatStacker_HatStackedHandler;
        }

        private void HatStacker_HatStackedHandler(object sender, OnHatStackedArgs e)
        {
            e.Item.transform.parent = transform;

            Sequence seq = GenerateStackSequence(e.ItemStack, e.Item);

            seq.onComplete = () => e.OnTweenComplete();
        }

        public Sequence GenerateStackSequence(Stack<StackableItem> itemStack, StackableItem item)
        {
            Sequence moveSequence = GenerateMoveSequence(itemStack, item);

            Sequence endSequence = _tweeningService.GenerateChangeScaleSequence(item.transform, 1.2f, 1f, 0.25f);

            Sequence totalSequence = DOTween.Sequence();
            totalSequence.Append(moveSequence);
            totalSequence.Append(endSequence);

            return totalSequence;
        }


        private Sequence GenerateMoveSequence(Stack<StackableItem> itemStack, StackableItem item)
        {
            Vector3 endPos = _positionCalculator.CalculatePosition(itemStack, item);

            Sequence moveSequence = _tweeningService.GenerateMoveSequenceWithRandomRotation(item.transform, endPos, 0.5f);

            return moveSequence;
        }
    }
}
