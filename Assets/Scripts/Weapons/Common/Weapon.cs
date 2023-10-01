using System;
using System.Collections.Generic;
using StackableElement;
using UISystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public GameObject Wielder { get; private set; }
        public Loadout Loadout { get; private set; }

        [SerializeField] private int _weaponID;
        public ItemDataEquipmentWeapon Data { get; private set; }
        public RuntimeItemDataEquipmentWeapon RuntimeData { get; private set; }
        [field: SerializeField] public Transform BulletParent { get; private set; }
        public Transform BulletSpawn { get; private set; }
        public Dictionary<EWeaponModule, WeaponModule> Modules { get; private set; }


        public Func<bool> MainFuncTrigger;
        public Action MainFunction;
        public Func<bool> MainFuncCancelCondition;
        public Action OnCancelMainFunc;

        public Func<bool> AltFuncTrigger;
        public Action AlternativeFunction;
        public Func<bool> AltFuncCancelCondition;
        public Action OnCancelAltFunc;

        public Func<bool> ReloadFuncTrigger;
        public Action ReloadFunction;

        private void Awake()
        {
            Loadout = GetComponentInParent<Loadout>();
            BulletSpawn = GameObject.Find("Bullet Spawn").GetComponent<Transform>();
            Modules = new Dictionary<EWeaponModule, WeaponModule>();

            if (IsValidWeaponID(_weaponID))
            {
                ChangeSourceData(_weaponID);
                LoadModule();
            }
        }

        public void ChangeSourceData(int id)
        {
            Data = DatabaseUtil.Instance.GetItemDataEquipmentWeapon(id);
            RuntimeData = new RuntimeItemDataEquipmentWeapon(Data);
        }

        public void Init()
        {
            BulletSpawn.localPosition = new Vector2(Data.BulletSpawnOffset[0], Data.BulletSpawnOffset[1]);

            Loadout.CurrenWeaponAnimator.runtimeAnimatorController =
                Resources.Load<AnimatorOverrideController>(Data.AnimatorOverrideControllerPath);
        }

        private void LoadModule()
        {
            Modules.Add(EWeaponModule.AmmunitionModule, gameObject.AddComponent<AmmunitionModule>());

            if (Data.HasFireModule)
            {
                Modules.Add(EWeaponModule.ShootingModule, gameObject.AddComponent<ShootingModule>());
            }

            if (Data.HasAimModule)
            {
                Modules.Add(EWeaponModule.AimingModule, gameObject.AddComponent<AimingModule>());
            }

            if (Data.HasChargeModule)
            {
                Modules.Add(EWeaponModule.ChargeModule, gameObject.AddComponent<ChargeModule>());
            }

            //Initialize modules
            foreach (var module in Modules.Values)
            {
                module.Init();
            }
        }

        private bool IsValidWeaponID(int id)
        {
            if (id > 100000 && id < 1000000)
            {
                return true;
            }

            throw new ArgumentException("Invalid Weapon ID.\n" +
                                        $"ID: {id}");
        }
    }
}