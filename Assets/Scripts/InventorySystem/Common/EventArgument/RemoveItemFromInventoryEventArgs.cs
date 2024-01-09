using System;

namespace InventorySystem.Common.EventArgument
{
    /// <summary>
    /// Event arguments for when removing an item from an inventory.
    /// </summary>
    public class RemoveItemFromInventoryEventArgs : EventArgs
    {
        /// <summary>
        /// The inventory that an item is adding to.
        /// </summary>
        public Inventory Inventory { get; private set; }

        /// <summary>
        /// The inventory slot that the item is removed from.
        /// </summary>
        public InventorySlotModel SlotModel { get; private set; }

        /// <summary>
        /// The item in the inventory before the removal happen.
        /// </summary>
        public WrappedItem ItemBeforeRemoval { get; private set; }

        /// <summary>
        /// The item in the inventory after the removal happen.
        /// </summary>
        public WrappedItem ItemAfterRemoval { get; private set; }

        /// <summary>
        /// The item that is removed from to the inventory.
        /// </summary>
        public WrappedItem ItemToRemove { get; private set; }


        /// <param name="inventory">The inventory that an item is adding to.</param>
        /// <param name="slotModel">The inventory slot that the item is removed from.</param>
        /// <param name="itemBeforeRemoval">The item in the inventory before the removal happen.</param>
        /// <param name="itemAfterRemoval">The item in the inventory after the removal happen.</param>
        /// <param name="itemToRemove">The item that is removed from to the inventory.</param>
        public RemoveItemFromInventoryEventArgs(
            Inventory inventory,
            InventorySlotModel slotModel,
            WrappedItem itemBeforeRemoval,
            WrappedItem itemAfterRemoval,
            WrappedItem itemToRemove)
        {
            Inventory = inventory;
            SlotModel = slotModel;
            ItemBeforeRemoval = itemBeforeRemoval;
            ItemAfterRemoval = itemAfterRemoval;
            ItemToRemove = itemToRemove;
        }
    }
}