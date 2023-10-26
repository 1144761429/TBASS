using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    [Serializable]
    public class RuntimeItemDataEquipmentWeapon
    {
        public Action OnReduceAmmo;
        public Action OnAddAmmo;

        private ItemDataEquipmentWeapon _data;

        public int AmmoInMag { get; private set; }
        public int AmmoInReserve;

        public float AimAngle;

       
        
        public RuntimeItemDataEquipmentWeapon(ItemDataEquipmentWeapon data)
        {
            _data = data;
            AmmoInMag = data.MagCapacity;
            AmmoInReserve = data.ReserveCapacity;
            AimAngle = data.DefaultAimAngle;
        }

        public void Reset(ItemDataEquipmentWeapon data)
        {
            _data = data;
            AmmoInMag = data.MagCapacity;
            AmmoInReserve = data.ReserveCapacity;
            AimAngle = data.DefaultAimAngle;
        }
        
        public void SetAmmo(int amount, bool ignore = false)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"Trying to set the amount of ammo to a negative number" +
                                            $"\n Weapon Name: {_data.Name}");
            }

            if (amount > _data.MagCapacity && !ignore)
            {
                Debug.LogWarning($"Trying to set the ammo amount in mag to a number not in range." +
                                 $"\n Weapon Name: {_data.Name}; Mag Capacity: {_data.MagCapacity}; Amount to Set: {amount}");
            }
            else
            {
                AmmoInMag = amount;
            }
        }

        public void ReduceAmmo(int amount, bool ignore = false)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"Trying to reduce a negative amount of ammo" +
                                            $"\n Weapon Name: {_data.Name}");
            }

            if (AmmoInMag < amount && !ignore)
            {
                Debug.LogWarning($"Trying to reduce {amount} ammo in mag but there is only {AmmoInMag} ammo left." +
                                 $"\n Weapon Name: {_data.Name}; Ammo In Mag: {AmmoInMag}; Amount to Reduce: {amount}");
            }
            else
            {
                AmmoInMag -= amount;
                OnReduceAmmo?.Invoke();
            }
        }

        public void AddAmmo(int amount, bool ignore = false)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"Trying to add a negative amount of ammo" +
                                            $"\n Weapon Name: {_data.Name}");
            }

            if (AmmoInMag + amount > _data.MagCapacity && !ignore)
            {
                Debug.LogWarning($"Trying to add {amount} ammo into mag would cause ammo amount overflow." +
                                 $"\n Weapon Name: {_data.Name}; Ammo In Mag: {AmmoInMag}; Mag Capacity: {_data.MagCapacity}; " +
                                 $"Amount to Add: {amount}");
            }
            else
            {
                AmmoInMag += amount;
                OnAddAmmo?.Invoke();
            }
        }
    }
}