using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using InventorySystem.Common;
using InventorySystem.Common.EventArgument;
using UISystem;
using UISystem.InventoryUI;
using UnityEngine;

namespace InventorySystem
{
    public class PlayerInventoryModel : UIModel
    {
        public Inventory EquipmentInventory { get; private set; }
        public Inventory MaterialInventory { get; private set; }
        public Inventory ConsumableInventory { get; private set; }

        public PlayerInventoryModel(PlayerInventoryController controller, List<InventorySlotModel> equipmentsSlots, List<InventorySlotModel> materialsSlots,
            List<InventorySlotModel> consumablesSlots) : base(EUIType.PanelPlayerInventory, controller)
        {
            EquipmentInventory = new Inventory(equipmentsSlots);
            MaterialInventory = new Inventory(materialsSlots);
            ConsumableInventory = new Inventory(consumablesSlots);
        }

        /// <summary>
        /// Add an item to player's inventory.
        /// </summary>
        /// <param name="item">The WrappedItem to be added.</param>
        /// <returns>True if the item is successfully added. False if the inventory is full.</returns>
        public bool AddItem([NotNull] WrappedItem item)
        {
            if (item.StaticData == null)
            {
                Debug.LogWarning("Adding to inventory failed due to the item's static data is null");
                return false;
            }

            switch (item.Type)
            {
                case EItemType.Equipment:
                    throw new NotImplementedException();
                case EItemType.Material:
                    throw new NotImplementedException();
                case EItemType.Consumable:
                    return ConsumableInventory.AddItem(item);
                default:
                    throw new Exception(
                        $"No matching enum for wrappedItem Name: {item.Name}, ID: {item.ID}, Type: {item.Type}.");
            }
        }

        public WrappedItem RemoveItem([NotNull] WrappedItem item, bool removeAll)
        {
            if (item.StaticData == null)
            {
                Debug.LogWarning("Removing from inventory failed due to the item's static data is null");
                return null;
            }

            switch (item.Type)
            {
                case EItemType.Equipment:
                    throw new NotImplementedException();
                case EItemType.Material:
                    throw new NotImplementedException();
                case EItemType.Consumable:
                    return ConsumableInventory.RemoveItem(item, removeAll);
                default:
                    throw new Exception(
                        $"No matching enum for wrappedItem Name: {item.Name}, ID: {item.ID}, Type: {item.Type}.");
            }
        }

        public void RegisterAddItemEvent(EventHandler<AddItemToInventoryEventArgs> action)
        {
            EquipmentInventory.OnAddItem += action;
            MaterialInventory.OnAddItem += action;
            ConsumableInventory.OnAddItem += action;
        }

        public void RegisterRemoveItemEvent(EventHandler<RemoveItemFromInventoryEventArgs> action)
        {
            EquipmentInventory.OnRemoveItem += action;
            MaterialInventory.OnRemoveItem += action;
            ConsumableInventory.OnRemoveItem += action;
        }
    }
}