using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputReader : IInputReader, IFixedTickable
{
    private Joystick _joystick;
    private bool _disabled = false;
    public event Action<Vector2> OnInputRead;

    public InputReader(Joystick joystick)
    {
        _joystick = joystick;
    }
    public void Disable() => _disabled = true;
    public void Enable() => _disabled = false;

    public void FixedTick()
    {
        OnInputRead?.Invoke(ReadInput());
    }

    private Vector2 ReadInput()
    {
        if (_disabled)
            return Vector2.zero;

        Vector2 moveVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
        return moveVector;
    }
}

public interface IInputReader
{
    public event Action<Vector2> OnInputRead;
    public void Disable();
    public void Enable();
}