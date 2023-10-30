using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

using WeaponSystem.DamagingEntities;

namespace WeaponSystem
{
    /// <summary>
    /// A weapon module means that the weapon have ammunition.
    /// </summary>
    public class AmmunitionModule : WeaponModule
    {
        public override EWeaponModule ModuleType => EWeaponModule.AmmunitionModule;

        #region Module Event

        public event Func<bool> ReloadTrigger;
        public event Func<bool> CanReloadConditions;
        public event Action BeforeReload;
        public event Action OnReload;
        public event Action ActionAfterReload;
        /// <summary>
        /// Event for what a bullet should do after getting it from the bullet object pool.
        /// This event is called for multiple times if each shot of a weapon shoot multiple bullet. E.g., a shotgun.
        /// </summary>
        public event Action<Bullet> OnGetBulletFromPool;

        public event Action<GameObject> OnHitEnemy;
        
        #endregion

        #region Module Properties
        
        public int AmmoAmountInReserve
        {
            get => _runtimeData.AmmoInReserve;
            set => _runtimeData.AmmoInReserve = value;
        }
        
        public bool CanReload { get; private set; }
        public bool IsReloading { get; private set; }

        public bool HaveAmmoInMag
        {
            get
            {
                if (_runtimeData.AmmoInMag > 0)
                {
                    return true;
                }
                else
                {
                    Debug.Log($"No ammo in mag. Ammo in mag: {_runtimeData.AmmoInMag}");
                    return false;
                }
            }
        }

        public bool HaveAmmoInReserve
        {
            get
            {
                if (_runtimeData.AmmoInReserve > 0)
                {
                    return true;
                }
                else
                {
                    Debug.Log("No more ammo in reserve.");
                    return false;
                }
            }
        }

        public bool IsMagNotFull
        {
            get
            {
                if (_runtimeData.AmmoInMag < _staticData.MagCapacity)
                {
                    return true;
                }
                else
                {
                    Debug.Log("Mag is full.");
                    return false;
                }
            }
        } 
        
        #endregion
        
        private ObjectPool<Bullet> _bulletObjPool;
        private List<Action<GameObject>> OnHitEnemyEvents;

        public AmmunitionModule(Weapon weapon, ItemDataEquipmentWeapon staticData,
            RuntimeItemDataEquipmentWeapon runtimeData) : base(weapon,
            weapon, staticData, runtimeData)
        {
            CanReload = true;
            _bulletObjPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true,
                30, 50);
            
            ReloadTrigger += () => WeaponInputHandler.Instance.ReloadKeyPressed;
            CanReloadConditions += () => CanReload;
            CanReloadConditions += () => IsMagNotFull;
            CanReloadConditions += () => HaveAmmoInReserve;

            _weapon.Events.ReloadTriggerCondition += ReloadTrigger;
            _weapon.Events.ReloadCallback += () => _mono.StartCoroutine(ReloadProcedure());
            
            _runtimeData.SetAmmo(_staticData.MagCapacity);
            AmmoAmountInReserve = _staticData.ReserveCapacity;
        }

        /// <summary>
        /// Retrieve a bullet from the the magazine.
        /// </summary>
        /// <param name="amount"> Amount of bullet to be retrieved.</param>
        /// <param name="traverseAfterInit"> If the projectile is in motion after initialization. </param>
        /// <param name="onHitEnemy"> Event that will trigger when the projectile hit a GameObject tagged with Enemy. </param>
        /// <returns>An array of bullet.</returns>
        public Bullet[] GetBullet(int amount, bool traverseAfterInit = false)
        {
            // If there no bullets in the mag, then return null.
            if (_runtimeData.AmmoInMag <= 0)
            {
                return null;
            }

            Bullet[] bullets = new Bullet[amount];

            for (int i = 0; i < amount; i++)
            {
                bullets[i] = _bulletObjPool.Get();
                bullets[i].Init(_weapon, _bulletObjPool, traverseAfterInit, OnHitEnemy);
            }

            // Reduce the amount of ammo in mag by 1.
            // Reason: guns like shotgun shoot multiple bullets out at once, but in the mag, player only consumes one.
            _runtimeData.ReduceAmmo(1);
            
            return bullets;
        }
        
