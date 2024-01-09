using System;
using WeaponSystem;

namespace UISystem
{
    public class ChangeWeaponEventArgs : EventArgs
    {
        public Weapon From { get; private set; }
        public Weapon To { get; private set; }
        public ELoadoutSlot LoadoutSlot { get; private set; }

        public ChangeWeaponEventArgs(Weapon from, Weapon to, ELoadoutSlot loadoutSlot)
        {
            From = from;
            To = to;
            LoadoutSlot = loadoutSlot;
        }
    }
}