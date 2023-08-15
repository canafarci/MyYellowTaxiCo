using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFX : MonoBehaviour
{
    [SerializeField] Renderer _renderer1, _renderer2, _renderer3, _renderer4;
    [SerializeField] Material _hasPassengerMaterial1, _hasPassengerMaterial2, _hasPassengerMaterial3;
    [SerializeField] GameObject _takeOffFX;
    public void PlayTakeOffFX()
    {
        _takeOffFX.SetActive(!_takeOffFX.activeSelf);
        ChangeMaterials();
    }

    void ChangeMaterials()
    {
        _renderer1.material = _hasPassengerMaterial1;
        _renderer2.material = _hasPassengerMaterial2;
        _renderer3.material = _hasPassengerMaterial3;
        _renderer4.material = _hasPassengerMaterial3;

    }

}
