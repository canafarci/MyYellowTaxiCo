using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToSpawnItemZone : WaitingEngine
{
    private SliderVisual _sliderVisual;

    private void Awake()
    {
        _sliderVisual = GetComponent<SliderVisual>();
    }
    public override void Begin(WaitZoneConfigSO config, GameObject instigator)
    {
        base.Begin(config, instigator);
        _sliderVisual.Show(instigator);
    }

    protected override bool CheckCanContinue(float remainingTime)
    {
        return remainingTime > 0f;
    }

    protected override void Iterate(ref float remainingTime, GameObject instigator)
    {
        remainingTime -= Globals.WAIT_ZONES_TIME_STEP;

        _sliderVisual.SetValue(instigator, remainingTime, _timeToUnlock);
    }

    public override void Cancel(GameObject instigator)
    {
        base.Cancel(instigator);
        _sliderVisual.Hide(instigator);
    }
}

