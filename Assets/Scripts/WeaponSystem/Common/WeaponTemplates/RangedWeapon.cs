using UnityEngine.Serialization;
using WeaponSystem.DamagingEntities;

namespace WeaponSystem
{
    public abstract class RangedWeapon : Weapon
    {
        public abstract EWeaponType rangedWeaponType { get; }
        
        public abstract EBulletType ProjectileType { get; }
        
        public DamagingEntity Projectile;
    }
}