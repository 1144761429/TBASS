using System;
using System.Collections.Generic;
using InventorySystem.Common.EventArgument;

namespace UISystem
{
    public class RecentAcquiredModel : UIModel
    {
        public event EventHandler<WrappedItem> OnAddItemToDisplayQueue;

        private PriorityQueue<WrappedItem, int> _recentlyObtainedItems;
        private Queue<WrappedItem> _displayQueue;

        private int _maxDisplayAmount;
        
        public RecentAcquiredModel(UIController controller, int maxDisplayAmount) : base(EUIType.PanelRecentAcquired, controller)
        {
            _recentlyObtainedItems = new PriorityQueue<WrappedItem, int>();
            _displayQueue = new Queue<WrappedItem>(maxDisplayAmount);
            _maxDisplayAmount = maxDisplayAmount;
        }

        public void AddItem(object sender, AddItemToInventoryEventArgs args)
        {
            if (_displayQueue.Count == _maxDisplayAmount)
            {
                _recentlyObtainedItems.Enqueue(args.ItemToAdd, args.ItemToAdd.Priority);
                return;
            }

            AddItemToDisplayQueue(args.ItemToAdd);
        }

        public void RemoveItem()
        {
            if (_displayQueue.TryDequeue(out WrappedItem _))
            {
                if (_recentlyObtainedItems.TryDequeue(out WrappedItem item, out int _))
                {
                    AddItemToDisplayQueue(item);
                }   
            }
        }
        
        private void AddItemToDisplayQueue(WrappedItem item)
        {
            _displayQueue.Enqueue(item);
            
            OnAddItemToDisplayQueue?.Invoke(this, item);
        }
    }
}