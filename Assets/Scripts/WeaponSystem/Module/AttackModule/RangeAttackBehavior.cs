#if false

using UnityEngine;
using WeaponSystem.DamagingEntities;

namespace WeaponSystem.AttackModule
{
    public class RangeAttackBehavior : AttackBehavior
    {
        public override EAttackBehaviorType AttackBehaviorType => EAttackBehaviorType.Range;

        [SerializeField] private ProjectilePattern projectilePattern;
        [SerializeField] private Bullet projectile;
    }
}

#endif

