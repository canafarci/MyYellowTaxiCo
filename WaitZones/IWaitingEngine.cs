using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IWaitingEngine
{
    void Begin(WaitZoneConfigSO config);
    void Cancel();
}
