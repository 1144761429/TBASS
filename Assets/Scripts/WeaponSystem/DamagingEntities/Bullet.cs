using UnityEngine;
using UnityEngine.Pool;

namespace WeaponSystem.DamagingEntities
{
    public class Bullet : Projectile
    {
        public Weapon ParentWeapon { get; private set; }

        public ObjectPool<Bullet> ProjectilePool { get; private set; }

        private new void Update()
        {
            if (gameObject.activeSelf && Traversing)
            {
                TraveledDistance += Time.deltaTime * Rb.velocity.magnitude;
                
                if (TraveledDistance >= MaxTravelDistance)
                {
                    ProjectilePool.Release(this);
                }
            }
        }
        
        public void Init(Weapon weapon, GameObject parent, ObjectPool<Bullet> pool, bool enableAfterInit)
        {
            base.Init(weapon.gameObject);

            ProjectilePool = pool;
            
            Traversing = enableAfterInit;
            
            ParentWeapon = weapon;

            MaxTravelDistance = weapon.StaticData.BulletTravelDistanceLimit;
            
            OnHit = null;
            OnHit += OnHitSomething;
        }
        
        private void OnHitSomething(GameObject other, Projectile bullet)
        {
            //Debug.Log($"hit {other.name}");
            
            if (other.layer == LayerMask.NameToLayer("Obstacle"))
            {
                ProjectilePool.Release(this);
                return;
            }

            if (other.CompareTag("Enemy"))
            {
                // float finalDmg =
                //     ParentWeapon.StaticData.Damage * (1 + buffHandler.WeaponDamageModifier.GetWeaponDmgModValue() +
                //                                 buffHandler.FinalDamageModifier.GetFinalDmgModValue());
                
                if (other.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(ParentWeapon.StaticData.Damage);
                }

                ProjectilePool.Release(this);
            }
        }
    }
}