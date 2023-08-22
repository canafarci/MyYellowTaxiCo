using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TaxiGame.Vehicles;
using Zenject;

namespace TaxiGame.Vehicles.Repair
{
    public class Handle : MonoBehaviour
    {
        private IHandleHolder _gasStationHolder;
        private IHandleHolder _previousHolder;
        private bool _isActive = false;
        public event EventHandler<HandleOwnerChangedArgs> OnHandleOwnerChanged;

        [Inject]
        private void Init(IHandleHolder holder)
        {
            _gasStationHolder = holder;
        }

        private void Start()
        {
            ReturnHandleToGasStation();
        }

        public void ChangeOwner(IHandleHolder newHolder)
        {
            TransferHandleOwnership(newHolder);
            InvokeHandleOwnerChangedEvent(newHolder.GetTransform());
            UpdateActiveStatus(newHolder);
        }

        private void TransferHandleOwnership(IHandleHolder newHolder)
        {
            newHolder.SetHandle(this);
            _previousHolder?.Clear();
            _previousHolder = newHolder;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            float distanceToGasStation = Vector3.Distance(transform.position, _gasStationHolder.GetTransform().position);

            if (distanceToGasStation > Globals.HANDLE_MAX_DISTANCE_FROM_STATION)
            {
                ReturnHandleToGasStation();
            }
        }

        public void ReturnHandleToGasStation()
        {
            ChangeOwner(_gasStationHolder);
        }

        private void InvokeHandleOwnerChangedEvent(Transform newParent)
        {
            OnHandleOwnerChanged?.Invoke(this, new HandleOwnerChangedArgs { Parent = newParent });
        }

        private void UpdateActiveStatus(IHandleHolder newHolder)
        {
            _isActive = newHolder != _gasStationHolder;
        }
    }

    public class HandleOwnerChangedArgs : EventArgs
    {
        public Transform Parent;
    }
}
