using System.Linq;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class AimingModule : WeaponModule
    {
        private Transform _bulletSpawn;
        [SerializeField] private AimIndicatorController _aimIndicatorController;
        private AmmunitionModule _ammunitionModule;

        public Func<bool> AimTrigger;
        public Func<bool> CanAimCondition;
        public Action ActionBeforeAim;
        public Action ActionOnAim;
        public Func<bool> AimCancelCondition;
        public Action ActionOnCancelingAim;
        public Action ActionAfterAiming;

        protected override void Awake()
        {
            base.Awake();
            _bulletSpawn = _weapon.BulletSpawn;
            _aimIndicatorController = GameObject
                .Instantiate(Resources.Load<GameObject>("Prefabs/Aim Laser"), _bulletSpawn, false)
                .GetComponent<AimIndicatorController>();
            HideAimVisual();

            if (!TryGetComponent<AmmunitionModule>(out _ammunitionModule))
            {
                throw new Exception("Ammunition Module is required to properly use Shooting Module");
            }
        }

        private void OnEnable()
        {
            AimTrigger += AimHeld;
            CanAimCondition += _ammunitionModule.IsNotReloading;
            ActionBeforeAim += DisplayAimVisual;
            ActionOnAim += ShrinkAimAngle;
            AimCancelCondition += AimReleased;
            ActionOnCancelingAim += ResetAimAngle;
            ActionOnCancelingAim += HideAimVisual;

            _ammunitionModule.ActionBeforeReload += HideAimVisual;
            _ammunitionModule.ActionBeforeReload += ResetAimAngle;

            _weapon.AltFuncTrigger += AimTrigger;
            _weapon.AlternativeFunction += TryAim;
            _weapon.AltFuncCancelCondition += AimCancelCondition;
            _weapon.OnCancelAltFunc += ActionOnCancelingAim;
        }

        public void DisplayAimVisual()
        {
            //print("Aim shown");
            _aimIndicatorController.gameObject.SetActive(true);
        }

        public void HideAimVisual()
        {
            //print("Aim hided");
            _aimIndicatorController.gameObject.SetActive(false);
        }

        private void TryAim()
        {
            if (FuncBoolUtil.Evaluate(CanAimCondition))
            {
                ActionBeforeAim?.Invoke();
                ActionOnAim?.Invoke();
                ActionAfterAiming?.Invoke();
            }
        }

        private bool AimHeld()
        {
            return _weapon.Loadout.InputHandler.AltFunctionKeyHeld;
        }

        private bool AimReleased()
        {
            return _weapon.Loadout.InputHandler.AltFunctionKeyReleased;
        }

        private void ResetAimAngle()
        {
            _runtimeData.AimAngle = _data.DefaultAimAngle;
        }

        private void ShrinkAimAngle()
        {
            //print("Shrinking");

            _runtimeData.AimAngle -= Time.deltaTime * 10 * _data.AimSpeed;
            _runtimeData.AimAngle = Mathf.Clamp(_runtimeData.AimAngle, _data.MinAimAngle, _data.DefaultAimAngle);
            _aimIndicatorController.UpdateAimAngle(_runtimeData.AimAngle);
        }
    }
}