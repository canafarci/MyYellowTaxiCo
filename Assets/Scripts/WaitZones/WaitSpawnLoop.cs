using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSpawnLoop : MonoBehaviour
{
    [SerializeField] private float _timeToUnlock;
    private IItemSpawner _itemSpawner;
    private ISliderVisual _sliderVisual;
    private float _remainingTime;

    private void Awake()
    {
        _itemSpawner = GetComponent<IItemSpawner>();
        _sliderVisual = GetComponent<ISliderVisual>();
    }

    public IEnumerator SpawnLoop(Collider other, GameObject slider)
    {
        if (!_itemSpawner.CanSpawnItem(other))
        {
            yield break;
        }
        _remainingTime = _timeToUnlock;

        _sliderVisual.ShowSlider(slider);

        float step = Globals.WAIT_ZONES_TIME_STEP;
        while (_remainingTime > 0f)
        {
            _sliderVisual.UpdateSlider(slider, _remainingTime, _timeToUnlock);
            _remainingTime -= step;
            yield return new WaitForSeconds(step);
        }

        _sliderVisual.HideSlider(slider);
        _itemSpawner.SpawnItem(other);
    }
}

