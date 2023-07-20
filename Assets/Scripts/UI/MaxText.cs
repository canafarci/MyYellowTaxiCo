using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxText : MonoBehaviour
{
    TextMeshProUGUI _text;
    Vector3 _baseOffset;
    Transform _player;
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _player = GameManager.Instance.References.PlayerInventory.transform;
    }

    private void Start()
    {
        _baseOffset = _player.position - transform.position;
    }
    private void OnEnable() => GameManager.Instance.References.PlayerInventory.InventorySizeChangeHandler += OnStackSizeChange;

    private void OnStackSizeChange(int size)
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;

        if (size == inventory.MaxStackSize)
        {
            _text.enabled = true;
        }
        else
        {
            _text.enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (_text.enabled)
        {
            transform.position = _player.position + _baseOffset;
        }
    }

}
