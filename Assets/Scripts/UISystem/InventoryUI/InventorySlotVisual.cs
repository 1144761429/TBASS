using System;
using InventorySystem.Common.EventArgument;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.InventoryUI
{
    public class InventorySlotVisual : UIVisual
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public TMP_Text AmountText { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }

        /// <summary>
        /// Visual update for when an item is added to this slot.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception">If the sprite asset path of the item is invalid.</exception>
        public void UpdateVisual(AddItemToInventoryEventArgs args)
        {
            // Update the icon sprite only if the item is newly added to the slot, meaning the slot was empty before storing the item.
            if (args.ItemBeforeAddition == null)
            {
                ItemData itemData = args.ItemAfterAddition.StaticData;

                Sprite sprite = Resources.Load<Sprite>(itemData.SpritePath);
                if (sprite == null)
                {
                    throw new Exception(
                        $"Update slot visual failed due to sprite path for item name: {itemData.Name} ID: {itemData.ID} is invalid.");
                }

                Icon.sprite = sprite;
            }

            AmountText.text = args.ItemAfterAddition.Amount.ToString();
        }

        /// <summary>
        /// Visual update for when an item is removed from this slot.
        /// </summary>
        /// <param name="args"></param>
        public void UpdateVisual(RemoveItemFromInventoryEventArgs args)
        {
            // If the item after removal is null, then clear the icon image and amount text
            if (args.ItemAfterRemoval == null)
            {
                Icon.sprite = null;
                AmountText.text = null;
                
                return;
            }

            // Otherwise, meaning there is a item in the slot and it have a positive amount,
            // then set the amount text to the correct value.
            AmountText.text = args.SlotModel.StoredItem.Amount.ToString();
        }
    }
}