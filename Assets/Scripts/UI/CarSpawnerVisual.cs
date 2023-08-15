using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CarSpawnerVisual : MonoBehaviour
{
    [SerializeField] private float _spawnRate, _initialSpawnTime;
    [SerializeField] private GameObject _sliderObject;
    private IFeedbackVisual _fillableSlider;
    private VehicleSpot _spot;

    [Inject]
    protected void Init(IFeedbackVisual visual, VehicleSpot spawner)
    {
        _fillableSlider = visual;
        _spot = spawner;
    }
    private void Start()
    {
        StartCoroutine(WaitLoop(_initialSpawnTime));
        _spot.OnVehicleDeparted += VehicleSpot_VehicleDepartedHandler;
    }

    private void VehicleSpot_VehicleDepartedHandler(int val)
    {
        StartCoroutine(WaitLoop(_spawnRate));
    }
    public IEnumerator WaitLoop(float time)
    {
        _sliderObject.SetActive(true);
        _fillableSlider.SetValue(0f, 1f);

        float maxTime = time;
        float step = Globals.TIME_STEP;

        while (time > 0f)
        {
            time -= step;

            yield return new WaitForSeconds(step);

            _fillableSlider.SetValue(maxTime - time, maxTime);
        }

        _sliderObject.SetActive(false);
    }
}
