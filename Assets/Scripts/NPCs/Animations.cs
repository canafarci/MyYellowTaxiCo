using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public bool IsMoving;
    [SerializeField] AnimationClip _idle, _holdingIdle, _running, _holdingRunning, _walking;
    AnimatorOverrideController _animatorOverrideController;
    bool _playingMove, _handsFull, _playingWalking;
    Animator _animator;
    Inventory _inventory;
    string _idleTrigger = "Idle";
    string _moveTrigger = "Move";
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _inventory = GetComponent<Inventory>();
    }
    private void OnEnable() => _inventory.InventorySizeChangeHandler += OnStackSizeChange;
    private void Start()
    {
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
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
    private void FixedUpdate()
    {
        if (!_playingMove && IsMoving)
        {
            _animator.SetTrigger(_moveTrigger);
            _playingMove = true;
        }
        else if (!IsMoving && _playingMove)
        {
            _animator.SetTrigger(_idleTrigger);
            _playingMove = false;
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
}
