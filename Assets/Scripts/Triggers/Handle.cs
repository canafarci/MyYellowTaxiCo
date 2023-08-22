using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TaxiGame.Vehicle;
using Zenject;

public class Handle : MonoBehaviour
{
    [SerializeField] HosePump _hose;
    private IHandleHolder _gasStationHolder;
    private IHandleHolder _previousHolder;

    [Inject]
    private void Init(IHandleHolder holder)
    {
        _gasStationHolder = holder;
    }
    private void Start()
    {
        ChangeOwner(_gasStationHolder);
    }

    private bool _isActive = false;
    public event EventHandler<HandleOwnerChangedArgs> OnHandleOwnerChanged;

    public void ChangeOwner(IHandleHolder handleHolder)
    {
        handleHolder.SetHandle(this);
        Transform parent = handleHolder.GetTransform();

        OnHandleOwnerChanged?.Invoke(this, new HandleOwnerChangedArgs { Parent = parent });

        if (_previousHolder != null)
            _previousHolder.Clear();

        _previousHolder = handleHolder;

        _isActive = handleHolder == _gasStationHolder ? false : true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("CarNoGas"))
        // {
        //     CarGasFill gasFill = other.GetComponent<CarGasFill>();
        //     if (gasFill.CarIsRepaired) { return; }
        //     gasFill.AttachHandle(this, _hose, _station);
        //     GameManager.Instance.References.PlayerAnimator.ResetWalking();
        // }
    }
    private void FixedUpdate()
    {
        if (!_isActive) { return; }

        float distance = Vector3.Distance(transform.position, _gasStationHolder.GetTransform().position);

        if (distance > 10f)
        {
            Return();
        }
    }
    public void Return()
    {
        ChangeOwner(_gasStationHolder);
    }
}
public class HandleOwnerChangedArgs : EventArgs
{
    public Transform Parent;
}