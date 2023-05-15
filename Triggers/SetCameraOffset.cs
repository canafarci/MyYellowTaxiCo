using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraOffset : MonoBehaviour
{
    [SerializeField] Vector3 _normalOffset, _officesOffset;
    Mover _mover;

    private void Awake() => _mover = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>();

    public void SetNormalOffset() => _mover.RotationOffset = _normalOffset;
    public void SetOfficesOffset() => _mover.RotationOffset = _officesOffset;
}
