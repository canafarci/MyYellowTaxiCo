using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TaxiGame.Items;
using TaxiGame.NPC;

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
    private void OnEnable() => GameManager.Instance.References.PlayerInventory.OnInventoryModified += OnStackSizeChange; //TODO inject player inventory

    private void OnStackSizeChange(object sender, InventoryModifiedArgs e)
    {
        if (IsNotStackableItem(e.InventoryObject.GetObjectType())) return;

        Inventory inventory = sender as Inventory;

        if (inventory.GetStackableItemCountInInventory() == inventory.StackableItemCapacity)
        {
            _text.enabled = true;
        }
        else
        {
            _text.enabled = false;
        }
    }

    private bool IsNotStackableItem(InventoryObjectType objectType)
    {
        return objectType == InventoryObjectType.Customer || objectType == InventoryObjectType.GasHandle;
    }

    private void LateUpdate()
    {
        if (_text.enabled)
        {
            transform.position = _player.position + _baseOffset;
        }
    }

}
