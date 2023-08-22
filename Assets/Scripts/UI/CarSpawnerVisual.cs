using System.Collections;
using TaxiGame.Vehicle;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visual
{
    public class CarSpawnerVisual : MonoBehaviour
    {
        [SerializeField] private float _spawnRate, _initialSpawnTime;
        [SerializeField] private GameObject _sliderObject;
        private IFeedbackVisual _fillableSlider;
        private VehicleSpot _spot;

        private void Start()
        {
            _spot.OnVehicleDeparted += VehicleSpot_VehicleDepartedHandler;

            StartCoroutine(WaitLoop(_initialSpawnTime));
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

        //initialization
        [Inject]
        protected void Init(IFeedbackVisual visual, VehicleSpot spawner)
        {
            _fillableSlider = visual;
            _spot = spawner;
        }
    }
}

