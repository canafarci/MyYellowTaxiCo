using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    [SerializeField] int _levelToUnlock;
    LevelProgress _levelProgress;
    private void Awake()
    {
        _levelProgress = FindObjectOfType<LevelProgress>();
    }
    private void OnEnable() => _levelProgress.LevelIncreaseHandler += OnLevelIncrease;
    private void OnDisable() => _levelProgress.LevelIncreaseHandler -= OnLevelIncrease;

    private void OnLevelIncrease(int level)
    {
        if (_levelToUnlock <= level)
            GetComponent<IUnlockable>().UnlockObject();
    }
}
