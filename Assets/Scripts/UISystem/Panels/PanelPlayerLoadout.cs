using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace UISystem
{
    public class PanelPlayerLoadout : PanelBase
    {
        public PanelPlayerLoadoutSlot CurrentWeaponSlot { get; private set; }
        [SerializeField] private PanelPlayerLoadoutSlot _primarySlot;
        [SerializeField] private PanelPlayerLoadoutSlot _secondarySlot;
        [SerializeField] private PanelPlayerLoadoutSlot _adeptSlot;
        [field: SerializeField] public Loadout PlayerLoadout { get; private set; }

        private const int POS_Y_TOP = 0;
        private const int POS_Y_MID = 18;
        private const int POS_Y_BOT = 32;

        private void Start()
        {
            CurrentWeaponSlot = _primarySlot;
            UpdateSlotsHierarchy();
            UpdateSlotsPosition();
        }

        public override void Init()
        {
            InitSlots();
            PlayerLoadout.OnSwitchWeapon += UpdateCurrentSlot;
        }

        /// <summary>
        /// Setup the necessary part for this script to execute properly
        /// </summary>
        private void InitSlots()
        {
            // Set the Loadout reference
            PlayerLoadout = GameObject.Find("Player Loadout").GetComponent<Loadout>();

            // Set the Weapon reference to each of the slots
            _primarySlot.SetWeapon(GameObject.Find("Primary Weapon").GetComponent<Weapon>());
            _secondarySlot.SetWeapon(GameObject.Find("Secondary Weapon").GetComponent<Weapon>());
            _adeptSlot.SetWeapon(GameObject.Find("Adept Weapon").GetComponent<Weapon>());

            _primarySlot.Init();
            _secondarySlot.Init();
            _adeptSlot.Init();
        }

        private void UpdateCurrentSlot()
        {
            switch (PlayerLoadout.CurrentWeaponEnum)
            {
                case ELoadoutWeaponType.Primary:
                    CurrentWeaponSlot = _primarySlot;
                    break;
                case ELoadoutWeaponType.Secondary:
                    CurrentWeaponSlot = _secondarySlot;
                    break;
                case ELoadoutWeaponType.Adept:
                    CurrentWeaponSlot = _adeptSlot;
                    break;
            }

            CurrentWeaponSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Top);
            UpdateSlotsHierarchy();
            UpdateSlotsPosition();
        }

        private void UpdateSlotsHierarchy()
        {
            if (CurrentWeaponSlot == _primarySlot)
            {
                _secondarySlot.transform.SetAsFirstSibling();
                _secondarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                _adeptSlot.transform.SetAsFirstSibling();
                _adeptSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Bot);
            }
            else if (CurrentWeaponSlot == _secondarySlot)
            {
                _primarySlot.transform.SetAsFirstSibling();
                _primarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                _adeptSlot.transform.SetAsFirstSibling();
                _adeptSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
            }
            else if (CurrentWeaponSlot == _adeptSlot)
            {
                _secondarySlot.transform.SetAsFirstSibling();
                _secondarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                _primarySlot.transform.SetAsFirstSibling();
                _primarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Bot);
            }
            else
            {
                Debug.LogError("CurrentWeaponSlot is null.");
            }
        }

        private void UpdateSlotsPosition()
        {
            if (CurrentWeaponSlot == _primarySlot)
            {
                _primarySlot.transform.localPosition = new Vector3(_primarySlot.transform.localPosition.x, POS_Y_TOP,
                    _primarySlot.transform.localPosition.z);
                _secondarySlot.transform.localPosition = new Vector3(_secondarySlot.transform.localPosition.x,
                    -POS_Y_MID, _secondarySlot.transform.localPosition.z);
                _adeptSlot.transform.localPosition = new Vector3(_adeptSlot.transform.localPosition.x, -POS_Y_BOT,
                    _adeptSlot.transform.localPosition.z);
            }

            if (CurrentWeaponSlot == _secondarySlot)
            {
                _primarySlot.transform.localPosition = new Vector3(_primarySlot.transform.localPosition.x, POS_Y_MID,
                    _primarySlot.transform.localPosition.z);
                _secondarySlot.transform.localPosition = new Vector3(_secondarySlot.transform.localPosition.x,
                    POS_Y_TOP, _secondarySlot.transform.localPosition.z);
                _adeptSlot.transform.localPosition = new Vector3(_adeptSlot.transform.localPosition.x, -POS_Y_MID,
                    _adeptSlot.transform.localPosition.z);
            }

            if (CurrentWeaponSlot == _adeptSlot)
            {
                _primarySlot.transform.localPosition = new Vector3(_primarySlot.transform.localPosition.x, POS_Y_BOT,
                    _primarySlot.transform.localPosition.z);
                _secondarySlot.transform.localPosition = new Vector3(_secondarySlot.transform.localPosition.x,
                    POS_Y_MID, _secondarySlot.transform.localPosition.z);
                _adeptSlot.transform.localPosition = new Vector3(_adeptSlot.transform.localPosition.x, POS_Y_TOP,
                    _adeptSlot.transform.localPosition.z);
            }
        }
    }
}