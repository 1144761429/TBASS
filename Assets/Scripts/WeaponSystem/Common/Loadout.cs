using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace WeaponSystem
{
    public enum ELoadoutWeaponType
    {
        Primary,
        Secondary,
        Adept
    }
    
    public class Loadout: MonoBehaviour
    {
        public GameObject graphicRep;
        
        public event Action<ELoadoutWeaponType> OnSwitchWeapon;
        public event Action<ELoadoutWeaponType, Weapon, Weapon> OnChangeWeapon;
        public event Action OnInitializeWeapon;
        
        public Weapon CurrentWeapon;
        public ELoadoutWeaponType CurrentLoadoutWeaponType { get; private set; }

        [field:Space(50)]
        [field: SerializeField] public int PrimaryWeaponID { get; private set; }
        [field: SerializeField] public int SecondaryWeaponID { get; private set; }
        [field: SerializeField] public int AdeptWeaponID { get; private set; }
        
        [field:Space(50)]
        [field: SerializeField] public GameObject PrimaryWeaponContainer { get; private set; }
        [field: SerializeField] public GameObject SecondaryWeaponContainer { get; private set; }
        [field: SerializeField] public GameObject AdeptWeaponContainer { get; private set; }
        
        [field:Space(50)]
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

        private void Awake()
        {
            // OnSwitchWeapon += (_) =>
            // {
            //     Debug.Log("SwitchWeaponInvoke");
            // };
            // OnChangeWeapon += (prevW, newW) =>
            // {
            //     Debug.Log(prevW.StaticData.Name + "   " + newW.StaticData.Name);
            // };
        }

        private void Start()
        {
            if (PrimaryWeaponID != 0)
            {
                ChangeWeapon(ELoadoutWeaponType.Primary, PrimaryWeaponID);
                //Debug.Log("Primary Weapon Initialized");
            }

            if (SecondaryWeaponID != 0)
            {
                ChangeWeapon(ELoadoutWeaponType.Secondary, SecondaryWeaponID);
                //Debug.Log("Secondary Weapon Initialized");
            }
            
            if (AdeptWeaponID != 0)
            {
                ChangeWeapon(ELoadoutWeaponType.Adept, AdeptWeaponID);
                //Debug.Log("Adept Weapon Initialized");
            }
            
            SwitchWeaponTo(ELoadoutWeaponType.Primary);
        }

        private void Update()
        {
            SwitchWeapon();
            
            switch (CurrentLoadoutWeaponType)
            {
                case ELoadoutWeaponType.Primary:
                    PrimaryWeaponFunction();
                    break;
                case ELoadoutWeaponType.Secondary:
                    SecondaryWeaponFunction();
                    break;
                case ELoadoutWeaponType.Adept:
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
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                ChangeWeapon(ELoadoutWeaponType.Primary,100003);
            }
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                ChangeWeapon(ELoadoutWeaponType.Primary,100001);
            }
        }
        
        public void ChangeWeapon(ELoadoutWeaponType loadoutWeaponType,int id)
        {
            // Check if the argument weapon ID is valid
            if (!DatabaseUtil.IsValidWeaponID(id))
            {
                return;
            }
            
            // Retrieve the weapon from the database via ID
            ItemDataEquipmentWeapon staticData = DatabaseUtil.Instance.GetItemDataEquipmentWeapon(id);
            RuntimeItemDataEquipmentWeapon runtimeData = new RuntimeItemDataEquipmentWeapon(staticData);
            
            Weapon weaponToBeChanged = null;
            Weapon newWeapon = null;
            
            switch (loadoutWeaponType)
            {
                case ELoadoutWeaponType.Primary:
                    weaponToBeChanged = PrimaryWeapon;
                    
                    if (weaponToBeChanged != null)
                    {
                        Destroy(weaponToBeChanged.gameObject);
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
                    PrimaryWeapon.LoadoutWeaponType = ELoadoutWeaponType.Primary;
                    PrimaryWeapon.Loadout = this;
                    
                    newWeapon = PrimaryWeapon;
                    break;
                case ELoadoutWeaponType.Secondary:
                    weaponToBeChanged = SecondaryWeapon;
                    
                    if (weaponToBeChanged != null)
                    {
                        Destroy(weaponToBeChanged.gameObject);
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
                    SecondaryWeapon.LoadoutWeaponType = ELoadoutWeaponType.Primary;
                    SecondaryWeapon.Loadout = this;
                    
                    newWeapon = SecondaryWeapon;
                    break;
                case ELoadoutWeaponType.Adept:
                    weaponToBeChanged = AdeptWeapon;
                    
                    if (weaponToBeChanged != null)
                    {
                        Destroy(weaponToBeChanged.gameObject);
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
                    AdeptWeapon.LoadoutWeaponType = ELoadoutWeaponType.Primary;
                    AdeptWeapon.Loadout = this;
                    
                    newWeapon = SecondaryWeapon;
                    break;
            }

            OnChangeWeapon?.Invoke(loadoutWeaponType, weaponToBeChanged, newWeapon);
        }
        
        /// <summary>
        /// Switching Weapon between primary, secondary, and adept.
        /// </summary>
        private void SwitchWeapon()
        {
            if (WeaponInputHandler.Instance.SwitchToPrimaryKeyPressed && CurrentWeapon != PrimaryWeapon && PrimaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Primary);
            }
            else if (WeaponInputHandler.Instance.SwitchToSecondaryKeyPressed && CurrentWeapon != SecondaryWeapon &&
                     SecondaryWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Secondary);
            }
            else if (WeaponInputHandler.Instance.SwitchToAdeptKeyPressed && CurrentWeapon != AdeptWeapon && AdeptWeapon != null)
            {
                SwitchWeaponTo(ELoadoutWeaponType.Adept);
            }
        }
        
        /// <summary>
        /// Switch weapon to a specific type.
        /// </summary>
        /// <param name="target">The specific type of weapon to switch to. </param>
        private void SwitchWeaponTo(ELoadoutWeaponType target)
        {
            switch (target)
            {
                case ELoadoutWeaponType.Primary:
                    CurrentWeapon = PrimaryWeapon;
                    CurrentLoadoutWeaponType = ELoadoutWeaponType.Primary;

                    if (!_primaryInitialized)
                    {
                        //PrimaryWeaponEvents = PrimaryWeapon.Events;

                        OnInitializeWeapon?.Invoke();
                        _primaryInitialized = true;
                    }

                    break;

                case ELoadoutWeaponType.Secondary:
                    CurrentWeapon = SecondaryWeapon;
                    CurrentLoadoutWeaponType = ELoadoutWeaponType.Secondary;

                    if (!_secondaryInitialized)
                    {
                        SecondaryWeaponEvents = SecondaryWeapon.Events;

                        OnInitializeWeapon?.Invoke();
                        _secondaryInitialized = true;
                    }

                    break;

                case ELoadoutWeaponType.Adept:
                    CurrentWeapon = AdeptWeapon;
                    CurrentLoadoutWeaponType = ELoadoutWeaponType.Adept;

                    if (!_adeptInitialized)
                    {
                        AdeptWeaponEvents = AdeptWeapon.Events;

                        OnInitializeWeapon?.Invoke();
                        _adeptInitialized = true;
                    }

                    break;
            }

            CurrentWeapon.Setup();
            //Debug.Log(OnSwitchWeapon.GetInvocationList().Length);
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