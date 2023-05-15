using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : MonoBehaviour
{
    public float ItemHeight;
    Collider _collider;
    public Enums.StackableItemType Hat;
    private void Awake() => _collider = GetComponent<Collider>();
}
