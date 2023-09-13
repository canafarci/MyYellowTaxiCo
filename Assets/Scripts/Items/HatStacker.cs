using System.Collections.Generic;
using UnityEngine;
using System;

namespace TaxiGame.Items
{
    public class HatStacker : MonoBehaviour
    {
        //Variables
        [SerializeField] private int _maxStackSize;
        private Stack<StackableItem> _stack = new Stack<StackableItem>();
        //used to lock picking hats up from the stacker while stacking is in process
        private int _currentlyStackingHatCount = 0;
        //Subscribed from HatStackerVisual
        public event EventHandler<OnHatStackedArgs> OnHatStacked;
        public void StackItem(StackableItem item)
        {
            _stack.Push(item);
            _currentlyStackingHatCount++;

            OnHatStacked?.Invoke(this, new OnHatStackedArgs
            {
                Item = item,
                OnStackCompleteDelegate = () => _currentlyStackingHatCount -= 1
            });
        }

        //Getters-Setters
        public int GetMaxStackSize() => _maxStackSize;
        public Stack<StackableItem> GetItemStack() => _stack;
        public bool IsStackingHat() => _currentlyStackingHatCount != 0;
    }

    public class OnHatStackedArgs : EventArgs
    {
        public StackableItem Item;
        public Action OnStackCompleteDelegate;
    }
}