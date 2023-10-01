using System;
using UnityEngine;

namespace WeaponSystem
{
    public class ChargeModule : WeaponModule, IChargeable
    {
        #region IChargeable Delegates and Properties

        public Func<bool> ChargeCondition { get; set; }
        public Action ChargeStartCallback { get; set; }
        public Action OnChargingCallback { get; set; }
        public Action FullyChargedCallback { get; set; }
        public Action ChargeCancelCallback { get; set; }
        public Action OnPauseCallback { get; set; }
        public Action OnResumeCallback { get; set; }

        public float ChargeProgress { get; set; }
        public float ChargeSpeed => 1;

        public float ChargeThreshold
        {
            get => _weapon.Data.ChargeThreshold;
            set { }
        }

        public bool CanBePaused { get; set; }
        public bool IsPaused { get; private set; }

        #endregion

        private ShootingModule _shootModule;
        private PanelChargeModuleBar _panelChargeModuleBar;

        #region Monobehavior Methods

        protected override void Awake()
        {
            base.Awake();

            ChargeStartCallback += UIManagerScreenSpace.Instance.OpenPanelChargeModuleBar;
            OnChargingCallback += UIManagerScreenSpace.Instance.UpdatePanelChargeModuleBar;
            OnChargingCallback += () => { print("Charging"); };
            ChargeCancelCallback += UIManagerScreenSpace.Instance.ClosePanelChargeModuleBar;

            //TODO: when switching weapon, close the PanelChargeModuleBar.
        }

        private void Update()
        {
            if (_weapon.Loadout.CurrentWeapon.Data.HasChargeModule)
            {
                if (_weapon.Loadout.InputHandler.MainFunctionKeyPressed)
                {
                    ChargeStartCallback?.Invoke();
                }

                if (_weapon.Loadout.InputHandler.MainFunctionKeyHeld)
                {
                    ChargeProgress += ChargeSpeed * Time.deltaTime;
                    OnChargingCallback?.Invoke();
                }

                if (_weapon.Loadout.InputHandler.MainFunctionKeyReleased)
                {
                    Reset();
                    ChargeCancelCallback?.Invoke();
                }
            }
        }

        #endregion

        public override void Init()
        {
            base.Init();

            if (TryGetComponent(out _shootModule))
            {
                _shootModule.CanEnterOnShootCondition += () => ChargeProgress >= ChargeThreshold;
            }
        }

        #region IChargeable Methods

        public void Reset()
        {
            ChargeProgress = 0;
        }

        public void SetIsPaused(bool isPaused)
        {
            IsPaused = isPaused;
            if (IsPaused)
            {
                OnPauseCallback?.Invoke();
            }
            else
            {
                OnResumeCallback?.Invoke();
            }
        }

        #endregion
    }
}