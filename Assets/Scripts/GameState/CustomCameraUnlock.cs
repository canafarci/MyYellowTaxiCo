using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraUnlock : MonoBehaviour, ICustomUnlockBehaviour
{
    [SerializeField] string _firstTimeIdentifier;
    [SerializeField] float _cameraDuration;
    [SerializeField] GameObject _camera;
    CameraUnlocker _cameraUnlocker;
    virtual public void OnUnlock()
    {
        if (PlayerPrefs.HasKey(_firstTimeIdentifier)) { return; }
        FindObjectOfType<CameraUnlocker>().StartCameraRoutine(_camera, _cameraDuration);
        PlayerPrefs.SetInt(_firstTimeIdentifier, 1);
    }
}
