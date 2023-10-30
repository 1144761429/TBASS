using System;
using System.Collections.Generic;
using BuffSystem.Common;
using UnityEngine.Pool;
using UnityEngine;

namespace WeaponSystem.DamagingEntities
{
    public class Bullet : DamagingEntity
    {
        public event Action<GameObject> OnHitEnemy;
        
        public ObjectPool<Bullet> entityPool;
        
        public bool Traversing { get; set; }
        private float _traveledDistance;

        private void Update()
        {
            if (gameObject.activeSelf && Traversing)
            {
                _traveledDistance += Time.deltaTime * ParentWeapon.StaticData.BulletSpeed;

                if (_traveledDistance >= ParentWeapon.StaticData.BulletTravelDistanceLimit)
                {
                    entityPool.Release(this);
                }
            }
        }
        
        public void Init(Weapon weapon, ObjectPool<Bullet> pool, bool traverseAfterInit, Action<GameObject> onHitEnemy)
        {
            base.Init(weapon);

            entityPool = pool;
            
            Traversing = traverseAfterInit;

            OnHitEnemy = onHitEnemy;
        }
        
        public void Init(Weapon weapon, ObjectPool<Bullet> pool, bool traverseAfterInit, List<Action<GameObject>> onHitEnemy)
        {
            base.Init(weapon);

            entityPool = pool;
            
            Traversing = traverseAfterInit;

            foreach (var action in onHitEnemy)
            {
                OnHitEnemy += action;
            }
        }


        public void Reset()
        {
            _traveledDistance = 0;
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                entityPool.Release(this);
                print("You hit a obstacle");
                return;
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                // float finalDmg =
                //     ParentWeapon.StaticData.Damage * (1 + buffHandler.WeaponDamageModifier.GetWeaponDmgModValue() +
                //                                 buffHandler.FinalDamageModifier.GetFinalDmgModValue());

                OnHitEnemy?.Invoke(other.gameObject);
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(ParentWeapon.StaticData.Damage);
                }
                else
                {
                    Debug.Log("Enemy is invincible.");
                }

                entityPool.Release(this);
                print("You hit an Enemy");
            }
        }
    }
}