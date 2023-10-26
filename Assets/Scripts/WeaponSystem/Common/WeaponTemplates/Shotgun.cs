namespace WeaponSystem
{
    public class Shotgun: RangedWeapon
    {
        public override EWeaponType rangedWeaponType => EWeaponType.SG;
        public override EBulletType ProjectileType => EBulletType.Physic;
        
        private AmmunitionModule _ammunitionModule;
        private ShootingModule _shootingModule;
        
        protected override void InternalInit()
        {
            base.InternalInit();
            
            _ammunitionModule = new AmmunitionModule(this, StaticData, RuntimeData);
            _shootingModule = new ShootingModule(this, StaticData, RuntimeData, EProjectilePatternType.R1P5);
            
            DependencyHandler = new ModuleDependencyHandler(this, _ammunitionModule, _shootingModule, null, null);
        }
    }
}