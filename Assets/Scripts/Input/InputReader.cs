using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    Joystick _joystick;
    Animations _animations;
    bool _disabled = false;
    private void Awake()
    {
        _animations = GameObject.FindGameObjectWithTag("Player").GetComponent<Animations>();
        _joystick = FindObjectOfType<Joystick>();
    }
    public void Disable() => _disabled = true;
    public void Enable() => _disabled = false;

    public Vector2 ReadInput()
    {
        Vector2 moveVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        if (_disabled)
            moveVector = Vector2.zero;

        SetAnimations(moveVector);

        return moveVector;
    }

    void SetAnimations(Vector2 moveVector)
    {
        if (moveVector == Vector2.zero)
            _animations.IsMoving = false;
        else
            _animations.IsMoving = true;
    }
}
