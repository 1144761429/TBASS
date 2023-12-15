using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using WeaponSystem.Modification;

public class HarpoonMod : WeaponModification
{
    public override EWeaponModificationType ModificationType => EWeaponModificationType.Harpoon;
    
    public bool isInHarpoonMode;
    private WeaponEventBundle weaponEvents;

    public GameObject target;

    public override void Init(Weapon weapon,ModuleDependencyHandler dependencyHandler)
    {
        base.Init(weapon, dependencyHandler);

        if (!dependencyHandler.HasShootingModule)
        {
            Debug.Log($"Harpoon Modification on weapon {weapon.StaticData.Name} initialization failed. " +
                      $"ShootingModule is required for this modification to work.");
            return;
        }
        
        weaponEvents = weapon.Events;

        weaponEvents.AltFuncTriggerCondition += () => PlayerInputHandler.IsWeaponAltFuncPressedThisFrame;
        weaponEvents.AltFunc += Release;
        
        dependencyHandler.SecondaryShootCondition += IsNotInHarpoonMode;
        dependencyHandler.ShootConditionFail += Pull;
        
        //If shooting multiple projectiles before OnHitEnemy is invoked, target will constantly change whenever a projectile hits an enemy.
        //Therefore, target will be the final enemy that is shot.
        dependencyHandler.OnHitEnemy += SetTarget;
        dependencyHandler.OnHitEnemy += EnterHarpoonMode;
    }

    private void Pull()
    {
        if (isInHarpoonMode)
        {
            target.transform.position = transform.position;
            
            isInHarpoonMode = false;
        }   
    }

    private void Release()
    {
        if (isInHarpoonMode)
        {
            target = null;
            
            isInHarpoonMode = false;
        }
    }
    
    private bool IsNotInHarpoonMode()
    {
        return !isInHarpoonMode;
    }
    
    private void EnterHarpoonMode(GameObject gameObj)
    {
        isInHarpoonMode = true;
    }

    private void ExitHarpoonMode()
    {
        isInHarpoonMode = false;
    }

    private void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
