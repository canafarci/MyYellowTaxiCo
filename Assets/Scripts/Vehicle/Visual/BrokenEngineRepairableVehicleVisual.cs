using System.Collections;
using DG.Tweening;
using TaxiGame.Items;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class BrokenEngineRepairableVehicleVisual : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] GameObject _image;
        [SerializeField] GameObject _fx;
        private BrokenEngineRepairableVehicle _repairableVehicle;
        private TweeningService _tweenService;

        [Inject]
        private void Init(BrokenEngineRepairableVehicle repairableVehicle, TweeningService tweenService)
        {
            _repairableVehicle = repairableVehicle;
            _tweenService = tweenService;
        }
        private void Start()
        {
            BrokenEngineRepairableVehicle.OnPlayerEnteredWithToolbox += BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler;
        }
        private void BrokenEngineRepairableVehicle_PlayerEnteredWithToolboxHandler(object sender, OnPlayerEnteredWithToolboxArgs e)
        {
            if (sender as BrokenEngineRepairableVehicle == _repairableVehicle)
            {
                StartCoroutine(RepairEngineSequence(e.Item, _target));
            }
        }
        private IEnumerator RepairEngineSequence(StackableItem item, Transform target)
        {
            item.transform.parent = transform;

            Sequence moveSequence = _tweenService.GenerateMoveSequence(item.transform, target, Globals.TOOLBOX_DROP_TWEEN_DURATION);

            yield return moveSequence.WaitForCompletion();
            _fx.SetActive(true);
            yield return new WaitForSeconds(Globals.TOOLBOX_DROP_REPAIR_ANIMATION_DURATION);
            _fx.SetActive(false);
            _image.SetActive(false);

            Sequence scaleSequence = _tweenService.GenerateChangeScaleSequence(item.transform, 1.25f, 0.1f, Globals.TOOLBOX_DROP_TWEEN_DURATION);
            scaleSequence.onComplete = () => Destroy(item.gameObject, 0.1f);
        }

    }

}
