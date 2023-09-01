using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TaxiGame.Visuals
{
    public class PhoneBoothMaterialChange : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _default, _active;
        private float _disabledDuration;
        private float _disabledToActiveRatio;
        private float _cycleOffset;

        private void Start()
        {
            SetRandomVariables();

            Invoke(nameof(StartLoop), _cycleOffset);
        }

        private void SetRandomVariables()
        {
            _disabledDuration = Random.Range(1f, 3f);
            _disabledToActiveRatio = Random.Range(0.2f, 1f);
            _cycleOffset = Random.Range(1f, 4f);
        }

        private void StartLoop() => StartCoroutine(ChangeMaterials());

        private IEnumerator ChangeMaterials()
        {
            yield return new WaitForSeconds(_disabledDuration);

            _renderer.material = _active;

            yield return new WaitForSeconds(_disabledDuration / _disabledToActiveRatio);

            _renderer.material = _default;

            StartCoroutine(ChangeMaterials());
        }
    }
}