using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlockable
{
    public void UnlockObject();
    public bool HasUnlockedBefore();
}