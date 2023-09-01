using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TaxiGame.Vehicles.Visuals
{
    public class HeliSpotVisual : MonoBehaviour
    {
        [SerializeField] GameObject _npc;
        [SerializeField] Animator _animator;
        private IVehicleEvents _vehicleEvents;

        [Inject]
        private void Init(IVehicleEvents vehicleEvents)
        {
            _vehicleEvents = vehicleEvents;
        }

        private void Start()
        {
            _vehicleEvents.OnVehicleDeparted += VehicleEvents_VehicleDepartedHandler;

            _animator.Play("HeliAnim", 0, 0.5f);
        }

        private void VehicleEvents_VehicleDepartedHandler()
        {
            _animator.Play("HeliAnim", 0, 0f);

            _npc.SetActive(true);

            TweenNPC();
        }

        private void TweenNPC()
        {
            Vector3 baseScale = _npc.transform.localScale;
            _npc.transform.localScale = Vector3.one * 0.00001f;
            _npc.transform.DOScale(baseScale, .4f);
        }
    }
}
