using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.WaitZones;
using UnityEngine;
namespace TaxiGame.UI
{
    public class SliderVisual : MonoBehaviour
    {
        Dictionary<GameObject, GameObject> _sliderLookup = new();
        Dictionary<GameObject, Material> _materialLookup = new();
        private WaitToSpawnItemZone _waitZone;
        private void Awake()
        {
            _waitZone = GetComponent<WaitToSpawnItemZone>();
        }
        private void OnEnable()
        {
            _waitZone.OnWaitEngineIteration += UpdateVisual;
            _waitZone.OnChangeSliderActivation += ChangeActivation;
        }
        private void ChangeActivation(object sender, OnChangeSliderActivationEventArgs e)
        {
            if (e.Active)
                Show(e.Instigator);
            else
                Hide(e.Instigator);
        }

        private void UpdateVisual(object sender, WaitEngineIterationEventArgs e)
        {
            SetValue(e.Instigator, e.CurrentValue, e.MaxValue);
        }

        private void Show(GameObject parent)
        {
            GameObject slider = AddSliderToLookup(parent);
            slider.SetActive(true);
        }

        private void SetValue(GameObject parent, float remainingTime, float totalTime)
        {
            Material mat = CheckAndGetMaterial(parent);

            float progress = remainingTime / totalTime;
            mat.SetFloat("_ClipUvUp", progress);
        }

        private Material CheckAndGetMaterial(GameObject parent)
        {
            Material mat;
            if (_materialLookup.ContainsKey(parent))
                mat = _materialLookup[parent];
            else
                mat = AddMaterialToLookup(parent);
            return mat;
        }

        private Material AddMaterialToLookup(GameObject parent)
        {
            Material mat;
            GameObject slider;

            slider = CheckAndGetSlider(parent);

            mat = slider.transform.GetChild(1).GetComponent<Renderer>().material;
            _materialLookup[parent] = mat;
            return mat;
        }

        private GameObject CheckAndGetSlider(GameObject parent)
        {
            GameObject slider;
            if (_sliderLookup.ContainsKey(parent))
                slider = _sliderLookup[parent];
            else
                slider = AddSliderToLookup(parent);
            return slider;
        }

        private GameObject AddSliderToLookup(GameObject parent)
        {
            GameObject slider = parent.GetComponent<ComponentReference>().Slider;
            _sliderLookup[parent] = slider;
            return slider;
        }

        private void Hide(GameObject parent)
        {
            GameObject slider = _sliderLookup[parent];
            slider.SetActive(false);
        }
        //cleanup
        private void OnDisable()
        {
            _waitZone.OnWaitEngineIteration -= UpdateVisual;
            _waitZone.OnChangeSliderActivation -= ChangeActivation;
        }
    }
}