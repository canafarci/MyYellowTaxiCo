using System.Collections.Generic;
using TaxiGame.WaitZones;
using UnityEngine;

namespace TaxiGame.UI
{
    public class WaitToSpawnItemZoneVisual : MonoBehaviour
    {
        private readonly Dictionary<GameObject, GameObject> _sliderLookup = new();
        private readonly Dictionary<GameObject, Material> _materialLookup = new();
        private WaitToSpawnItemZone _waitZone;

        private void Awake()
        {
            _waitZone = GetComponent<WaitToSpawnItemZone>();
        }

        private void OnEnable()
        {
            _waitZone.OnWaitEngineIteration += WaitEngine_WaitEngineIterationHandler;
            _waitZone.OnChangeSliderActivation += WaitToSpawnItemZone_ChangeActivationHandler;
        }

        private void WaitToSpawnItemZone_ChangeActivationHandler(object sender, OnChangeSliderActivationEventArgs e)
        {
            SetSliderActivation(e.Instigator, e.Active);
        }

        private void WaitEngine_WaitEngineIterationHandler(object sender, WaitEngineIterationEventArgs e)
        {
            SetSliderValue(e.Instigator, e.CurrentValue, e.MaxValue);
        }

        private GameObject GetOrCreateSlider(GameObject parent)
        {
            if (!_sliderLookup.TryGetValue(parent, out GameObject slider))
            {
                slider = parent.GetComponent<ComponentReference>().Slider;
                _sliderLookup[parent] = slider;
            }
            return slider;
        }

        private Material GetOrCreateMaterial(GameObject parent)
        {
            if (!_materialLookup.TryGetValue(parent, out Material mat))
            {
                mat = parent.GetComponent<ComponentReference>().Slider.transform.GetChild(1).GetComponent<Renderer>().material;
                _materialLookup[parent] = mat;
            }
            return mat;
        }

        private void SetSliderActivation(GameObject parent, bool active)
        {
            GameObject slider = GetOrCreateSlider(parent);
            slider.SetActive(active);
            ResetSliderValue(parent);
        }

        private void ResetSliderValue(GameObject parent)
        {
            Material mat = GetOrCreateMaterial(parent);
            mat.SetFloat("_ClipUvUp", 0f);
        }

        private void SetSliderValue(GameObject parent, float remainingTime, float totalTime)
        {
            GameObject slider = GetOrCreateSlider(parent);
            slider.SetActive(true);

            Material mat = GetOrCreateMaterial(parent);
            mat.SetFloat("_ClipUvUp", remainingTime / totalTime);
        }

        private void OnDisable()
        {
            _waitZone.OnWaitEngineIteration -= WaitEngine_WaitEngineIterationHandler;
            _waitZone.OnChangeSliderActivation -= WaitToSpawnItemZone_ChangeActivationHandler;
        }
    }
}
