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
    public class GasStationVisual : MonoBehaviour
    {
        [SerializeField] private GameObject _thunder;
        [SerializeField] private GameObject _stationFX;
        [SerializeField] private GameObject _circleUI;
        [SerializeField] private HosePump _hosePump;
        [SerializeField] private Animator _animator;
        [SerializeField] private Material _thunderWithEmission;
        [SerializeField] private Material _thundeWithoutEmission;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Slider _gasStationSlider;

        private GasStation _gasStation;

        [Inject]
        private void Init(GasStation station)
        {
            _gasStation = station;
        }

        private void Start()
        {
            _gasStation.OnGasHandleOwnerChanged += GasStation_GasHandleOwnerChangedHandler;
            LowGasBrokenCar.OnGasHandleAttachedToCar += LowGasBrokenCar_GasHandleAttachedToCarHandler;
        }

        private void LowGasBrokenCar_GasHandleAttachedToCarHandler(object sender, GasHandleAttachToCarEventArgs e)
        {
            if (e.GasHandle != _gasStation.GetOriginalHandle()) return;
            StartCoroutine(GasFillVisual());
        }
        private IEnumerator GasFillVisual()
        {
            GasFillStartVisual();
            yield return new WaitForSeconds(Globals.LOW_GAS_CAR_REPAIR_DURATION);
            GasFillEndVisual();
        }
        private void GasFillStartVisual()
        {
            _stationFX.SetActive(true);
            _hosePump.enabled = true;
            _animator.enabled = true;
            _renderer.material = _thunderWithEmission;
            DOTween.To(() => _gasStationSlider.value, x => _gasStationSlider.value = x,
                                                                                    1,
                                                                                    Globals.LOW_GAS_CAR_REPAIR_DURATION);
        }

        private void GasFillEndVisual()
        {
            _stationFX.SetActive(false);
            _animator.enabled = false;
            _renderer.material = _thundeWithoutEmission;
            _gasStationSlider.value = 0f;
            _hosePump.baseThickness = 0.075f;
            _hosePump.bulgeThickness = 0.075f;
        }



        private void GasStation_GasHandleOwnerChangedHandler(object sender, OnGasHandleOwnerChangedArgs e)
        {
            //TODO add dotween to thunder obj

            if (e.OwnerIsGasStation)
            {
                _thunder.SetActive(false);
                _circleUI.SetActive(true);

            }
            else
            {
                _thunder.SetActive(true);
                _circleUI.SetActive(false);
            }
        }
    }
}
