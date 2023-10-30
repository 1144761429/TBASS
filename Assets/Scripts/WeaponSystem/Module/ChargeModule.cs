using System;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// A class that represents the charging feature of a weapon. 
    /// </summary>
    public class ChargeModule : WeaponModule, IChargeable
    {
        public override EWeaponModule ModuleType => EWeaponModule.ChargeModule;

        #region IChargeable Delegates and Properties

        public Func<bool> ChargeCondition { get; set; }
        public Action OnChargeStart { get; set; }
        public Action OnCharging { get; set; }
        public Action OnFullyCharged { get; set; }
        public Action OnChargeCancel { get; set; }
        public Action OnFreeze { get; set; }
        public Action OnUnfreeze { get; set; }

        public float ChargeProgress { get; set; }
        public float ChargeSpeed => 1;

        public float ChargeThreshold
        {
            get => _weapon.StaticData.ChargeThreshold;
            set { }
        }

        public bool CanBePaused { get; set; }
        public bool IsFrozen { get; private set; }

        #endregion
        
        private PanelChargeModuleBar _panelChargeModuleBar;

        public ChargeModule(Weapon weapon, ItemDataEquipmentWeapon staticData,
            RuntimeItemDataEquipmentWeapon runtimeData) : base(weapon,
            weapon, staticData, runtimeData)
        {
            OnChargeStart += UIManagerScreenSpace.Instance.OpenPanelChargeModuleBar;
            OnCharging += UIManagerScreenSpace.Instance.UpdatePanelChargeModuleBar;
            OnChargeCancel += UIManagerScreenSpace.Instance.ClosePanelChargeModuleBar;
            
            //TODO: when switching weapon, close the PanelChargeModuleBar.

            _dependencyHandler.BeforeCheckShootCondition += () =>
            {
                if (ChargeProgress / ChargeThreshold < 1)
                {
                    Reset();
                }
            };
            _dependencyHandler.ShootCondition += () => ChargeProgress >= ChargeThreshold;
            _dependencyHandler.AfterShoot += Reset;
            
            _weapon.Events.MainFuncCancelCondition += () => WeaponInputHandler.Instance.MainFunctionKeyReleased;
            _weapon.Events.MainFuncCancelCallback += OnChargeCancel;
        }
        

        public override void ModuleUpdate()
        {
            if (_weapon.Loadout.CurrentWeapon.StaticData.HasChargeModule)
            {
                if (WeaponInputHandler.Instance.MainFunctionKeyPressed)
                {
                    OnChargeStart?.Invoke();
                }

                if (WeaponInputHandler.Instance.MainFunctionKeyHeld)
                {
                    ChargeProgress += ChargeSpeed * Time.deltaTime;
                    OnCharging?.Invoke();
                }
            }
        }

        #region IChargeable Methods

        public void Reset()
        {
            ChargeProgress = 0;
        }

        public void SetIsFrozen(bool isFrozen)
        {
            IsFrozen = isFrozen;
            if (IsFrozen)
            {
                OnFreeze?.Invoke();
            }
            else
            {
                OnUnfreeze?.Invoke();
            }
        }

        #endregion
    }
}