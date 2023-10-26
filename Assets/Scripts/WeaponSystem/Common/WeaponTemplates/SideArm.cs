namespace WeaponSystem
{
    public class SideArm:SingleFireWeapon
    {
        public override EWeaponType rangedWeaponType => EWeaponType.HG;
        public override EBulletType ProjectileType => EBulletType.Physic;
    }
}