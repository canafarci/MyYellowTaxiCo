using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stacker : MonoBehaviour
{
    public Stack<StackableItem> ItemStack { get { return _stack; } }
    public int MaxStackSize { get { return _maxStackSize; } }
    [SerializeField] protected int _maxStackSize;
    protected Stack<StackableItem> _stack = new Stack<StackableItem>();
    protected StackPositionCalculator _positionCalculator;

    private void Awake()
    {
        _positionCalculator = GetComponent<StackPositionCalculator>();
    }

    public void StackItem(StackableItem item) => StartCoroutine(Stack(item));

    private IEnumerator Stack(StackableItem item)
    {
        item.transform.parent = transform;

        Vector3 endPos = _positionCalculator.CalculatePosition(_stack, item);
        yield return StartCoroutine(DotweenFX.StackTweenHat(item, endPos));

        _stack.Push(item);
    }
}
