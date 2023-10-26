using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public enum EPlayerLoadoutSlotHierarchicalPos
    {
        Top,
        Mid,
        Bot
    }

    public class PanelPlayerLoadoutSlot : PanelBase
    {
        private Loadout _loadout;
        private RectTransform _rectTransform;
        [field: SerializeField] public EPlayerLoadoutSlotHierarchicalPos HierarchicalPos { get; private set; }

        [SerializeField] private ELoadoutWeaponType _correspondLoadoutWeaponType;
        [SerializeField] private Weapon _weapon;
        private ItemDataEquipmentWeapon _weaponData;
        private RuntimeItemDataEquipmentWeapon _runtimeWeaponData;
        private AmmunitionModule _ammunitionModule;

        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TMP_Text _ammoInfo;

        private readonly Vector2 SIZE_TOP = new Vector2(150, 70);
        private readonly Vector2 SIZE_MID = new Vector2(120, 56);
        private readonly Vector2 SIZE_BOT = new Vector2(96, 44);
        

        public override void Init()
        {
            base.Init();
            _loadout = GetComponentInParent<PanelPlayerLoadout>().PlayerLoadout;
            _rectTransform = GetComponent<RectTransform>();

            switch (_correspondLoadoutWeaponType)
            {
                case ELoadoutWeaponType.Primary:
                    _runtimeWeaponData = _loadout.PrimaryWeapon.RuntimeData;
                    break;
                case ELoadoutWeaponType.Secondary:
                    _runtimeWeaponData = _loadout.SecondaryWeapon.RuntimeData;
                    break;
                case ELoadoutWeaponType.Adept:
                    _runtimeWeaponData = _loadout.AdeptWeapon.RuntimeData;
                    break;
            }

            _runtimeWeaponData.OnReduceAmmo += UpdateAmmoInfo;

            if (_weapon.Modules.TryGetValue(EWeaponModule.AmmunitionModule, out var module))
            {
                _ammunitionModule = (AmmunitionModule)module;
                _ammunitionModule.ActionOnReload += UpdateAmmoInfo;
            }
            else
            {
                Debug.LogWarning($"The weapon in loadout slot {gameObject.name} has no AmmunitionModule");
            }
            
            UpdateWeaponData();
            UpdateWeaponIcon();
            UpdateAmmoInfo();
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos pos)
        {
            HierarchicalPos = pos;
            UpdateSlotVisual();
        }
        public void UpdateWeaponData()
        {
            _weaponData = _weapon.StaticData;
            _runtimeWeaponData = _weapon.RuntimeData;
        }

        public void UpdateWeaponIcon()
        {
            print(_weaponData.Name);
            _weaponIcon.sprite = Resources.Load<Sprite>(_weaponData.SpritePath);
        }
        
        private void UpdateSlotVisual()
        {
            ChangeSize(HierarchicalPos);
        }
        
        private void UpdateAmmoInfo()
        {
            _ammoInfo.text = $"{_runtimeWeaponData.AmmoInMag} I {_runtimeWeaponData.AmmoInReserve}";
        }

        private void ChangeSize(EPlayerLoadoutSlotHierarchicalPos pos)
        {
            switch (pos)
            {
                case EPlayerLoadoutSlotHierarchicalPos.Top:
                    _rectTransform.sizeDelta = SIZE_TOP;
                    break;
                case EPlayerLoadoutSlotHierarchicalPos.Mid:
                    _rectTransform.sizeDelta = SIZE_MID;
                    break;
                case EPlayerLoadoutSlotHierarchicalPos.Bot:
                    _rectTransform.sizeDelta = SIZE_BOT;
                    break;
            }
        }
    }
}