using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public List<CinemachineVirtualCamera> Cameras = new List<CinemachineVirtualCamera>();
    private void OnEnable() => SceneManager.activeSceneChanged += OnSceneChange;
    private void OnDisable() => SceneManager.activeSceneChanged -= OnSceneChange;

    private void Start()
    {
        AddCamerasToList();
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        AddCamerasToList();
    }

    private void AddCamerasToList()
    {
        Cameras = new List<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera cam in FindObjectsOfType<CinemachineVirtualCamera>(true))
        {
            Cameras.Add(cam);
        }
    }

    public GameObject ActivateCamera(string objectName)
    {
        GameObject returnObject = null;
        for (int i = 0; i < Cameras.Count; i++)
        {
            if (Cameras[i].name == objectName)
            {
                Cameras[i].Priority = 10;
                returnObject = Cameras[i].gameObject;
            }

            else
                Cameras[i].Priority = 0;
        }
        return returnObject;
    }
}