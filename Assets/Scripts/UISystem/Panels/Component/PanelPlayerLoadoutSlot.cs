using UnityEngine;
using WeaponSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

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
        [field: SerializeField] public EPlayerLoadoutSlotHierarchicalPos HierarchicalPos { get; private set; }
        
        private RectTransform _rectTransform;
        
        //[SerializeField] private ELoadoutWeaponType correspondLoadoutWeaponType;
        
        private Weapon _weapon;
        private ItemDataEquipmentWeapon _weaponData;
        private RuntimeItemDataEquipmentWeapon _runtimeWeaponData;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text ammoInfo;

        private readonly Vector2 SIZE_TOP = new Vector2(150, 70);
        private readonly Vector2 SIZE_MID = new Vector2(120, 56);
        private readonly Vector2 SIZE_BOT = new Vector2(96, 44);
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void Init()
        {
            //Debug.Log("Panel Player Loadout Slot Init called");
            base.Init();
        }

        /// <summary>
        /// Update the weapon and the data of this slot is linked to.
        /// </summary>
        /// <param name="weapon"></param>
        public void UpdateWeaponData(Weapon weapon)
        {
            _weapon = weapon;
            _weaponData = _weapon.StaticData;
            _runtimeWeaponData = _weapon.RuntimeData;
            
            // Assign the event here because in the weapon system, each weapon is an independent Weapon class,
            // meaning whenever we change the weapon, all the event of the old weapon will not be applied to the new weapon.
            // Therefore, it is necessary to assign the event again.
            _weapon.RuntimeData.OnReduceAmmo += UpdateAmmoInfo;
            _weapon.DependencyHandler.OnReload += UpdateAmmoInfo;
        }

        /// <summary>
        /// Update the weapon icon on the left of the ammo info in the slot.
        /// </summary>
        public void UpdateWeaponIcon()
        {
            icon.sprite = Resources.Load<Sprite>(_weaponData.SpritePath);
        }
        
        /// <summary>
        /// Update the displayed ammo info, the "ammo in mag | ammo in reserve", in the loadout slot.
        /// </summary>
        public void UpdateAmmoInfo()
        {
            ammoInfo.text = $"{_runtimeWeaponData.AmmoInMag} I {_runtimeWeaponData.AmmoInReserve}";
        }
        
        /// <summary>
        /// Set the hierarchical position of this slot in the loadout.
        /// </summary>
        /// <param name="pos">The target position of this slot in the loadout.</param>
        public void SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos pos)
        {
            HierarchicalPos = pos;
            ChangeRectTransformSize();
        }
        
        private void ChangeRectTransformSize()
        {
            switch (HierarchicalPos)
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