using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GasStation : MonoBehaviour, IHandleHolder
{
    public Slider Slider;
    public Renderer Renderer;
    public Animator Animator;
    public GameObject Thunder, StationFX, CircleUI;
    [SerializeField] Transform _handleTrans;

    public EventHandler<OnGasHandleOwnerChangedArgs> OnGasHandleOwnerChanged;

    //* NEW
    private Handle _handle;
    private Handle _originalHandle;

    [Inject]
    private void Init(Handle handle)
    {
        _handle = handle;
        _originalHandle = _handle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _handle != null)
        {
            IHandleHolder handleHolder = other.GetComponent<IHandleHolder>();
            _handle.ChangeOwner(handleHolder);
        }
    }
    public void Clear()
    {
        _handle = null;
        OnGasHandleOwnerChanged?.Invoke(this, new OnGasHandleOwnerChangedArgs { OwnerIsGasStation = false });
    }
    public void SetHandle(Handle handle)
    {
        _handle = handle;
        OnGasHandleOwnerChanged?.Invoke(this, new OnGasHandleOwnerChangedArgs { OwnerIsGasStation = true });
    }

    public Transform GetTransform()
    {
        return _handleTrans;
    }

    public Handle GetOriginalHandle() => _originalHandle;

}
public class OnGasHandleOwnerChangedArgs : EventArgs
{
    public bool OwnerIsGasStation;
}
