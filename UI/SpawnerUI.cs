using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerUI : MonoBehaviour
{
    [SerializeField] float _spawnRate, _initialSpawnTime;
    [SerializeField] GameObject _object;
    IFillableUI _fillable;
    float _currentSpawnTime;
    private void Awake()
    {
        _fillable = GetComponent<IFillableUI>();
    }
    private void Start()
    {
        _currentSpawnTime = _spawnRate;
        StartCoroutine(WaitLoop(_initialSpawnTime));
    }
    public IEnumerator WaitLoop(float time = 0f)
    {
        _object.SetActive(true);
        _fillable.SetFill(0f, 1f);
        time = time == 0f ? time = _currentSpawnTime : time;

        float maxTime = time;
        float step = .025f;

        while (time > 0f)
        {
            time -= step;

            yield return new WaitForSeconds(step);

            if (_fillable != null)
                _fillable.SetFill(maxTime - time, maxTime);
        }

        _object.SetActive(false);
    }
}
