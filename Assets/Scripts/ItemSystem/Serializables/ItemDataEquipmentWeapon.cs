using System;
using UnityEngine.Serialization;
using WeaponSystem;

[Serializable]
public class ItemDataEquipmentWeapon : ItemDataEquipment
{
    public EWeaponType WeaponType;
    public string DefaultAnimPath;
    public string FireAnimPath;
    public string AnimatorOverrideControllerPath;

    public int MagCapacity;
    public int ReserveCapacity;
    public float ReloadTime;

    public bool HasFireModule;
    public EFireMode FireMode;
    public float Damage;
    public float RPM;
    public EBulletType BulletType;
    public EInstantRegisterType InstantRegisterType;
    public string BulletAssetPath;
    public float[] BulletSpawnOffset;
    public float BulletSpeed;
    public float BulletTravelDistanceLimit;
    public int BulletPerFire;
    public float ShotTimeInterval;
    public float[] BulletSpreadAngles;

    public bool HasAimModule;
    public float DefaultAimAngle;
    public float MinAimAngle;
    public float AimSpeed;

    public bool HasChargeModule;
    public float ChargeThreshold;

    public override string ToString()
    {
        return base.ToString()
               + $"WeaponType: {WeaponType}\n"
               + $"MagCapacity: {MagCapacity}\n"
               + $"ReserveCapacity: {ReserveCapacity}\n"
               + $"ReloadTime: {ReloadTime}\n"
               + $"-----FIRE MODULE-----\n"
               + $"HasFireModule: {HasFireModule}\n"
               + $"FireMode: {FireMode}\n"
               + $"Damage: {Damage}\n"
               + $"RPM: {RPM}\n"
               + $"BulletType: {BulletType}\n"
               + $"InstantRegisterType: {InstantRegisterType}\n"
               + $"BulletSpawnOffset: {BulletSpawnOffset}\n"
               + $"BulletSpeed: {BulletSpeed}\n"
               + $"BulletTravelDistanceLimit: {BulletTravelDistanceLimit}\n"
               + $"BulletPerFire: {BulletPerFire}\n"
               + $"ShotTimeInterval: {ShotTimeInterval}\n"
               + $"BulletSpreadAngle: {BulletSpreadAngles}\n"
               + $"-----AIMING MODULE-----\n"
               + $"HasAimModule: {HasAimModule}\n"
               + $"DefaultAimAngle: {DefaultAimAngle}\n"
               + $"MinAimAngle: {MinAimAngle}\n"
               + $"AimSpeed: {AimSpeed}\n"
               + $"-----CHARGE MODULE-----\n"
               + $"HasChargeModule: {HasChargeModule}\n"
               + $"ChargeThreshold: {ChargeThreshold}\n";
    }
}