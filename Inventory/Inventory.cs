using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int MaxStackSize { get { return _maxStackSize; } set { _maxStackSize = value; } }
    public int MaxFollowerSize { get { return _maxFollowerSize; } }
    public int ItemCount { get { return _linkedList.Count; } }
    public int FollowerCount { get { return _followers.Count; } }
    [SerializeField] int _maxStackSize, _maxFollowerSize;

    LinkedList<Follower> _followers = new LinkedList<Follower>();
    LinkedList<StackableItem> _linkedList = new LinkedList<StackableItem>();
    StackPositionCalculator _positionCalculator;
    public event Action<int> InventorySizeChangeHandler;

    private void Awake() => _positionCalculator = GetComponentInChildren<StackPositionCalculator>();
    public void AddFollowerToList(Follower follower) => _followers.AddFirst(follower);
    public void StackItem(StackableItem item)
    {
        item.transform.parent = _positionCalculator.transform;

        Vector3 endPos = _positionCalculator.CalculatePosition(_linkedList, item);
        StartCoroutine(DotweenFX.StackTweenItem(item, endPos));
        _linkedList.AddFirst(item);

        InventorySizeChangeHandler.Invoke(_linkedList.Count);
    }
    public void RemoveItem(StackableItem item)
    {
        _linkedList.Remove(item);
        RecalculatePositions();

        InventorySizeChangeHandler.Invoke(_linkedList.Count);
    }
    public void RemoveFollower(Follower follower) => _followers.Remove(follower);
    public StackableItem GetItem(Enums.StackableItemType hat) => _linkedList.Where(x => x.Hat == hat).LastOrDefault();
    public Follower[] GetFollowers(Enums.StackableItemType hat) => _followers.Where(x => x.Hat == hat).ToArray();
    private void RecalculatePositions() => _positionCalculator.RecalculatePositions(_linkedList);
}
