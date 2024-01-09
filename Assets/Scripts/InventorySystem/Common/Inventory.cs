using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using InventorySystem.Common.EventArgument;
using ItemSystem.ItemCustomComparers;
using UnityEngine;
using ArgumentNullException = System.ArgumentNullException;

namespace InventorySystem.Common
{
    /// <summary>
    /// The M of MVC.
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Event for successfully adding an item to the inventory.
        /// </summary>
        public event EventHandler<AddItemToInventoryEventArgs> OnAddItem;

        /// <summary>
        /// Event for removing item from the inventory.
        /// </summary>
        public event EventHandler<RemoveItemFromInventoryEventArgs> OnRemoveItem;

        /// <summary>
        /// All the InventorySlot of this inventory.
        /// </summary>
        public List<InventorySlotModel> Slots { get; private set; }

        /// <summary>
        /// If there is no more empty slots.
        /// </summary>
        public bool IsFull => _items.Count == Slots.Count;

        /// <summary>
        /// All the items in this inventory.
        /// </summary>
        private SortedSet<WrappedItem> _items;

        /// <summary>
        /// A dictionary the maps an item to the slot that stores it.
        /// </summary>
        private Dictionary<WrappedItem, InventorySlotModel> _itemToSlotDict;

        /// <summary>
        /// The index that indicates which slots will the next item be stored into.
        /// </summary>
        private int _insertSlotIndex;
        
        public Inventory(List<InventorySlotModel> slots)
        {
            Slots = slots;

            _items = new SortedSet<WrappedItem>(new WrappedItemRarityComparer());
            _itemToSlotDict = new Dictionary<WrappedItem, InventorySlotModel>();

            OnRemoveItem += Rearrange;
        }

        /// <summary>
        /// Add an item to this inventory.
        /// </summary>
        /// <param name="addedItem">The item to be added to this inventory.</param>
        /// <returns>True if the item is successfully added. False if the addition failed.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="addedItem"/> is null.</exception>
        public bool AddItem([NotNull] WrappedItem addedItem)
        {
            if (addedItem == null)
            {
                throw new ArgumentNullException($"Argument wrappedItem cannot be null.");
            }

            // If the item exist in the inventory, then change its amount
            if (ContainItem(addedItem.Name, addedItem.ID, out WrappedItem targetItem))
            {
                Debug.Log($"In: {targetItem.Amount}, ToAdd: {addedItem.Amount}");
                
                WrappedItem itemBeforeAddition = new WrappedItem(targetItem.Name, targetItem.ID, targetItem.Amount);

                _itemToSlotDict[targetItem].IncreaseAmount(addedItem.Amount);
                

                AddItemToInventoryEventArgs args =
                    new AddItemToInventoryEventArgs(this, _itemToSlotDict[targetItem], itemBeforeAddition, targetItem,
                        addedItem);
                OnAddItem?.Invoke(this, args);

                return true;
            }

            // If the item does not exist in the inventory, find an empty slot and store it
            if (_insertSlotIndex < Slots.Count)
            {
                InventorySlotModel emptySlot = Slots[_insertSlotIndex];
                // Debug.Log(Slots == null);
                emptySlot.Store(addedItem);
                _items.Add(addedItem);
                _itemToSlotDict.Add(addedItem, emptySlot);

                _insertSlotIndex++;
                
                AddItemToInventoryEventArgs args =
                    new AddItemToInventoryEventArgs(this, emptySlot, null, addedItem, addedItem);
                OnAddItem?.Invoke(this, args);

                return true;
            }

            // Otherwise, return false if there is no empty slot
            string warningText = $"Adding item failed due to: Inventory has no more empty slot." +
                                 $"\nOr a potential error: an unregistered item occupied a slots.";
            Debug.LogWarning(warningText);
            return false;
        }

