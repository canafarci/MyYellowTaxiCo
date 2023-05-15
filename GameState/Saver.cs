using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour, ISaveable
{
    [SerializeField] string _identifier;
    public void Load(Action onLoad)
    {
        if (_identifier == null) { return; }
        if (!PlayerPrefs.HasKey(_identifier)) { return; }
        onLoad();
    }

    public void Save(int stage) => PlayerPrefs.SetInt(_identifier, stage);
}