        /// <summary>
        /// Retrieve a bullet from the the magazine.
        /// </summary>
        /// <param name="amount"> Amount of bullet to be retrieved.</param>
        /// <param name="traverseAfterInit"> If the projectile is in motion after initialization. </param>
        /// <param name="onHitEnemy"> A list of events that will trigger when the projectile hit a GameObject tagged with Enemy. </param>
        /// <returns>An array of bullet.</returns>
        // public Bullet[] GetBullet(int amount, bool traverseAfterInit = false, List<Action<GameObject>> onHitEnemy = null)
        // {
        //     // If there no bullets in the mag, then return null.
        //     if (_runtimeData.AmmoInMag <= 0)
        //     {
        //         return null;
        //     }
        //
        //     Bullet[] bullets = new Bullet[amount];
        //
        //     for (int i = 0; i < amount; i++)
        //     {
        //         bullets[i] = _bulletObjPool.Get();
        //         bullets[i].Init(_weapon, _bulletObjPool, traverseAfterInit, onHitEnemy);
        //     }
        //
        //     // Reduce the amount of ammo in mag by 1.
        //     // Reason: guns like shotgun shoot multiple bullets out at once, but in the mag, player only consumes one.
        //     _runtimeData.ReduceAmmo(1);
        //     
        //     return bullets;
        // }

        /// <summary>
        /// The procedure of how reload should work.
        /// This method handles the different events that should happen during reloading.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ReloadProcedure()
        {
            // If all reload conditions are met, then start the reload procedure.
            if (FuncBoolUtil.Evaluate(CanReloadConditions))
            {
                BeforeReload?.Invoke();
                yield return Reload();
                OnReload?.Invoke();
                IsReloading = false;
                ActionAfterReload?.Invoke();
            }
            // Otherwise, directly return.
            else
            {
                Debug.Log("Can reload: "+CanReload);
                Debug.Log("Reload failed.");
            }
        }
        
        /// <summary>
        /// The core, actual reload method.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Reload()
        {
            IsReloading = true;
            Debug.Log("Reloading...");
            
            yield return new WaitForSecondsRealtime(_staticData.ReloadTime);

            // Calculate how many bullets should be loaded into the mag,
            // and how many bullets in the reserve should be reduced.
            int ammoNeeded = _staticData.MagCapacity - _runtimeData.AmmoInMag;
            if (AmmoAmountInReserve >= ammoNeeded)
            {
                _runtimeData.SetAmmo(_staticData.MagCapacity);
                AmmoAmountInReserve -= ammoNeeded;
            }
            else
            {
                _runtimeData.AddAmmo(AmmoAmountInReserve);
                AmmoAmountInReserve = 0;
            }

            Debug.Log("Reloaded!");
        }

        #region Object Pool Methods

        private Bullet CreateBullet()
        {
            // GameObject bulletGameObj =
            //     Object.Instantiate(Resources.Load<GameObject>(_staticData.BulletAssetPath), _weapon.BulletParent, true);
            
            GameObject bulletGameObj =
                Object.Instantiate(((RangedWeapon)_weapon).Projectile.gameObject, _weapon.BulletParent, true);
            
            bulletGameObj.name = $"{_staticData.Name} bullet {_bulletObjPool.CountAll + 1}";
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();

            return bullet;
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            OnGetBulletFromPool?.Invoke(bullet);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.Traversing = false;
            bullet.gameObject.SetActive(false);
            
            bullet.Reset();
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            Object.Destroy(bullet.gameObject);
        }

        #endregion
    }
}