        /// <summary>
        /// Remove an item from this inventory.
        /// </summary>
        /// <param name="itemToRemove">The item to be removed from this inventory.</param>
        /// <param name="removeAll">If should remove all. Meaning set the amount of <paramref name="itemToRemove"/> to 0.</param>
        /// <returns>The item after removal. This will be null if the item is completely removed from the inventory.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="itemToRemove"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the amount of <paramref name="itemToRemove"/> is larger than the actual amount in the invenotry.</exception>
        public WrappedItem RemoveItem([NotNull] WrappedItem itemToRemove, bool removeAll)
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException($"Argument wrappedItem cannot be null.");
            }

            // TODO: name and id validation check

            // Check if the the itemToRemove exists in this inventory.
            if (ContainItem(itemToRemove.Name, itemToRemove.ID, out WrappedItem targetItem))
            {
                // If the amount to remove is larger than the amount of the item,
                // then throw an ArgumentException
                if (itemToRemove.Amount > targetItem.Amount)
                {
                    throw new ArgumentOutOfRangeException(targetItem.ToString(),
                        $"Amount to remove: {itemToRemove.Amount}.");
                }

                InventorySlotModel slotModel = _itemToSlotDict[targetItem];
                WrappedItem itemBeforeRemoval = new WrappedItem(targetItem.Name, targetItem.ID, targetItem.Amount);

                // If remove all or the remove amount equals to the amount of that of the target item in the inventory
                if (removeAll || targetItem.Amount == itemToRemove.Amount)
                {
                    // Before remove the item from the inventory, set its amount to 0;
                    targetItem.DecreaseAmount(itemToRemove.Amount);

                    _items.Remove(targetItem);
                    slotModel.Clear();
                    //_emptySlots.Enqueue(slotModel); //TODO: make use of leftmost slots first
                    _itemToSlotDict.Remove(targetItem);
                    
                    _insertSlotIndex--;
                    
                    RemoveItemFromInventoryEventArgs args1 =
                        new RemoveItemFromInventoryEventArgs(this, slotModel, itemBeforeRemoval, null, itemToRemove);
                    OnRemoveItem?.Invoke(this, args1);

                    return null;
                }

                // If the amount to remove is smaller than the amount of the target item in the inventory,
                // then, decrease the amount of the target item.
                _itemToSlotDict[targetItem].DecreaseAmount(itemToRemove.Amount);
                RemoveItemFromInventoryEventArgs args2 =
                    new RemoveItemFromInventoryEventArgs(this, slotModel, itemBeforeRemoval, targetItem, itemToRemove);
                OnRemoveItem?.Invoke(this, args2);

                return targetItem;
            }

            Debug.LogWarning(
                $"Removal failed due to item {itemToRemove.Name} with ID {itemToRemove.ID} does not exist in _items or _itemSlotPairDict.");
            return null;
        }

        /// <summary>
        /// Clear all the items stored in this inventory.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            _itemToSlotDict.Clear();
            
            foreach (var slot in Slots)
            {
                slot.Clear();
                //_emptySlots.Enqueue(slot); //TODO: make the rightmost slots at bottom, leftmost at top
            }
        }

        /// <summary>
        /// Check if a WrappedItem is in the inventory.
        /// This method has a time complexity of O(N) where N is the number of item stored in the inventory.
        /// </summary>
        /// <param name="name">The name of the item to search.</param>
        /// <param name="id">The id of the item to search.</param>
        /// <param name="wrappedItem">The desired WrappedItem.</param>
        /// <returns>True if the item with the specified name and id exists. Otherwise, the <paramref name="wrappedItem"/> will be null and return false.</returns>
        public bool ContainItem(string name, int id, out WrappedItem wrappedItem)
        {
            //TODO: add a validation for if name and id match.

            wrappedItem = _items.FirstOrDefault(item => item.Name.Equals(name) && item.ID == id);

            return wrappedItem != null;
        }

        /// <summary>
        /// Rearrange the items and slots. This method will eliminate empty slots between slots occupied by items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Rearrange(object sender, RemoveItemFromInventoryEventArgs args)
        {
            if (args.ItemAfterRemoval != null)
            {
                return;
            }

            InventorySlotModel slot = args.SlotModel;

            for (int i = Slots.IndexOf(slot); i < Slots.Count - 1; i++)
            {
                if (!Slots[i + 1].IsOccupied)
                {
                    break;
                }

                _itemToSlotDict[Slots[i + 1].StoredItem] = Slots[i];
                Slots[i].Store(Slots[i + 1].StoredItem);
                Slots[i + 1].Clear();
            }
        }

        #region Debug Helper

        /// <summary>
        /// Print the information of all the items in this inventory.
        /// </summary>
        public void PrintAllItemInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var wrappedItem in _items)
            {
                stringBuilder.Append(wrappedItem + "\n");
            }

            Debug.Log(stringBuilder);
        }

        #endregion
    }
}
