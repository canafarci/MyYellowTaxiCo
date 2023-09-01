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

    public event Action<bool> OnMoveDistanceCalculated;

    [Inject]
    private void Init(Rigidbody rb, IInputReader reader, ModifierUpgradesReceiver upgradeReceiver)
    {
        _rigidbody = rb;
        _reader = reader;
        _upgradeReceiver = upgradeReceiver;

        _upgradeReceiver.OnPlayerSpeedUpgrade += ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler;
        _reader.OnInputRead += InputReader_ReadInputHandler;
    }
    //this event is published every FixedTick (Rigidbody moves in fixedupdate)
    private void InputReader_ReadInputHandler(Vector2 input)
    {
        Vector3 distance = CalculateDistance(input);
        _rigidbody.MovePosition(transform.position + distance);
    }

    public void ModifierUpgradesReceiver_PlayerSpeedUpgradeHandler(float speed)
    {
        _speed = speed;
    }

    private Vector3 CalculateDistance(Vector2 input)
    {
        Vector3 distance;

        if (input == Vector2.zero)
        {
            OnMoveDistanceCalculated?.Invoke(false);
            distance = Vector3.zero;
        }
        else
        {
            OnMoveDistanceCalculated?.Invoke(true);

            Vector3 moveVector = RotateMoveVector(new Vector3(input.x, 0, input.y));
            transform.rotation = Quaternion.LookRotation(moveVector);

            distance = _speed * Time.fixedDeltaTime * moveVector;
        }

        return distance;
    }

    private Vector3 RotateMoveVector(Vector3 start)
    {
        start.Normalize();
        return Quaternion.AngleAxis(Globals.CAMERA_ROTATION_OFFSET.y, Vector3.up) * start;
    }

}
