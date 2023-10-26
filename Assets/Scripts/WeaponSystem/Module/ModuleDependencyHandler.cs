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
                    _shootingModule.BeforeCheckShootCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.BeforeCheckShootCondition -= value;
            }
        }

        public event Func<bool> ShootCondition
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.ShootCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.ShootCondition -= value;
            }
        }
        
        public event Func<bool> SecondaryShootCondition
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.SecondaryShootCondition += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.SecondaryShootCondition -= value;
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

        public event Action ActionAfterShoot
        {
            add
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.AfterShoot += value;
            }
            remove
            {
                if (!ShootingModuleNullityCheck())
                    _shootingModule.AfterShoot -= value;
            }
        }
        
        #endregion
        
        #region Ammunition Module
        
        public event Action ActionBeforeReload
        {
            add
            {
                if (!AmmunitionModuleNullityCheck()) _ammunitionModule.ActionBeforeReload += value;
            }
            remove
            {
                if (!AmmunitionModuleNullityCheck()) _ammunitionModule.ActionBeforeReload -= value;
            }
        }
        
        public event Action<GameObject> OnHitEnemy
        {
            add
            {
                if (!AmmunitionModuleNullityCheck()) _ammunitionModule.OnHitEnemy += value;
            }
            remove
            {
                if (!AmmunitionModuleNullityCheck()) _ammunitionModule.OnHitEnemy -= value;
            }
        }

        public bool HaveAmmoInMag => !AmmunitionModuleNullityCheck() && _ammunitionModule.HaveAmmoInMag;

        public bool IsReloading => !AmmunitionModuleNullityCheck() && _ammunitionModule.IsReloading;

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
        private bool AmmunitionModuleNullityCheck()
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

