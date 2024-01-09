using System;
using UnityEngine;
using WeaponSystem.DamagingEntities;


namespace WeaponSystem
{
    /// <summary>
    /// <para>
    /// A communicator class for different modules of a weapon.
    /// </para>
    /// <para>
    /// This helps weapon modules script to be more organized and clean
    /// by avoiding creating references of other modules through calling GetComponent().
    /// </para>
    /// </summary>
    public class ModuleDependencyHandler
    {
        public bool HasShootingModule => !ShootingModuleNullityCheck();
        
        private AmmunitionModule _ammunitionModule;
        private ShootingModule _shootingModule;
        private AimingModule _aimingModule;
        private ChargeModule _chargeModule;
        
        private Weapon _weapon;
        
        
        #region Shooting Module
        
        public event Action BeforeCheckShootCondition
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.BeforeCheckLaunchCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.BeforeCheckLaunchCondition -= value;
            }
        }

        public event Func<bool> ShootCondition
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.PrimaryLaunchCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.PrimaryLaunchCondition -= value;
            }
        }
        
        public event Func<bool> SecondaryShootCondition
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.SecondaryLaunchCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.SecondaryLaunchCondition -= value;
            }
        }
        
        public event Action ShootConditionFail
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.OnShootConditionFail += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.OnShootConditionFail -= value;
            }
        }

        public event Action AfterShoot
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.AfterLaunch += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.AfterLaunch -= value;
            }
        }
        
        #endregion
        
        
        #region Ammunition Module
        
        public event Action BeforeReload
        {
            add
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.BeforeReload += value;
            }
            remove
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.BeforeReload -= value;
            }
        }
        public event Action OnReload
        {
            add
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.OnReload += value;
            }
            remove
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.OnReload -= value;
            }
        }
        
        public event Action<GameObject, Projectile> OnHitEnemy
        {
            add
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.OnHitEnemy += value;
            }
            remove
            {
                if (!IsAmmunitionModuleNull()) _ammunitionModule.OnHitEnemy -= value;
            }
        }

        public Bullet Bullet => IsAmmunitionModuleNull() ? null : _ammunitionModule.Bullet;
        public bool HaveAmmoInMag => !IsAmmunitionModuleNull() && _ammunitionModule.HaveAmmoInMag;
        public bool IsReloading => !IsAmmunitionModuleNull() && _ammunitionModule.IsReloading;

        public Bullet[] GetBullet(int amount)
        {
            return _ammunitionModule.GetBullet(amount);
        }

        #endregion
        
        
        public ModuleDependencyHandler(Weapon weapon,AmmunitionModule ammunitionModule,ShootingModule shootingModule,ChargeModule chargeModule,AimingModule aimingModule)
        {
            _weapon = weapon;
            _ammunitionModule = ammunitionModule;
            _shootingModule = shootingModule;
            _chargeModule = chargeModule;
            _aimingModule = aimingModule;
        }

        /// <summary>
        /// Check if the <c>ammunitionModule</c> is null.
        /// </summary>
        /// <returns>True if <c>ammunitionModule</c> is null. Otherwise, false.</returns>
        private bool IsAmmunitionModuleNull()
        {
            if (_ammunitionModule == null)
            {
                Debug.Log("Ammunition module is null");
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Check if the <c>shootingModule</c> is null.
        /// </summary>
        /// <returns>True if <c>shootingModule</c> is null. Otherwise, false.</returns>
        private bool ShootingModuleNullityCheck()
        {
            return _shootingModule == null;
        }
    }
}

