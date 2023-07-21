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
    protected IFeedbackVisual _fillable;
    private float _currentSpawnTime;
    protected void Awake()
    {
        _fillable = GetComponent<IFeedbackVisual>();
    }
    private void Start()
    {
        _currentSpawnTime = _spawnRate;
        StartCoroutine(WaitLoop(true));
    }
    public IEnumerator WaitLoop(bool isInitialSpawn)
    {
        _object.SetActive(true);
        _fillable.SetValue(0f, 1f);

        float time;
        if (isInitialSpawn)
            time = _initialSpawnTime;
        else
            time = _spawnRate;

        float maxTime = time;
        float step = .025f;

        while (time > 0f)
        {
            time -= step;

            yield return new WaitForSeconds(step);

            if (_fillable != null)
                _fillable.SetValue(maxTime - time, maxTime);
        }

        _object.SetActive(false);
    }
}
