using System;
using UnityEngine;

namespace WeaponSystem
{
    public class LoadoutGraphic: MonoBehaviour
    {
        [SerializeField] private Loadout loadout;
        [SerializeField] private Animator animator;


        private void Awake()
        {
            loadout.OnSwitchWeapon += ChangeAnimatorController;
            loadout.OnChangeWeapon += (prevWeapon,newWeapon) =>
            {
                ELoadoutWeaponType currentLoadoutWeaponType = loadout.CurrentLoadoutWeaponType;
                
                // On changing weapon, only change the animator controller if the weapon to be changed is the one we are currently holding.
                if (prevWeapon != null && prevWeapon.LoadoutWeaponType == currentLoadoutWeaponType)
                {
                    ChangeAnimatorController(currentLoadoutWeaponType);
                }
            };
        }

        private void ChangeAnimatorController(ELoadoutWeaponType loadoutWeaponType)
        {
            Debug.Log("Change Animator Controller called.");
            Weapon weapon = null;
            switch (loadoutWeaponType)
            {
                case ELoadoutWeaponType.Primary:
                    weapon = loadout.PrimaryWeapon;
                    break;
                case ELoadoutWeaponType.Secondary:
                    weapon = loadout.SecondaryWeapon;
                    break;
                case ELoadoutWeaponType.Adept:
                    weapon = loadout.AdeptWeapon;
                    break;
            }

            if (weapon == null)
            {
                throw new NullReferenceException("Weapon is null.");
            }
            
            AnimatorOverrideController animatorOverrideController = Resources.Load<AnimatorOverrideController>(weapon.StaticData.AnimatorOverrideControllerPath);
            animator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}