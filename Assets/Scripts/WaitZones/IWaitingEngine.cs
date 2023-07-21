using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Taxi.WaitZones
{
    public interface IWaitingEngine
    {
        void Begin(WaitZoneConfigSO config, GameObject instigator);
        void Cancel(GameObject instigator);
    }
}