using System;
using InventorySystem.Common.EventArgument;
using JetBrains.Annotations;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Common
{
    /// <summary>
    /// The M of MVC.
    /// </summary>
    public class InventorySlotModel : UIModel
    {
        /// <summary>
        /// Event of storing an item into this inventory slot.
        /// </summary>
        public event Action<AddItemToInventoryEventArgs> OnStoreItem; 
        
        /// <summary>
        /// Event of removing an item from this inventory slot.
        /// </summary>
        public event Action<RemoveItemFromInventoryEventArgs> OnRemoveItem; 
        
        /// <summary>
        /// The item that is stored in this slot.
        /// </summary>
        public WrappedItem StoredItem { get; private set; }
        /// <summary>
        /// The item data of the item that is stored in this slot.
        /// </summary>
        public ItemData ItemData => StoredItem.StaticData;
        /// <summary>
        /// The amount of the item that is stored in this slot.
        /// </summary>
        public int Amount => StoredItem.Amount;
        /// <summary>
        /// If this slot is occupied, meaning there is an item stored in it.
        /// </summary>
        public bool IsOccupied => StoredItem != null;
        /// <summary>
        /// The inventory that this slot belongs to.
        /// </summary>
        public Inventory Inventory { get; private set; }

        public InventorySlotModel(InventorySlotController controller) : base(EUIType.Component,controller)
        {
            
        }

        /// <summary>
        /// Store a wrapped item to the this slot.
        /// </summary>
        /// <param name="itemToStore">The item to store.</param>
        public void Store([NotNull] WrappedItem itemToStore)
        {
            StoredItem = itemToStore;

            AddItemToInventoryEventArgs args =
                new AddItemToInventoryEventArgs(Inventory, this, null, itemToStore, itemToStore);
            OnStoreItem?.Invoke(args);
        }

        public void IncreaseAmount(int amount)
        {
            if (StoredItem == null)
            {
                throw new NullReferenceException("There is no StoredItem reference to increase amount.");
            }

            if (string.IsNullOrEmpty(StoredItem.Name) || StoredItem.ID == 0)
            {
                throw new NullReferenceException("There is a reference to the StoredItem, " +
                                                 "but the name of the stored item is null or empty. Or the ID of the stored item is 0.");
            }
            
            WrappedItem itemBeforeAddition = StoredItem.Clone();
            WrappedItem itemToStore = new WrappedItem(StoredItem.Name, StoredItem.ID, amount);
            StoredItem.IncreaseAmount(amount);
            
            AddItemToInventoryEventArgs args =
                new AddItemToInventoryEventArgs(Inventory, this, itemBeforeAddition, StoredItem, itemToStore);
            OnStoreItem?.Invoke(args);
        }

        public void DecreaseAmount(int amount)
        {
            StoredItem.DecreaseAmount(amount);
            
            WrappedItem itemBeforeRemoval = StoredItem.Clone();
            WrappedItem itemToRemove = new WrappedItem(StoredItem.Name, StoredItem.ID, amount);
            RemoveItemFromInventoryEventArgs args =
                new RemoveItemFromInventoryEventArgs(Inventory, this, itemBeforeRemoval, StoredItem, itemToRemove);
            
            OnRemoveItem?.Invoke(args);
        }
    
        public void Clear()
        {
            WrappedItem itemBeforeRemoval = StoredItem.Clone();
            RemoveItemFromInventoryEventArgs args =
                new RemoveItemFromInventoryEventArgs(Inventory, this, itemBeforeRemoval, null, itemBeforeRemoval);
            
            StoredItem = null;
            
            OnRemoveItem?.Invoke(args);
        }
        
    }
}