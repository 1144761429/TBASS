using System;
using BehaviorDesigner.Runtime;
using UnityEngine;
using WeaponSystem;

namespace BehaviorDesignerCustom
{
    public class SentryGunAttack : SentryTurretAction,IAttackAction
    {
        public ProjectilePattern ProjectilePattern => sentryGun.ProjectilePattern;
        public SharedGameObject Target;

        public override void OnStart()
        {
            Debug.Log("Sentry Gun Attack starts");
            
            if (ProjectilePattern == null)
            {
                Debug.Log("Projectile pattern is empty.");
            }
            else
            {
                Attack();
            }
        }

        public Action<GameObject, IDamageable> BeforeAttack => throw new NotImplementedException();

        public Action<GameObject, IDamageable> OnAttack => throw new NotImplementedException();

        public Action<GameObject, IDamageable> AfterAttack => throw new NotImplementedException();
        
        public void Attack()
        {
            Debug.Log("Sentry Turret is shooting!!!");
            sentryGun.Launch();
        }
    }
}