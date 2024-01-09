using System;
using UnityEngine;
using WeaponSystem;

namespace UISystem
{
    /// <summary>
    /// The M of MVC.
    /// </summary>
    public class PlayerLoadoutSlotModel : UIModel
    {
        public event Action<Weapon> OnUpdateData; 
        
        /// <summary>
        /// The weapon stored in this loadout slot.
        /// </summary>
        public Weapon Weapon { get; private set; }

        public ItemDataEquipmentWeapon WeaponStaticData => Weapon.StaticData;
        public RuntimeItemDataEquipmentWeapon WeaponRuntimeData => Weapon.RuntimeData;
        
        public PlayerLoadoutSlotModel(PlayerLoadoutSlotController controller, Weapon weapon):base(EUIType.Component, controller)
        {
            Weapon = weapon;
        }
        
        public void UpdateData(Weapon weapon)
        {
            Weapon = weapon;

            OnUpdateData?.Invoke(weapon);
        }
    }
}