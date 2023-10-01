using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    public enum ELoadoutWeaponType
    {
        Primary,
        Secondary,
        Adept
    }

    public class Loadout : MonoBehaviour
    {
        [field: SerializeField] public Weapon PrimaryWeapon { get; private set; }
        [field: SerializeField] public Weapon SecondaryWeapon { get; private set; }
        [field: SerializeField] public Weapon AdeptWeapon { get; private set; }
        public Weapon CurrentWeapon { get; private set; }
        public ELoadoutWeaponType CurrentWeaponEnum { get; private set; }
        private bool _primaryInitialized;
        private bool _secondaryInitialized;
        private bool _adeptInitialized;

        public WeaponInputHandler InputHandler { get; private set; }
        public Animator CurrenWeaponAnimator { get; private set; }
        private AnimationEventHandler _animationEventHandler;
        private Transform _bulletSpawn;
        private PlayerMovement _player;


        #region Primary Weapon Event

        public Func<bool> PrimaryWeaponMainFuncTrigger;
        public Action PrimaryWeaponMainFunction;
        public Func<bool> PrimaryWeaponMainFuncCancelCondition;
        public Action OnPrimaryWeaponCancelMainFunc;
        public Func<bool> PrimaryWeaponAltFuncTrigger;
        public Action PrimaryWeaponAltFunction;
        public Func<bool> PrimaryWeaponAltFuncCancelCondition;
        public Action OnPrimaryWeaponCancelAltFunc;
        public Func<bool> PrimaryWeaponReloadFuncTrigger;
        public Action PrimaryWeaponReloadFunciton;

        #endregion

        #region Secondary Weapon Event

        public Func<bool> SecondaryWeaponMainFuncTrigger;
        public Action SecondaryWeaponMainFunction;
        public Func<bool> SecondaryWeaponMainFuncCancelCondition;
        public Action OnSecondaryWeaponCancelMainFunc;
        public Func<bool> SecondaryWeaponAltFuncTrigger;
        public Action SecondaryWeaponAltFunction;
        public Func<bool> SecondaryWeaponAltFuncCancelCondition;
        public Action OnSecondaryWeaponCancelAltFunc;
        public Func<bool> SecondaryWeaponReloadFuncTrigger;
        public Action SecondaryWeaponReloadFunciton;

        #endregion

        #region Adept Weapon Event

        public Func<bool> AdeptWeaponMainFuncTrigger;
        public Action AdeptWeaponMainFunction;
        public Func<bool> AdeptWeaponMainFuncCancelCondition;
        public Action OnAdeptWeaponCancelMainFunc;
        public Func<bool> AdeptWeaponAltFuncTrigger;
        public Action AdeptWeaponAltFunction;
        public Func<bool> AdeptWeaponAltFuncCancelCondition;
        public Action OnAdeptWeaponCancelAltFunc;
        public Func<bool> AdeptWeaponReloadFuncTrigger;
        public Action AdeptWeaponReloadFunciton;

        #endregion

        #region General Event

        public Action OnInitializeWeapon;
        public Action OnSwitchWeapon;

        #endregion

        private void Awake()
        {
            _player = GetComponentInParent<PlayerMovement>();
            CurrenWeaponAnimator = GetComponentInChildren<Animator>();
            _animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
            InputHandler = GetComponentInChildren<WeaponInputHandler>();
            _bulletSpawn = GameObject.Find("Bullet Spawn").GetComponent<Transform>();

            OnSwitchWeapon += () =>
            {
                if (CurrentWeapon.Data.HasChargeModule)
                {
                    if (CurrentWeapon.gameObject.TryGetComponent(out ChargeModule chargeModule))
                    {
                        UIManagerScreenSpace.Instance.ChargeModuleBarPanel.SetChargeModule(chargeModule);
                    }
                    else
                    {
                        Debug.LogError(
                            $"Weapon {CurrentWeapon.Data.Name} in slot {CurrentWeaponEnum} has a ChargeModule in database, " +
                            $"but there is no ChargeModule script on the GameObject.");
                    }
                }
                else
                {
                    UIManagerScreenSpace.Instance.ChargeModuleBarPanel.SetChargeModule(null);
                }
            };
        }

        private void OnEnable()
        {
            _animationEventHandler.OnFinished += ShootAnimExist;
        }

        private void OnDisable()
        {
            _animationEventHandler.OnFinished -= ShootAnimExist;
        }

        private void Start()
        {
            SwitchWeaponTo(ELoadoutWeaponType.Primary);
        }

        private void Update()
        {
            SwitchWeapon();
            RotateWeapon();

            if (CurrentWeapon == PrimaryWeapon)
            {
                PrimaryWeaponFunction();
            }
            else if (CurrentWeapon == SecondaryWeapon)
            {
                SecondaryWeaponFunction();
            }
            else if (CurrentWeapon == AdeptWeapon)
            {
                AdeptWeaponFunction();
            }
        }

        private void PrimaryWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(PrimaryWeaponMainFuncTrigger))
            {
                PrimaryWeaponMainFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponMainFuncCancelCondition))
            {
                OnPrimaryWeaponCancelMainFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponAltFuncTrigger))
            {
                PrimaryWeaponAltFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponAltFuncCancelCondition))
            {
                OnPrimaryWeaponCancelAltFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponReloadFuncTrigger))
            {
                PrimaryWeaponReloadFunciton?.Invoke();
            }
        }

        private void SecondaryWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(SecondaryWeaponMainFuncTrigger))
            {
                SecondaryWeaponMainFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponAltFuncTrigger))
            {
                SecondaryWeaponAltFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponAltFuncCancelCondition))
            {
                OnSecondaryWeaponCancelAltFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponReloadFuncTrigger))
            {
                SecondaryWeaponReloadFunciton?.Invoke();
            }
        }

        private void AdeptWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(AdeptWeaponMainFuncTrigger))
            {
                AdeptWeaponMainFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponAltFuncTrigger))
            {
                AdeptWeaponAltFunction?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponAltFuncCancelCondition))
            {
                OnAdeptWeaponCancelAltFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponReloadFuncTrigger))
            {
                AdeptWeaponReloadFunciton?.Invoke();
            }
        }

        private void SwitchWeapon()
        {
            if (InputHandler.SwitchToPrimaryKeyPressed && CurrentWeapon != PrimaryWeapon && PrimaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Primary);
            }
            else if (InputHandler.SwitchToSecondaryKeyPressed && CurrentWeapon != SecondaryWeapon &&
                     SecondaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Secondary);
            }
            else if (InputHandler.SwitchToAdeptKeyPressed && CurrentWeapon != AdeptWeapon && AdeptWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Adept);
            }
        }

        private void SwitchWeaponTo(ELoadoutWeaponType target)
        {
            switch (target)
            {
                case ELoadoutWeaponType.Primary:
                    CurrentWeapon = PrimaryWeapon;
                    CurrentWeaponEnum = ELoadoutWeaponType.Primary;

                    if (!_primaryInitialized)
                    {
                        PrimaryWeaponMainFuncTrigger += PrimaryWeapon.MainFuncTrigger;
                        PrimaryWeaponMainFunction += PrimaryWeapon.MainFunction;
                        PrimaryWeaponMainFuncCancelCondition += PrimaryWeapon.MainFuncCancelCondition;
                        OnPrimaryWeaponCancelMainFunc += PrimaryWeapon.OnCancelMainFunc;

                        PrimaryWeaponAltFuncTrigger += PrimaryWeapon.AltFuncTrigger;
                        PrimaryWeaponAltFunction += PrimaryWeapon.AlternativeFunction;
                        PrimaryWeaponAltFuncCancelCondition += PrimaryWeapon.AltFuncCancelCondition;
                        OnPrimaryWeaponCancelAltFunc += PrimaryWeapon.OnCancelAltFunc;

                        PrimaryWeaponReloadFuncTrigger += PrimaryWeapon.ReloadFuncTrigger;
                        PrimaryWeaponReloadFunciton += PrimaryWeapon.ReloadFunction;

                        OnInitializeWeapon?.Invoke();
                        _primaryInitialized = true;
                    }

                    break;

                case ELoadoutWeaponType.Secondary:
                    CurrentWeapon = SecondaryWeapon;
                    CurrentWeaponEnum = ELoadoutWeaponType.Secondary;

                    if (!_secondaryInitialized)
                    {
                        SecondaryWeaponMainFuncTrigger += SecondaryWeapon.MainFuncTrigger;
                        SecondaryWeaponMainFunction += SecondaryWeapon.MainFunction;
                        SecondaryWeaponMainFuncCancelCondition += SecondaryWeapon.MainFuncCancelCondition;
                        OnSecondaryWeaponCancelMainFunc += SecondaryWeapon.OnCancelMainFunc;

                        SecondaryWeaponAltFuncTrigger += SecondaryWeapon.AltFuncTrigger;
                        SecondaryWeaponAltFunction += SecondaryWeapon.AlternativeFunction;
                        SecondaryWeaponAltFuncCancelCondition += SecondaryWeapon.AltFuncCancelCondition;
                        OnSecondaryWeaponCancelAltFunc += SecondaryWeapon.OnCancelAltFunc;

                        SecondaryWeaponReloadFuncTrigger += SecondaryWeapon.ReloadFuncTrigger;
                        SecondaryWeaponReloadFunciton += SecondaryWeapon.ReloadFunction;

                        OnInitializeWeapon?.Invoke();
                        _secondaryInitialized = true;
                    }

                    break;

                case ELoadoutWeaponType.Adept:
                    CurrentWeapon = AdeptWeapon;
                    CurrentWeaponEnum = ELoadoutWeaponType.Adept;

                    if (!_adeptInitialized)
                    {
                        AdeptWeaponMainFuncTrigger += AdeptWeapon.MainFuncTrigger;
                        AdeptWeaponMainFunction += AdeptWeapon.MainFunction;
                        AdeptWeaponMainFuncCancelCondition += AdeptWeapon.MainFuncCancelCondition;
                        OnAdeptWeaponCancelMainFunc += AdeptWeapon.OnCancelMainFunc;

                        AdeptWeaponAltFuncTrigger += AdeptWeapon.AltFuncTrigger;
                        AdeptWeaponAltFunction += AdeptWeapon.AlternativeFunction;
                        AdeptWeaponAltFuncCancelCondition += AdeptWeapon.AltFuncCancelCondition;
                        OnAdeptWeaponCancelAltFunc += AdeptWeapon.OnCancelAltFunc;

                        AdeptWeaponReloadFuncTrigger += AdeptWeapon.ReloadFuncTrigger;
                        AdeptWeaponReloadFunciton += AdeptWeapon.ReloadFunction;

                        OnInitializeWeapon?.Invoke();
                        _adeptInitialized = true;
                    }

                    break;
            }

            CurrentWeapon.Init();
            OnSwitchWeapon?.Invoke();
        }

        /// <summary>
        /// Method for rotating the weapon according to where the mouse is aiming at, relative to the player
        /// </summary>
        private void RotateWeapon()
        {
            float angle = Vector2.SignedAngle(Vector2.right, MouseUtil.GetVector2ToMouse(transform.position));
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

            if (_player.Forward == Vector2.right)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_player.Forward == Vector2.left)
            {
                transform.localScale = new Vector3(-1, -1, 1);
            }
        }

        #region Animation

        private void ShootAnimEnter()
        {
            CurrenWeaponAnimator.SetBool("Fired", true);
        }

        private void ShootAnimExist()
        {
            CurrenWeaponAnimator.SetBool("Fired", false);
        }

        #endregion
    }
}