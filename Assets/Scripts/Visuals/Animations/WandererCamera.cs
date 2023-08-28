using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class WandererCamera : MonoBehaviour
{
    [SerializeField] float _camDuration;
    GameObject _camera;
    private void Start()
    {
        if (PlayerPrefs.HasKey("WandererSpawnCamUsed")) return;

        CinemachineVirtualCamera vcam = FindObjectsOfType<CinemachineVirtualCamera>(true).First(x => x.gameObject.name == "Wanderer Camera");
        _camera = vcam.gameObject;
        vcam.m_LookAt = transform;
        vcam.m_Follow = transform;
        StartCoroutine(CamCoroutine());
        PlayerPrefs.SetInt("WandererSpawnCamUsed", 1);
    }

    IEnumerator CamCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _camera.SetActive(true);
        yield return new WaitForSeconds(_camDuration);
        _camera.SetActive(false);
    }
}
