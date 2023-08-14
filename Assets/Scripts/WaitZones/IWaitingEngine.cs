using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TaxiGame.WaitZones
{
    public interface IWaitingEngine
    {
        void Begin(Action config, GameObject instigator);
        void Cancel(GameObject instigator);
    }
}