using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace TaxiGame.Items
{
    public class HatStacker : MonoBehaviour
    {
        public Stack<StackableItem> ItemStack { get { return _stack; } }
        public int MaxStackSize { get { return _maxStackSize; } }
        [SerializeField] private int _maxStackSize;
        private Stack<StackableItem> _stack = new Stack<StackableItem>();
        public event EventHandler<OnHatStackedArgs> OnHatStacked;

        public void StackItem(StackableItem item)
        {
            Action stackItemDelegate = () => _stack.Push(item);

            OnHatStacked?.Invoke(this, new OnHatStackedArgs
            {
                OnTweenComplete = stackItemDelegate,
                ItemStack = _stack,
                Item = item

            });
        }
    }

    public class OnHatStackedArgs : EventArgs
    {
        public Action OnTweenComplete;
        public Stack<StackableItem> ItemStack;
        public StackableItem Item;
    }
}