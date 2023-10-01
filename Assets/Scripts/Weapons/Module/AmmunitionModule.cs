using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Mono.Cecil;

namespace WeaponSystem
{
    public class AmmunitionModule : WeaponModule
    {
        public int CurrentAmmoNumInReserve
        {
            get { return _runtimeData.AmmoInReserve; }
            set { _runtimeData.AmmoInReserve = value; }
        }

        public bool CanReload
        {
            get { return _runtimeData.AmmoInMag < _data.MagCapacity; }
        }

        private Transform _bulletParent;
        private ObjectPool<Bullet> _bulletObjPool;
        private bool _isReloading;

        /// <summary>
        /// Event for what a bullet should do after getting it from the bullet object pool.
        /// This event is called for multiple times if each shot of a weapon shoot multiple bullet. E.g., a shotgun.
        /// </summary>
        public Action<Bullet> OnGetBulletFromPool;

        public Func<bool> ReloadTrigger;
        public Func<bool> CanReloadCondition;
        public Action ActionBeforeReload;
        public Action AcitonOnReload;
        public Action ActionAfterReload;

        protected override void Awake()
        {
            base.Awake();

            _bulletObjPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true,
                30, 50);
            _bulletParent = _weapon.BulletParent;
        }

        private void OnEnable()
        {
            ReloadTrigger += ReloadPressed;
            CanReloadCondition += MagIsNotFull;
            CanReloadCondition += HaveAmmoReserve;

            _weapon.ReloadFuncTrigger += ReloadTrigger;
            _weapon.ReloadFunction += StartReload;
        }

        public override void Init()
        {
            base.Init();

            _runtimeData.SetAmmo(_data.MagCapacity);
            CurrentAmmoNumInReserve = _data.ReserveCapacity;
        }

        public Bullet[] GetBullet(int amount)
        {
            if (_runtimeData.AmmoInMag <= 0)
            {
                return null;
            }

            Bullet[] bullets = new Bullet[5];

            for (int i = 0; i < amount; i++)
            {
                bullets[i] = _bulletObjPool.Get();
            }

            _runtimeData.ReduceAmmo(1);
            return bullets;
        }

        public bool IsReloading()
        {
            return _isReloading;
        }

        public bool IsNotReloading()
        {
            return !_isReloading;
        }

        public bool HaveAmmoInMag()
        {
            return _runtimeData.AmmoInMag > 0;
        }

        public bool HaveAmmoReserve()
        {
            return CurrentAmmoNumInReserve > 0;
        }

        public bool MagIsNotFull()
        {
            return _runtimeData.AmmoInMag < _data.MagCapacity;
        }

        private IEnumerator ReloadProcess()
        {
            if (FuncBoolUtil.Evaluate(CanReloadCondition))
            {
                ActionBeforeReload?.Invoke();
                yield return Reload();
                AcitonOnReload?.Invoke();
                _isReloading = false;
                ActionAfterReload?.Invoke();
            }
        }

        private bool ReloadPressed()
        {
            return _weapon.Loadout.InputHandler.ReloadKeyPressed;
        }

        private void StartReload()
        {
            StartCoroutine(ReloadProcess());
        }

        private IEnumerator Reload()
        {
            _isReloading = true;
            print("Reloading...");
            yield return new WaitForSecondsRealtime(_data.ReloadTime);

            int ammoNeeded = _data.MagCapacity - _runtimeData.AmmoInMag;
            if (CurrentAmmoNumInReserve >= ammoNeeded)
            {
                _runtimeData.SetAmmo(_data.MagCapacity);
                CurrentAmmoNumInReserve -= ammoNeeded;
            }
            else
            {
                //_runtimeData.AmmoInMag += CurrentAmmoNumInReserve;
                _runtimeData.AddAmmo(CurrentAmmoNumInReserve);
                CurrentAmmoNumInReserve = 0;
            }

            print("Reloaded!");
        }

        #region Object Pool Methods

        private Bullet CreateBullet()
        {
            GameObject bulletGameObj =
                Instantiate(Resources.Load<GameObject>(_data.BulletAssetPath), _bulletParent, true);
            bulletGameObj.name = $"{_data.Name} bullet {_bulletObjPool.CountAll + 1}";
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullet.Init(_weapon, _bulletObjPool);
            return bullet;
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            OnGetBulletFromPool?.Invoke(bullet);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);

            bullet.Reset();
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        #endregion
    }
}