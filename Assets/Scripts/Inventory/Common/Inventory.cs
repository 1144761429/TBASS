using System;
using System.Collections.Generic;
using UnityEngine;
using ArgumentNullException = System.ArgumentNullException;

public class Inventory<T> : MonoBehaviour where T : ItemData
{
    public Action<WrappedItem> OnAddItem;
    public Action OnRemoveItem;

    public List<InventorySlot> Slots { get; private set; }
    private List<WrappedItem<T>> _items = new();
    private Dictionary<WrappedItem<T>, InventorySlot> _itemSlotPairDict = new();

    public bool IsFull
    {
        get => _items.Count >= Slots.Count;
    }

    public void Init()
    {
        //TODO: read what items the player has from the json file
    }

    public bool AddItem(WrappedItem<T> wrappedItem)
    {
        if (wrappedItem == null)
        {
            throw new ArgumentNullException($"Argument wrappedItem cannot be null.");
        }

        //If the inventory is full, log a warning and do not add the item
        if (_items.Count >= Slots.Count)
        {
            Debug.LogWarning($"Adding item failed due to: Inventory {gameObject.name} is full");
            return false;
        }

        //If the item exist in the inventory, then change its amount
        if (HasItem(wrappedItem.ID, out WrappedItem<T> targetWrappedItem))
        {
            ChangeAmount(targetWrappedItem, targetWrappedItem.Amount.Value += wrappedItem.Amount.Value);
            OnAddItem?.Invoke(new WrappedItem(wrappedItem.Data, wrappedItem.Amount));
            return true;
        }
        //Otherwise, the item does not exist in the inventory, find an empty slot and store it
        else
        {
            if (TryFindEmptySlot(out InventorySlot emptySlot))
            {
                _items.Add(wrappedItem);
                _itemSlotPairDict.Add(wrappedItem, emptySlot);
                emptySlot.Store(new WrappedItem(wrappedItem.Data, wrappedItem.Amount));
                OnAddItem?.Invoke(new WrappedItem(wrappedItem.Data, wrappedItem.Amount));
                return true;
            }
            else
            {
                string warningText = $"Adding item failed due to: Inventory {gameObject.name} has no more empty slot." +
                                     $"\nPotential error: an unregistered item occupied a slots.";
                Debug.LogWarning(warningText);
                return false;
            }
        }
    }

    public void RemoveItem(WrappedItem<T> wrappedItem)
    {
        if (wrappedItem == null)
        {
            throw new ArgumentNullException($"Argument wrappedItem cannot be null.");
        }

        //TODO: Check if the wrapped Item exists in the item list and itemSlotPair dict.
        if (_items.Contains(wrappedItem) && _itemSlotPairDict.ContainsKey(wrappedItem))
        {
            _items.Remove(wrappedItem);
            _itemSlotPairDict[wrappedItem].Clear();
            _itemSlotPairDict.Remove(wrappedItem);
        }
        else
        {
            Debug.LogWarning(
                $"Removal failed due to item {wrappedItem.Name} with ID {wrappedItem.ID} does not exist in _items or _itemSlotPairDict.");
        }
    }

    public void Clear()
    {
        _items.Clear();
        _itemSlotPairDict.Clear();
        foreach (var slot in Slots)
        {
            slot.Clear();
        }
    }

    private bool TryFindEmptySlot(out InventorySlot emptySlot)
    {
        foreach (var slot in Slots)
        {
            if (!slot.IsOccupied)
            {
                emptySlot = slot;
                return true;
            }
        }

        emptySlot = null;
        return false;
    }

    public bool HasItem(int id)
    {
        return _items.Exists(wrappedItem => wrappedItem.ID == id);
    }

    public bool HasItem(int id, out WrappedItem<T> wrappedItem)
    {
        if (HasItem(id))
        {
            wrappedItem = _items.Find(targetWrappedItem => targetWrappedItem.ID == id);
            return true;
        }

        wrappedItem = null;
        return false;
    }

    public void SetupSlots(List<InventorySlot> slots)
    {
        Slots = slots;
    }

    private void ChangeAmount(WrappedItem<T> wrappedItem, int amount)
    {
        if (HasItem(wrappedItem.ID, out WrappedItem<T> targetWrappedItem))
        {
            targetWrappedItem.Amount.Value = amount;
            _itemSlotPairDict[targetWrappedItem].UpdateSlotVisual();
        }
        else
        {
            throw new Exception($"Target item is not in the inventory. Amount change failed");
        }
    }

    #region Debug Helper

    private void DebugItemNamesInInventory()
    {
        string itemNames = "";
        foreach (var wrappedItem in _itemSlotPairDict.Keys)
        {
            itemNames += wrappedItem.Name + "\n";
        }

        Debug.Log(itemNames);
    }

    #endregion
}