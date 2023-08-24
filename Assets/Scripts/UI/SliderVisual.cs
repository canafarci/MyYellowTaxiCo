using System.Collections.Generic;
using TaxiGame.WaitZones;
using UnityEngine;

namespace TaxiGame.UI
{
    public class SliderVisual : MonoBehaviour
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
            _waitZone.OnWaitEngineIteration += HandleWaitEngineIteration;
            _waitZone.OnChangeSliderActivation += HandleChangeActivation;
        }

        private void HandleChangeActivation(object sender, OnChangeSliderActivationEventArgs e)
        {
            SetSliderActivation(e.Instigator, e.Active);
        }

        private void HandleWaitEngineIteration(object sender, WaitEngineIterationEventArgs e)
        {
            SetSliderValue(e.Instigator, e.CurrentValue, e.MaxValue);
        }

        private void SetSliderActivation(GameObject parent, bool active)
        {
            GameObject slider = GetOrCreateSlider(parent);
            slider.SetActive(false);
            ResetSliderValue(parent);
        }

        private void SetSliderValue(GameObject parent, float remainingTime, float totalTime)
        {
            Material mat = GetOrCreateMaterial(parent);
            mat.SetFloat("_ClipUvUp", remainingTime / totalTime);
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

        private GameObject GetOrCreateSlider(GameObject parent)
        {
            if (!_sliderLookup.TryGetValue(parent, out GameObject slider))
            {
                slider = parent.GetComponent<ComponentReference>().Slider;
                _sliderLookup[parent] = slider;
            }
            return slider;
        }

        private void ResetSliderValue(GameObject parent)
        {
            Material mat = GetOrCreateMaterial(parent);
            mat.SetFloat("_ClipUvUp", 0f);
        }

        private void OnDisable()
        {
            _waitZone.OnWaitEngineIteration -= HandleWaitEngineIteration;
            _waitZone.OnChangeSliderActivation -= HandleChangeActivation;
        }
    }
}
