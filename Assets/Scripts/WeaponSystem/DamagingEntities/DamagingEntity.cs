using System;
using UnityEngine;
using UnityEngine.Pool;

namespace WeaponSystem.DamagingEntities
{
    public abstract class DamagingEntity : MonoBehaviour
    {
        public Weapon ParentWeapon;
       
        public Rigidbody2D Rb { get; protected set; }
        
        public void Init(Weapon weapon)
        {
            ParentWeapon = weapon;
            
            Rb = GetComponent<Rigidbody2D>();
        }
    }
}
