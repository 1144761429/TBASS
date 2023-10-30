using System;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;

namespace UISystem
{
    public class PanelPlayerLoadout : PanelBase
    {
        //TODO: this property can be deleted by replacing every usage to PlayerLoadout.CurrentLoadoutWeaponType
        public PanelPlayerLoadoutSlot CurrentWeaponSlot { get; private set; } 
        
        [SerializeField] private PanelPlayerLoadoutSlot primarySlot;
        [SerializeField] private PanelPlayerLoadoutSlot secondarySlot;
        [SerializeField] private PanelPlayerLoadoutSlot adeptSlot;
        
        private Loadout _playerLoadout;

        private const int POS_Y_TOP = 0;
        private const int POS_Y_MID = 18;
        private const int POS_Y_BOT = 32;

        private void Awake()
        {
            _playerLoadout = GameObject.FindWithTag("PlayerLoadout").GetComponent<Loadout>();
        }

        public override void Init()
        {
            InitSlots();

            _playerLoadout.OnSwitchWeapon += UpdateVisual;
            _playerLoadout.OnChangeWeapon += (type, _, _) => UpdateSlotWeaponData(type);
            _playerLoadout.OnChangeWeapon += (type, _, _) => UpdateSlotDisplayedInfo(type);
        }

        /// <summary>
        /// Init the slots UI for the loadout.
        /// </summary>
        private void InitSlots()
        {
            primarySlot.Init();
            secondarySlot.Init();
            adeptSlot.Init();
        }
        
        /// <summary>
        /// Update the visual of PanelPlayerLoadout according to which weapon is the CurrentLoadoutWeaponType of player's loadout.
        /// </summary>
        /// <param name="loadoutWeaponType">The CurrentLoadoutWeaponType of player's loadout.</param>
        private void UpdateVisual(ELoadoutWeaponType loadoutWeaponType)
        {
            switch (loadoutWeaponType)
            {
                case ELoadoutWeaponType.Primary:
                    CurrentWeaponSlot = primarySlot;
                    break;
                case ELoadoutWeaponType.Secondary:
                    CurrentWeaponSlot = secondarySlot;
                    break;
                case ELoadoutWeaponType.Adept:
                    CurrentWeaponSlot = adeptSlot;
                    break;
            }

            CurrentWeaponSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Top);

            UpdateSlotsHierarchy();
            UpdateSlotsPosition();
        }

        /// <summary>
        /// Change the hierarchical relation of the three slots according to which weapon is currently at top of the hierarchy.
        /// </summary>
        ///
        /// <example>
        /// If the current weapon is primary,
        /// then the primary slots should appear at the top layer, secondary slot on the middle layer,
        /// and the adept slot on the bottom layer.
        /// </example>
        private void UpdateSlotsHierarchy()
        {
            if (CurrentWeaponSlot == primarySlot)
            {
                secondarySlot.transform.SetAsFirstSibling();
                secondarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                adeptSlot.transform.SetAsFirstSibling();
                adeptSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Bot);
            }
            else if (CurrentWeaponSlot == secondarySlot)
            {
                primarySlot.transform.SetAsFirstSibling();
                primarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                adeptSlot.transform.SetAsFirstSibling();
                adeptSlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
            }
            else if (CurrentWeaponSlot == adeptSlot)
            {
                secondarySlot.transform.SetAsFirstSibling();
                secondarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Mid);
                primarySlot.transform.SetAsFirstSibling();
                primarySlot.SetHierarchicalPos(EPlayerLoadoutSlotHierarchicalPos.Bot);
            }
            else
            {
                Debug.LogError("CurrentWeaponSlot is null.");
            }
        }

        /// <summary>
        /// Change the position of three slots according to their hierarchical relation.
        /// </summary>
        /// <example>
        /// If the current weapon type is primary, since the space between three slots are not the same,
        /// the y position between primary and secondary slots should change, as well as that between the secondary and adept slots.
        /// </example>
        private void UpdateSlotsPosition()
        {
            // Transform primarySlotTransform = _primarySlot.transform;
            // Transform secondarySlotTransform = _secondarySlot.transform;
            // Transform adeptSlotTransform = _adeptSlot.transform;

            if (CurrentWeaponSlot == primarySlot)
            {
                primarySlot.transform.localPosition = new Vector3(primarySlot.transform.localPosition.x, POS_Y_TOP,
                    primarySlot.transform.localPosition.z);
                secondarySlot.transform.localPosition = new Vector3(secondarySlot.transform.localPosition.x,
                    -POS_Y_MID, secondarySlot.transform.localPosition.z);
                adeptSlot.transform.localPosition = new Vector3(adeptSlot.transform.localPosition.x, -POS_Y_BOT,
                    adeptSlot.transform.localPosition.z);
            }

            if (CurrentWeaponSlot == secondarySlot)
            {
                primarySlot.transform.localPosition = new Vector3(primarySlot.transform.localPosition.x, POS_Y_MID,
                    primarySlot.transform.localPosition.z);
                secondarySlot.transform.localPosition = new Vector3(secondarySlot.transform.localPosition.x,
                    POS_Y_TOP, secondarySlot.transform.localPosition.z);
                adeptSlot.transform.localPosition = new Vector3(adeptSlot.transform.localPosition.x, -POS_Y_MID,
                    adeptSlot.transform.localPosition.z);
            }

            if (CurrentWeaponSlot == adeptSlot)
            {
                primarySlot.transform.localPosition = new Vector3(primarySlot.transform.localPosition.x, POS_Y_BOT,
                    primarySlot.transform.localPosition.z);
                secondarySlot.transform.localPosition = new Vector3(secondarySlot.transform.localPosition.x,
                    POS_Y_MID, secondarySlot.transform.localPosition.z);
                adeptSlot.transform.localPosition = new Vector3(adeptSlot.transform.localPosition.x, POS_Y_TOP,
                    adeptSlot.transform.localPosition.z);
            }
        }

        private void UpdateSlotWeaponData(ELoadoutWeaponType type)
        {
            switch (type)
            {
                case ELoadoutWeaponType.Primary:
                    primarySlot.UpdateWeaponData(_playerLoadout.PrimaryWeapon);
                    break;
                case ELoadoutWeaponType.Secondary:
                    secondarySlot.UpdateWeaponData(_playerLoadout.SecondaryWeapon);
                    break;
                case ELoadoutWeaponType.Adept:
                    adeptSlot.UpdateWeaponData(_playerLoadout.AdeptWeapon);
                    break;
            }
        }
        
        /// <summary>
        /// Update the info displayed by a slot.
        /// </summary>
        /// <param name="type">The info of the target slot to be updated</param>
        /// <exception cref="ArgumentException"> If the argument type is out of options of primary, secondary, and adept.
        /// This basically will never happen.</exception>
        private void UpdateSlotDisplayedInfo(ELoadoutWeaponType type)
        {
            PanelPlayerLoadoutSlot targetSlot;

            switch (type)
            {
                case ELoadoutWeaponType.Primary:
                    targetSlot = primarySlot;
                    break;
                case ELoadoutWeaponType.Secondary:
                    targetSlot = secondarySlot;
                    break;
                case ELoadoutWeaponType.Adept:
                    targetSlot = adeptSlot;
                    break;
                default:
                    throw new ArgumentException(
                        "ELoadoutWeaponType is out of options of primary, secondary, and adept.");
            }
            
            targetSlot.UpdateWeaponIcon();
            targetSlot.UpdateAmmoInfo();
        }
        
        
    }
}