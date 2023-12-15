using BehaviorDesigner.Runtime;
using UnityEngine;
using WeaponSystem;

namespace BehaviorDesignerCustom
{
    public class SentryTurretAttack : SentryTurretAction
    {
        public ProjectilePattern ProjectilePattern;
        public SharedGameObject Target;

        public override void OnStart()
        {
            //Debug.Log("starts");
            
            if (ProjectilePattern == null)
            {
                //Debug.Log("Projectile pattern is empty.");
            }
            else
            {
                Attack();
            }
        }

        private void Attack()
        {
            Debug.Log("Sentry Turret is shooting!!!");
            
        }
    }
}