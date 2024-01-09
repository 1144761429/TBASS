using System;
using UISystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace WeaponSystem
{
    public enum ELoadoutSlot
    {
        Primary,
        Secondary,
        Adept
    }
    
    public class Loadout: MonoBehaviour
    {
        public GameObject graphicRep;
        
        public event Action<ELoadoutSlot> OnSwitchWeapon;
        public event Action<ChangeWeaponEventArgs> OnChangeWeapon;
        
        public Weapon CurrentWeapon;
        public ELoadoutSlot CurrentLoadoutSlot { get; private set; }

        [field:Space(30)]
        [field: SerializeField] public int PrimaryWeaponID { get; private set; }
        [field: SerializeField] public int SecondaryWeaponID { get; private set; }
        [field: SerializeField] public int AdeptWeaponID { get; private set; }
        
        [field:Space(30)]
        [field: SerializeField] public GameObject PrimaryWeaponContainer { get; private set; }
        [field: SerializeField] public GameObject SecondaryWeaponContainer { get; private set; }
        [field: SerializeField] public GameObject AdeptWeaponContainer { get; private set; }
        
        [field:Space(30)]
        [field: SerializeField] public Weapon PrimaryWeapon { get; private set; }
        [field: SerializeField] public Weapon SecondaryWeapon { get; private set; }
        [field: SerializeField] public Weapon AdeptWeapon { get; private set; }
        
        public WeaponEventBundle PrimaryWeaponEvents;
        public WeaponEventBundle SecondaryWeaponEvents;
        public WeaponEventBundle AdeptWeaponEvents;

        private bool _primaryInitialized;
        private bool _secondaryInitialized;
        private bool _adeptInitialized;

        public Transform BulletSpawnPos;

        
        private void Start()
        {
            if (PrimaryWeaponID != 0)
            {
                ChangeWeapon(ELoadoutSlot.Primary, PrimaryWeaponID);
            }

            if (SecondaryWeaponID != 0)
            {
                ChangeWeapon(ELoadoutSlot.Secondary, SecondaryWeaponID);
            }
            
            if (AdeptWeaponID != 0)
            {
                ChangeWeapon(ELoadoutSlot.Adept, AdeptWeaponID);
            }
            
            SwitchWeaponTo(ELoadoutSlot.Primary);
        }

        private void Update()
        {
            SwitchWeapon();
            
            switch (CurrentLoadoutSlot)
            {
                case ELoadoutSlot.Primary:
                    PrimaryWeaponFunction();
                    break;
                case ELoadoutSlot.Secondary:
                    SecondaryWeaponFunction();
                    break;
                case ELoadoutSlot.Adept:
                    AdeptWeaponFunction();
                    break;
            }
            
            // TODO: This should be in Loadout Graphic
            float actualProjectileAngle = Vector2.SignedAngle(Vector2.right, MouseUtil.GetVector2ToMouse(transform.position));
            if (MouseUtil.GetVector2ToMouse(transform.position).x > 0)
            {
                graphicRep.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                graphicRep.transform.localScale = new Vector3(-1, 1, 1);
                actualProjectileAngle = actualProjectileAngle - 180;
            }
            
            graphicRep.transform.localRotation = Quaternion.Euler(0, 0, actualProjectileAngle);
            BulletSpawnPos.localRotation = Quaternion.Euler(0, 0, actualProjectileAngle);
        }
        
        public void ChangeWeapon(ELoadoutSlot loadoutSlot,int id)
        {
            // Check if the argument weapon ID is valid
            if (!DatabaseUtil.IsValidWeaponID(id))
            {
                return;
            }
            
            // Retrieve the weapon from the database via ID
            ItemDataEquipmentWeapon staticData = DatabaseUtil.Instance.GetItemDataEquipmentWeapon(id);
            RuntimeItemDataEquipmentWeapon runtimeData = new RuntimeItemDataEquipmentWeapon(staticData);
            
            Weapon from = null; // The weapon before change.
            Weapon to = null; // The weapon after change.
            
            switch (loadoutSlot)
            {
                case ELoadoutSlot.Primary:
                    from = PrimaryWeapon;
                    
                    if (from != null)
                    {
                        Destroy(from.gameObject);
                    }
                    
                    if (staticData.WeaponType==EWeaponType.AR)
                    {
                        PrimaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Auto Rifle"),
                            PrimaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.SG)
                    {
                        PrimaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Shotgun"),
                            PrimaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.HG)
                    {
                        PrimaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Sidearm Harpoon"),
                            PrimaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    
                    // Load the required data and events for the weapon.
                    PrimaryWeapon.StaticData = staticData;
                    PrimaryWeapon.RuntimeData = runtimeData;
                    PrimaryWeapon.Init();
                    PrimaryWeaponEvents = PrimaryWeapon.Events;
                    
                    
                    // Build connection between the weapon and the loadout.
                    PrimaryWeapon.LoadoutSlot = ELoadoutSlot.Primary;
                    PrimaryWeapon.Loadout = this;
                    
                    to = PrimaryWeapon;
                    break;
                case ELoadoutSlot.Secondary:
                    from = SecondaryWeapon;
                    
                    if (from != null)
                    {
                        Destroy(from.gameObject);
                    }
                    
                    if (staticData.WeaponType==EWeaponType.AR)
                    {
                        SecondaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Auto Rifle"),
                            SecondaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.SG)
                    {
                        SecondaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Shotgun"),
                            SecondaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.HG)
                    {
                        SecondaryWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Sidearm"),
                            SecondaryWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    
                    // Load the required data and events for the weapon.
                    SecondaryWeapon.StaticData = staticData;
                    SecondaryWeapon.RuntimeData = runtimeData;
                    SecondaryWeapon.Init();
                    SecondaryWeaponEvents = SecondaryWeapon.Events;
                    
                    // Build connection between the weapon and the loadout.
                    SecondaryWeapon.LoadoutSlot = ELoadoutSlot.Primary;
                    SecondaryWeapon.Loadout = this;
                    
                    to = SecondaryWeapon;
                    break;
                case ELoadoutSlot.Adept:
                    from = AdeptWeapon;
                    
                    if (from != null)
                    {
                        Destroy(from.gameObject);
                    }
                    
                    if (staticData.WeaponType==EWeaponType.AR)
                    {
                        AdeptWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Auto Rifle"),
                            AdeptWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.SG)
                    {
                        AdeptWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Shotgun"),
                            AdeptWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    else if (staticData.WeaponType==EWeaponType.HG)
                    {
                        AdeptWeapon = Instantiate(Resources.Load<GameObject>("Prefabs/Weapons/Sidearm"),
                            AdeptWeaponContainer.transform).GetComponent<Weapon>();
                    }
                    
                    // Load the required data and events for the weapon.
                    AdeptWeapon.StaticData = staticData;
                    AdeptWeapon.RuntimeData = runtimeData;
                    AdeptWeapon.Init();
                    AdeptWeaponEvents = AdeptWeapon.Events;
                    
                    // Build connection between the weapon and the loadout.
                    AdeptWeapon.LoadoutSlot = ELoadoutSlot.Primary;
                    AdeptWeapon.Loadout = this;
                    
                    to = AdeptWeapon;
                    break;
            }

            ChangeWeaponEventArgs args = new ChangeWeaponEventArgs(from, to, loadoutSlot);
            
            OnChangeWeapon?.Invoke(args);
        }
        
        /// <summary>
        /// Switching Weapon between primary, secondary, and adept.
        /// </summary>
        private void SwitchWeapon()
        {
            if (PlayerInputHandler.IsSwitchToPrimaryWeaponPressedThisFrame && CurrentWeapon != PrimaryWeapon && PrimaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutSlot.Primary);
            }
            else if (PlayerInputHandler.IsSwitchToSecondaryWeaponPressedThisFrame && CurrentWeapon != SecondaryWeapon &&
                     SecondaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutSlot.Secondary);
            }
            else if (PlayerInputHandler.IsSwitchToAdeptWeaponPressedThisFrame && CurrentWeapon != AdeptWeapon && AdeptWeapon != null)
            {
                SwitchWeaponTo(ELoadoutSlot.Adept);
            }
        }
        
        /// <summary>
        /// Switch weapon to a specific type.
        /// </summary>
        /// <param name="target">The specific type of weapon to switch to. </param>
        private void SwitchWeaponTo(ELoadoutSlot target)
        {
            switch (target)
            {
                case ELoadoutSlot.Primary:
                    CurrentWeapon = PrimaryWeapon;
                    CurrentLoadoutSlot = ELoadoutSlot.Primary;

                    if (!_primaryInitialized)
                    {
                        PrimaryWeaponEvents = PrimaryWeapon.Events;
                        _primaryInitialized = true;
                    }

                    break;

                case ELoadoutSlot.Secondary:
                    CurrentWeapon = SecondaryWeapon;
                    CurrentLoadoutSlot = ELoadoutSlot.Secondary;

                    if (!_secondaryInitialized)
                    {
                        SecondaryWeaponEvents = SecondaryWeapon.Events;
                        _secondaryInitialized = true;
                    }

                    break;

                case ELoadoutSlot.Adept:
                    CurrentWeapon = AdeptWeapon;
                    CurrentLoadoutSlot = ELoadoutSlot.Adept;

                    if (!_adeptInitialized)
                    {
                        AdeptWeaponEvents = AdeptWeapon.Events;
                        _adeptInitialized = true;
                    }

                    break;
            }

            CurrentWeapon.Setup();

            OnSwitchWeapon?.Invoke(target);
        }
        
        private void PrimaryWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(PrimaryWeaponEvents.MainFuncTriggerCondition))
            {
                PrimaryWeaponEvents.MainFunc?.Invoke();
            }
            else
            {
                PrimaryWeaponEvents.MainFuncConditionFail?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponEvents.MainFuncCancelCondition))
            {
                PrimaryWeaponEvents.MainFuncCancelCallback?.Invoke();
            }

            
            if (FuncBoolUtil.Evaluate(PrimaryWeaponEvents.AltFuncTriggerCondition))
            {
                PrimaryWeaponEvents.AltFunc?.Invoke();
            }
            else
            {
                PrimaryWeaponEvents.AltFuncConditionFail?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(PrimaryWeaponEvents.AltFuncCancelCondition))
            {
                PrimaryWeaponEvents.AltFuncCancelCallback?.Invoke();
            }

            
            if (FuncBoolUtil.Evaluate(PrimaryWeaponEvents.ReloadTriggerCondition))
            {
                PrimaryWeaponEvents.ReloadCallback?.Invoke();
            }
        }
        private void SecondaryWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(SecondaryWeaponEvents.MainFuncTriggerCondition))
            {
                SecondaryWeaponEvents.MainFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponEvents.MainFuncCancelCondition))
            {
                SecondaryWeaponEvents.MainFuncCancelCallback?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponEvents.AltFuncTriggerCondition))
            {
                SecondaryWeaponEvents.AltFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponEvents.AltFuncCancelCondition))
            {
                SecondaryWeaponEvents.AltFuncCancelCallback?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(SecondaryWeaponEvents.ReloadTriggerCondition))
            {
                SecondaryWeaponEvents.ReloadCallback?.Invoke();
            }
        }
        private void AdeptWeaponFunction()
        {
            if (FuncBoolUtil.Evaluate(AdeptWeaponEvents.MainFuncTriggerCondition))
            {
                AdeptWeaponEvents.MainFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponEvents.MainFuncCancelCondition))
            {
                AdeptWeaponEvents.MainFuncCancelCallback?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponEvents.AltFuncTriggerCondition))
            {
                AdeptWeaponEvents.AltFunc?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponEvents.AltFuncCancelCondition))
            {
                AdeptWeaponEvents.AltFuncCancelCallback?.Invoke();
            }

            if (FuncBoolUtil.Evaluate(AdeptWeaponEvents.ReloadTriggerCondition))
            {
                AdeptWeaponEvents.ReloadCallback?.Invoke();
            }
        }
    }
}