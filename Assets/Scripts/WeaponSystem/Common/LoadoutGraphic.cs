using System;
using UISystem;
using UnityEngine;

namespace WeaponSystem
{
    public class LoadoutGraphic : MonoBehaviour
    {
        [SerializeField] private Loadout loadout;
        [SerializeField] private Animator animator;


        private void Awake()
        {
            loadout.OnSwitchWeapon += ChangeAnimatorController;
            loadout.OnChangeWeapon += ChangeAnimatorController;
        }

        private void ChangeAnimatorController(ChangeWeaponEventArgs args)
        {
            Weapon weapon = null;
            switch (args.LoadoutSlot)
            {
                case ELoadoutSlot.Primary:
                    weapon = loadout.PrimaryWeapon;
                    break;
                case ELoadoutSlot.Secondary:
                    weapon = loadout.SecondaryWeapon;
                    break;
                case ELoadoutSlot.Adept:
                    weapon = loadout.AdeptWeapon;
                    break;
            }

            if (weapon == null)
            {
                throw new NullReferenceException("Weapon is null.");
            }

            AnimatorOverrideController animatorOverrideController =
                Resources.Load<AnimatorOverrideController>(weapon.StaticData.AnimatorOverrideControllerPath);
            animator.runtimeAnimatorController = animatorOverrideController;
        }

        private void ChangeAnimatorController(ELoadoutSlot slot)
        {
            //Debug.Log("Change Animator Controller called.");
            Weapon weapon = null;
            switch (slot)
            {
                case ELoadoutSlot.Primary:
                    weapon = loadout.PrimaryWeapon;
                    break;
                case ELoadoutSlot.Secondary:
                    weapon = loadout.SecondaryWeapon;
                    break;
                case ELoadoutSlot.Adept:
                    weapon = loadout.AdeptWeapon;
                    break;
            }

            if (weapon == null)
            {
                throw new NullReferenceException("Weapon is null.");
            }

            AnimatorOverrideController animatorOverrideController =
                Resources.Load<AnimatorOverrideController>(weapon.StaticData.AnimatorOverrideControllerPath);
            animator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}