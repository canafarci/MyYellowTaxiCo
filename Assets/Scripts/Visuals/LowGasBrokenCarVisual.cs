using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Vehicle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TaxiGame.Visuals
{
    public class LowGasBrokenCarVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _noGasIcon;
        [SerializeField] private GameObject _fullIcon;
        [SerializeField] private GameObject _chargeIcon;
        [SerializeField] private GameObject _emptyIcon;
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
        }

        private IEnumerator GasHandleAttached()
        {
            Tween tween = DOTween.To(() => _carSlider.fillAmount, x => _carSlider.fillAmount = x, 1, 3);

            yield return tween.WaitForCompletion();

            _fullIcon.SetActive(false);
            _chargeIcon.SetActive(false);
            _emptyIcon.SetActive(false);

            //yield return new WaitForSeconds(0.5f);
            _noGasIcon.SetActive(false);

        }
    }
}
