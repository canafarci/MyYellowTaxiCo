using DG.Tweening;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class DriverHatDistributorVisual : MonoBehaviour
    {
        private DriverHatDistributor _hatDistributor;
        private TweeningService _tweeningService;

        [Inject]
        private void Init(DriverHatDistributor hatDistributor, TweeningService tweenService)
        {
            _hatDistributor = hatDistributor;
            _tweeningService = tweenService;
        }

        private void Start()
        {
            _hatDistributor.OnHatDistributed += HatDistributor_HatDistributedHandler;
        }

        private void HatDistributor_HatDistributedHandler(object sender, HatDistributedEventArgs e)
        {
            MoveObjectToTransform(e.Item, e.Driver.GetHatTransform());
        }

        private void MoveObjectToTransform(Transform item, Transform target)
        {
            item.SetParent(target);

            Sequence moveSequence = _tweeningService.GenerateMoveSequence(item, target, 0.5f);
            Sequence endSequence = _tweeningService.GenerateChangeScaleSequence(item.transform, 1.2f, 1f, 0.25f);

            Sequence totalSequence = DOTween.Sequence();
            totalSequence.Append(moveSequence);
            totalSequence.Append(endSequence);
        }
    }
}
