using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public void Save(int stage);
    public void Load(Action onLoad);
}
