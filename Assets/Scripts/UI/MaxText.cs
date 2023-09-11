using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TaxiGame.Items;
using TaxiGame.NPC;
using Zenject;
using System.Linq;

public class MaxText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private ItemUtility _itemUtility;
    private Inventory _inventory;

    [Inject]
    private void Init(TextMeshProUGUI text,
        ItemUtility itemUtility,
        [Inject(Id = "UI")] Inventory inventory)
    {
        _text = text;
        _itemUtility = itemUtility;
        _inventory = inventory;
    }
    private void OnEnable()
    {
        _inventory.OnInventoryModified += Inventory_InventoryModifiedHandler;
    }
    private void Inventory_InventoryModifiedHandler(object sender, InventoryModifiedArgs e)
    {
        InventoryObjectType objectType = e.InventoryObject.GetObjectType();

        if (IsStackableItem(objectType))
        {
            Inventory inventory = sender as Inventory;

            if (inventory.IsInventoryFull())
            {
                _text.enabled = true;
            }
            else
            {
                _text.enabled = false;
            }
        }
    }

    private bool IsStackableItem(InventoryObjectType objectType)
    {
        return _itemUtility.IsStackableObject(objectType);
    }
}
