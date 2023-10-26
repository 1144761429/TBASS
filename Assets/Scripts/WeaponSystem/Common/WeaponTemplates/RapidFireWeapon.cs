using System;

namespace WeaponSystem
{
    public abstract class RapidFireWeapon : RangedWeapon
    {
        private AmmunitionModule _ammunitionModule;
        private ShootingModule _shootingModule;

        protected override void InternalInit()
        {
            base.InternalInit();
            Modules.Add(EWeaponModule.AmmunitionModule, _ammunitionModule);
            Modules.Add(EWeaponModule.ShootingModule, _shootingModule);
            _ammunitionModule = new AmmunitionModule(this, StaticData, RuntimeData);
            _shootingModule = new ShootingModule(this, StaticData, RuntimeData, EProjectilePatternType.R1P1);
            
            DependencyHandler = new ModuleDependencyHandler(this, _ammunitionModule, _shootingModule, null, null);
        }

        public void UpdateModule()
        {
            _ammunitionModule.ModuleUpdate();
            _shootingModule.ModuleUpdate();
        }
    }
}