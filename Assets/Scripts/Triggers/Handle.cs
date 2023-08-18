using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Handle : MonoBehaviour
{
    [SerializeField] Transform _originalPos;
    [SerializeField] HosePump _hose;
    [SerializeField] GasStation _station;
    public bool IsActive = false;
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
        if (!IsActive) { return; }

        float distance = Vector3.Distance(transform.position, _originalPos.position);

        if (distance > 10f)
        {
            GameManager.Instance.References.PlayerAnimator.ResetWalking();
            Return();
        }
    }
    public void Return()
    {
        transform.parent = null;

        transform.DORotate(_originalPos.rotation.eulerAngles, 1f);
        transform.DOMove(_originalPos.position, 1f).onComplete = () =>
        {
            IsActive = false;
            _station.IsActive = false;
            _station.Thunder.SetActive(false);
        };
    }
}