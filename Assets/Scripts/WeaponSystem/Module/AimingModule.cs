using System;
using UnityEngine;

namespace WeaponSystem
{
    public class AimingModule : WeaponModule
    {
        public override EWeaponModule ModuleType => EWeaponModule.AimingModule;
        
        #region Module Events
        
        public event Func<bool> AimTrigger;
        public event Func<bool> AimCondition;
        public event Action ActionBeforeAim;
        public event Action ActionOnAim;
        public event Func<bool> AimCancelCondition;
        public event Action ActionOnCancelingAim;
        public event Action ActionAfterAiming;
        
        #endregion
        
        private Transform _bulletSpawn => _weapon.BulletSpawnPos;
        private ConeAimIndicator _coneAimIndicator;

        public AimingModule(Weapon weapon, ItemDataEquipmentWeapon staticData,
            RuntimeItemDataEquipmentWeapon runtimeData) : base(weapon,
            weapon, staticData, runtimeData)
        {
            _coneAimIndicator = GameObject
                .Instantiate(Resources.Load<GameObject>("Prefabs/Aim Laser"), _bulletSpawn, false)
                .GetComponent<ConeAimIndicator>();
            
            HideAimVisual();
            
            AimTrigger += () => WeaponInputHandler.Instance.AltFunctionKeyHeld;
            AimCondition += ()=> !_dependencyHandler.IsReloading;
            ActionBeforeAim += DisplayAimVisual;
            ActionOnAim += ShrinkAimCone;
            AimCancelCondition += () => WeaponInputHandler.Instance.AltFunctionKeyReleased;
            ActionOnCancelingAim += HideAimVisual;
            ActionOnCancelingAim += ResetAimAngle;

            _weapon.Events.AltFuncTriggerCondition += AimTrigger;
            _weapon.Events.AltFunc += StartAim;
            _weapon.Events.AltFuncCancelCondition += AimCancelCondition;
            _weapon.Events.AltFuncCancelCallback += ActionOnCancelingAim;
            
            _dependencyHandler.ActionBeforeReload += HideAimVisual;
            _dependencyHandler.ActionBeforeReload += ResetAimAngle;
            _dependencyHandler.ActionAfterShoot += ResetAimAngle;
        }

        /// <summary>
        /// Display the aim indicator.
        /// </summary>
        private void DisplayAimVisual()
        {
            _coneAimIndicator.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide the aim indicator.
        /// </summary>
        private void HideAimVisual()
        {
            _coneAimIndicator.gameObject.SetActive(false);
        }

        /// <summary>
        /// Start the procedure of aim.
        /// </summary>
        private void StartAim()
        {
            // Only aim if aim conditions are passed.
            if (FuncBoolUtil.Evaluate(AimCondition))
            {
                ActionBeforeAim?.Invoke();
                ActionOnAim?.Invoke();
                ActionAfterAiming?.Invoke();
            }
        }

        /// <summary>
        /// Reset the the aim angle in runtime data.
        /// </summary>
        private void ResetAimAngle()
        {
            _runtimeData.AimAngle = _staticData.DefaultAimAngle;
        }

        /// <summary>
        /// Shrink the the degree of aim cone.
        /// </summary>
        private void ShrinkAimCone()
        {
            _runtimeData.AimAngle -= Time.deltaTime * 10 * _staticData.AimSpeed;
            _runtimeData.AimAngle = Mathf.Clamp(_runtimeData.AimAngle, _staticData.MinAimAngle, _staticData.DefaultAimAngle);
            _coneAimIndicator.UpdateAimAngle(_runtimeData.AimAngle);
        }
    }
}