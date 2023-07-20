using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool IsActive = true;
    [SerializeField] protected float _speed, _rotateSpeed;
    float _baseSpeed;
    public Vector3 RotationOffset;
    bool _disabledMovement = false;
    InputReader _reader;
    Rigidbody _rigidbody;
    Animator _animator;
    public void DisableMovement() => _disabledMovement = true;
    public void EnableMovement() => _disabledMovement = false;
    private void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _baseSpeed = _speed;
        _animator.speed = _speed / 7f; //constant base speed
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        Move(_reader.ReadInput());
    }

    public void IncreaseSpeed(float modifier)
    {
        _speed = _baseSpeed * modifier;
        _animator.speed = _speed / 7f;
    }

    void Move(Vector2 input)
    {
        if (input == Vector2.zero) return;

        Vector3 moveVector = RotateTowardsUp(new Vector3(input.x, 0, input.y));

        transform.rotation = Quaternion.LookRotation(moveVector);
        if (!_disabledMovement)
            _rigidbody.MovePosition(transform.position + (moveVector.normalized * Time.deltaTime * _speed));
    }

    Vector3 RotateTowardsUp(Vector3 start)
    {
        // if you know start will always be normalized, can skip this step
        start.Normalize();

        Vector3 axis = Vector3.Cross(start, Vector3.right);

        return Quaternion.AngleAxis(RotationOffset.y, Vector3.up) * start;
    }

}
