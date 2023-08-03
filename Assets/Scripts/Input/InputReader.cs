using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputReader : IInputReader
{
    private Joystick _joystick;
    private bool _disabled = false;

    public InputReader(Joystick joystick)
    {
        _joystick = joystick;
    }
    public void Disable() => _disabled = true;
    public void Enable() => _disabled = false;

    public Vector2 ReadInput()
    {
        if (_disabled)
            return Vector2.zero;

        Vector2 moveVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        return moveVector;
    }
}

public interface IInputReader
{
    public Vector2 ReadInput();
    public void Disable();
    public void Enable();
}