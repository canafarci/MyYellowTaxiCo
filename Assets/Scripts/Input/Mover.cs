using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Upgrades;
using UnityEngine;
using Zenject;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private float _speed;
    private IInputReader _reader;
    private Rigidbody _rigidbody;
    private ModifierUpgradesReceiver _upgradeReceiver;

    public event Action<float> OnMoveDistanceCalculated;

    [Inject]
    private void Init(Rigidbody rb, IInputReader reader, ModifierUpgradesReceiver upgradeReceiver)
    {
        _rigidbody = rb;
        _reader = reader;
        _upgradeReceiver = upgradeReceiver;

        _upgradeReceiver.OnPlayerSpeedUpgrade += ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler;
    }
    public void ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        Move(_reader.ReadInput());
    }

    private void Move(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            OnMoveDistanceCalculated?.Invoke(0f);
        }
        else
        {
            OnMoveDistanceCalculated?.Invoke(1f);

            Vector3 moveVector = RotateMoveVector(new Vector3(input.x, 0, input.y));
            transform.rotation = Quaternion.LookRotation(moveVector);

            Vector3 distance = moveVector.normalized * moveVector.magnitude * Time.deltaTime * _speed;

            _rigidbody.MovePosition(transform.position + distance);
        }
    }

    private Vector3 RotateMoveVector(Vector3 start)
    {
        start.Normalize();
        return Quaternion.AngleAxis(Globals.CAMERA_ROTATION_OFFSET.y, Vector3.up) * start;
    }

}
