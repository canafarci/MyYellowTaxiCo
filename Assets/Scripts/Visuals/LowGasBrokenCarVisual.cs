using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Vehicles.Repair;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaxiGame.Visuals
{
    public class LowGasBrokenCarVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _lowGasIndicator;
        [SerializeField] private Image[] _lowGasImages;
        [SerializeField] private Color32 _gasFilledColor;
        [SerializeField] Image _carSlider;
        private LowGasBrokenCar _brokenCar;

        [Inject]
        private void Init(LowGasBrokenCar brokenCar)
        {
            _brokenCar = brokenCar;
        }

        private void Start()
        {
            LowGasBrokenCar.OnGasHandleAttachedToCar += LowGasBrokenCar_GasHandleAttachedToCarHandler;
        }

        private void LowGasBrokenCar_GasHandleAttachedToCarHandler(object sender, GasHandleAttachToCarEventArgs e)
        {
            // Check if the sender is the same as the current broken car
            if (sender as LowGasBrokenCar != _brokenCar) { return; }

            StartCoroutine(GasHandleAttached());
        }

        private IEnumerator GasHandleAttached()
        {
            Tween sliderTween = DOTween.To(() => _carSlider.fillAmount, x => _carSlider.fillAmount = x,
                                         1,
                                         Globals.LOW_GAS_CAR_REPAIR_DURATION);

            ChangeIndicatorImageColors();

            yield return sliderTween.WaitForCompletion();

            _lowGasIndicator.SetActive(false);

        }

        private void ChangeIndicatorImageColors()
        {
            foreach (Image image in _lowGasImages)
            {
                Sequence sequence = DOTween.Sequence();
                //1 /4 of the time to change to white, 3/4 to change to green
                sequence.Append(image.DOColor(Color.white, Globals.LOW_GAS_CAR_REPAIR_DURATION / 4f));
                sequence.Append(image.DOColor(_gasFilledColor, Globals.LOW_GAS_CAR_REPAIR_DURATION / 4f * 3f));
            }
        }

    }
}
