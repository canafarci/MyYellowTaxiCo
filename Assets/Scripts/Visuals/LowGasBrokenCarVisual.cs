using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Vehicle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaxiGame.Visual
{
    public class LowGasBrokenCarVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _lowGasIndicator;
        [SerializeField] private Image[] _lowGasImages;

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
            if (sender as LowGasBrokenCar != _brokenCar) { return; }
            StartCoroutine(GasHandleAttached());
        }

        private IEnumerator GasHandleAttached()
        {
            Tween sliderTween = DOTween.To(() => _carSlider.fillAmount, x => _carSlider.fillAmount = x,
                                         1,
                                         Globals.LOW_GAS_CAR_REPAIR_DURATION);

            foreach (Image image in _lowGasImages)
            {
                image.DOColor(Color.green, Globals.LOW_GAS_CAR_REPAIR_DURATION);
            }

            yield return sliderTween.WaitForCompletion();

            _lowGasIndicator.SetActive(false);

        }
    }
}
