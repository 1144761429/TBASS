using System;
using System.Collections.Generic;
using BuffSystem.Common;
using UnityEngine.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem.DamagingEntities
{
    public class Projectile : DamagingEntity
    {
        public Action<GameObject, Projectile> OnHit;
        
        public ObjectPool<Projectile> ProjectilePool { get; private set; }
        public bool Traversing { get; set; }
        public float TraveledDistance { get; protected set; }
        public float MaxTravelDistance { get; protected set; }
        

        public void Update()
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

        public void Init(GameObject parent, ObjectPool<Projectile> pool, bool enableAfterInit)
        {
            base.Init(parent);
            
            ProjectilePool = pool;
            
            Traversing = enableAfterInit;

            MaxTravelDistance = 10;
        }

        public void ResetTraveledDistance()
        {
            TraveledDistance = 0;
        }
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            OnHit?.Invoke(other.gameObject, this);
        }
    }
}