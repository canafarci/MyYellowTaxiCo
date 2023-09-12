using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CustomCameraUnlock : MonoBehaviour
{
    [SerializeField] private float _cameraDuration;
    private CameraUnlocker _cameraUnlocker;

    [Inject]
    private void Init(CameraUnlocker unlocker)
    {
        _cameraUnlocker = unlocker;
    }
    public void CenterCameraOnUnlock()
    {
        _cameraUnlocker.StartCameraRoutine(gameObject, _cameraDuration);
    }
}
