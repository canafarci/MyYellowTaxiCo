using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Animations : MonoBehaviour
{
    [SerializeField] private AnimationClip _idle, _holdingIdle, _running, _holdingRunning, _walking;
    private AnimatorOverrideController _animatorOverrideController;
    private bool _playingMove, _handsFull, _playingWalking;
    private Animator _animator;
    private Inventory _inventory;
    private IInputReader _inputReader;
    private readonly string _idleTrigger = "Idle";
    private readonly string _moveTrigger = "Move";

    [Inject]
    private void Init(IInputReader reader, Animator animator, Inventory inventory)
    {
        _inputReader = reader;
        _animator = animator;
        _inventory = inventory;

        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
        _inventory.InventorySizeChangeHandler += OnStackSizeChange;
    }

    private void OnStackSizeChange(int size)
    {
        if (size != 0)
        {
            _animatorOverrideController["Running"] = _holdingRunning;
            _animatorOverrideController["IDLE"] = _holdingIdle;
        }
        else
        {
            _animatorOverrideController["Running"] = _running;
            _animatorOverrideController["IDLE"] = _idle;
        }
    }
    public void SetHoldingIdle()
    {
        _animatorOverrideController["IDLE"] = _holdingIdle;

    }
    public void ResetIdle()
    {
        _animatorOverrideController["IDLE"] = _idle;
    }
    public void SetWalking()
    {
        _animatorOverrideController["Running"] = _walking;
        _animatorOverrideController["IDLE"] = _idle;

    }
    public void ResetWalking()
    {
        _animatorOverrideController["Running"] = _running;
        _animatorOverrideController["IDLE"] = _idle;
    }

    private void FixedUpdate()
    {
        if (!_playingMove && _inputReader.ReadInput() != Vector2.zero)
        {
            _animator.SetTrigger(_moveTrigger);
            _playingMove = true;
        }
        else if (_inputReader.ReadInput() == Vector2.zero && _playingMove)
        {
            _animator.SetTrigger(_idleTrigger);
            _playingMove = false;
        }
    }
}
