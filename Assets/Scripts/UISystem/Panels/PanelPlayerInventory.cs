using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class PanelPlayerInventory : PanelBase
    {
        [field: SerializeField] public List<InventorySlot> EquipmentTabSlots { get; private set; }
        [field: SerializeField] public List<InventorySlot> MaterialTabSlots { get; private set; }
        [field: SerializeField] public List<InventorySlot> ConsumablesTabSlots { get; private set; }

        private void Awake()
        {
            PlayerInventory.Instance.SetupSlots(this);
        }

        //TODO: add method for sorting inventory. E.g., sorting items by rarity...
    }
}