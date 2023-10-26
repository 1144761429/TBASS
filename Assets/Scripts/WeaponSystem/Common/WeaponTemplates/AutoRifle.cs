using System;
using UnityEngine;
using WeaponSystem.DamagingEntities;

namespace WeaponSystem
{
    public class AutoRifle:RapidFireWeapon
    {
        public override EWeaponType rangedWeaponType => EWeaponType.AR;
        public override EBulletType ProjectileType => EBulletType.Physic;
    }
}