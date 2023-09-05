using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class WandererCamera : MonoBehaviour
    {
        [SerializeField] private float _camDuration;
        private CinemachineVirtualCamera _camera;
        private WandererSpawner _wandererSpawner;

        [Inject]
        private void Init(CinemachineVirtualCamera cam, WandererSpawner wandererSpawner)
        {
            _camera = cam;
            _wandererSpawner = wandererSpawner;
        }

        private void Start()
        {
            _wandererSpawner.OnWandererSpawned += WandererSpawner_WandererSpawnedHandler;
        }

        private void WandererSpawner_WandererSpawnedHandler(object sender, OnWandererSpawnedArgs e)
        {
            StartCameraRoutine(e.Target);
        }

        private void StartCameraRoutine(Transform target)
        {
            _camera.m_LookAt = target;
            _camera.m_Follow = target;
            StartCoroutine(CamCoroutine());
        }

        private IEnumerator CamCoroutine()
        {
            _camera.Priority = 100;
            yield return new WaitForSeconds(_camDuration);
            _camera.Priority = 0;
        }
    }
}