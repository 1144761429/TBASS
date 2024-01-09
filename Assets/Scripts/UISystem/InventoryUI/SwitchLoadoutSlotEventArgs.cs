using System;
using WeaponSystem;

namespace UISystem.InventoryUI
{
    public class SwitchLoadoutSlotEventArgs : EventArgs
    {
        public PlayerLoadoutSlotController SlotSwitchTo { get; private set; }
        public ELoadoutSlot Front { get; private set; }

        public SwitchLoadoutSlotEventArgs(PlayerLoadoutSlotController to, ELoadoutSlot front)
        {
            SlotSwitchTo = to;
            Front = front;
        }
    }
}