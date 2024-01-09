using System;
using UnityEngine;
using WeaponSystem;

namespace UISystem
{
    public class PlayerLoadoutSlotEventArgs : EventArgs
    {
        /// <summary>
        /// Weapon stored in the loadout slot.
        /// </summary>
        public Weapon Weapon { get; private set; }
        /// <summary>
        /// The controller of the slot which the event happens on.
        /// </summary>
        public PlayerLoadoutSlotController CurrentSlot { get; private set; }
        /// <summary>
        /// The controller of the slot being displayed at the most front in the loadout UI.
        /// </summary>
        public PlayerLoadoutSlotController FrontSlot { get; private set; }
        
        /// <param name="weapon">Weapon stored in the loadout slot.</param>
        /// <param name="currentSlot">The controller of the slot which the event happens on.</param>
        /// <param name="frontSlot">The controller of the slot being displayed at the most front in the loadout UI.</param>
        public PlayerLoadoutSlotEventArgs(Weapon weapon, PlayerLoadoutSlotController currentSlot, PlayerLoadoutSlotController frontSlot)
        {
            Weapon = weapon;
            CurrentSlot = currentSlot;
            FrontSlot = frontSlot;
        }
    }
}