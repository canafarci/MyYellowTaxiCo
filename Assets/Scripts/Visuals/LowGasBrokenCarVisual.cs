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
        [SerializeField] private GameObject _chargeCompletedIndicator;

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
            Tween sliderTween = DOTween.To(() => _carSlider.fillAmount, x => _carSlider.fillAmount = x, 1, 3);

            yield return sliderTween.WaitForCompletion();

            _lowGasIndicator.SetActive(false);
            _chargeCompletedIndicator.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            _chargeCompletedIndicator.SetActive(false);
        }
    }
}
