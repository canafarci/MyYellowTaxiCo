using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MaterialLoop : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Material _default, _active;
    [SerializeField] float _disabledDuration, _disabledToActiveRatio, _cycleOffset;
    [SerializeField] WaitingItemGenerator _generator;


    private void Start()
    {
        Invoke("StartLoop", _cycleOffset);
    }

    void StartLoop() => StartCoroutine(Loop());

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(_disabledDuration);

        _renderer.material = _active;
        if (_generator != null)
            _generator.IsActive = true;

        yield return new WaitForSeconds(_disabledDuration / _disabledToActiveRatio);

        _renderer.material = _default;
        if (_generator != null)
            _generator.IsActive = false;

        StartCoroutine(Loop());
    }

}
