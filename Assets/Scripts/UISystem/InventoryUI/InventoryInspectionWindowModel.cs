using System;
using InventorySystem.Common.EventArgument;
using UnityEngine;

namespace UISystem.InventoryUI
{
    /// <summary>
    /// M of MVC.
    /// </summary>
    public class InventoryInspectionWindowModel : UIModel
    {
        /// <summary>
        /// Event for setting the item to inspect.
        /// </summary>
        public event Action<WrappedItem> OnSetItemToInspect; 
        
        /// <summary>
        /// The item being inspected.
        /// </summary>
        public WrappedItem CurrentItem { get; private set; }
        
        public InventoryInspectionWindowModel(UIController controller) : base(EUIType.Component, controller)
        {
        }
        
        /// <summary>
        /// Set the item to inspect.
        /// </summary>
        /// <param name="item">The new item to inspect.</param>
        public void SetInspectedItem(WrappedItem item)
        {
            CurrentItem = item;
            
            OnSetItemToInspect?.Invoke(item);
        }

        /// <summary>
        /// Set the item being currently inspected to null.
        /// </summary>
        public void Clear()
        {
            CurrentItem = null;
        }
    }
}
