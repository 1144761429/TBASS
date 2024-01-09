using System;
using System.Diagnostics.CodeAnalysis;

namespace InventorySystem.Common.EventArgument
{
    /// <summary>
    /// Event argument for when adding an item to an inventory.
    /// </summary>
    public class AddItemToInventoryEventArgs : EventArgs
    {
        /// <summary>
        /// The inventory that an item is added to.
        /// </summary>
        public Inventory Inventory { get; private set; }

        /// <summary>
        /// The inventory slot that the item is added to.
        /// </summary>
        public InventorySlotModel SlotModel { get; private set; }

        /// <summary>
        /// The item in the inventory before the addition happen.
        /// </summary>
        public WrappedItem ItemBeforeAddition { get; private set; }

        /// <summary>
        /// The item in the inventory after the addition happen.
        /// </summary>
        public WrappedItem ItemAfterAddition { get; private set; }

        /// <summary>
        /// The item that is added to the inventory.
        /// </summary>
        public WrappedItem ItemToAdd { get; private set; }


        /// <param name="inventory">The inventory that an item is added to.</param>
        /// <param name="slotModel">The inventory slot that the item is added to.</param>
        /// <param name="itemBeforeAddition">The item in the inventory before the addition happen.</param>
        /// <param name="itemAfterAddition">The item in the inventory after the addition happen.</param>
        /// <param name="itemToAdd">The item that is added to the inventory.</param>
        public AddItemToInventoryEventArgs(
            Inventory inventory,
            InventorySlotModel slotModel,
            WrappedItem itemBeforeAddition,
            [NotNull] WrappedItem itemAfterAddition,
            [NotNull] WrappedItem itemToAdd)
        {
            Inventory = inventory;
            SlotModel = slotModel;
            ItemBeforeAddition = itemBeforeAddition;
            ItemAfterAddition = itemAfterAddition;
            ItemToAdd = itemToAdd;
        }
    }
}



