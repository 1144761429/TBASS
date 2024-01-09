using System;
using System.ComponentModel;
using UISystem.InventoryUI;
using UnityEngine;
using WeaponSystem;

namespace UISystem
{
    public class PlayerLoadoutController : UIController
    {
        public override EUIType Type => EUIType.PanelPlayerLoadout;

        private PlayerLoadoutVisual _visual => (PlayerLoadoutVisual)Visual;
        private PlayerLoadoutModel _model => (PlayerLoadoutModel)Model;

        private Loadout _loadout;
        
        private void Awake()
        {
            _loadout = GameObject.FindWithTag("PlayerLoadout").GetComponent<Loadout>();
            _loadout.OnSwitchWeapon += SetCurrentSlot;
            _loadout.OnChangeWeapon += ChangeStoredWeapon;
            
            // TODO: Redundant?
            Model = new PlayerLoadoutModel(this,
                _visual.PrimarySlotController.Model as PlayerLoadoutSlotModel, 
                _visual.SecondarySlotController.Model as PlayerLoadoutSlotModel, 
                _visual.AdeptSlotController.Model as PlayerLoadoutSlotModel,
                GameObject.FindWithTag("PlayerLoadout").GetComponent<Loadout>());

            _model.OnSetCurrentSlot += _visual.UpdateSlotsPosAndSize;
            _model.OnSetCurrentSlot += _visual.UpdateHierarchicalSiblingRelation;
        }

        public void SetCurrentSlot(ELoadoutSlot slot)
        {
            switch (slot)
            {
                case ELoadoutSlot.Primary:
                    _model.SetCurrentSlot(_visual.PrimarySlotController);
                    break;
                case ELoadoutSlot.Secondary:
                    _model.SetCurrentSlot(_visual.SecondarySlotController);
                    break;
                case ELoadoutSlot.Adept:
                    _model.SetCurrentSlot(_visual.AdeptSlotController);
                    break;
                default:
                    throw new InvalidEnumArgumentException("There is an unidentified ELoadoutSlot enum.");
            }
        }
        
        public void ChangeStoredWeapon(ChangeWeaponEventArgs args)
        {
            switch (args.LoadoutSlot)
            {
                case ELoadoutSlot.Primary:
                    _visual.PrimarySlotController.ChangeStoredWeapon(args.To);
                    break;
                case ELoadoutSlot.Secondary:
                    _visual.SecondarySlotController.ChangeStoredWeapon(args.To);
                    break;
                case ELoadoutSlot.Adept:
                    _visual.AdeptSlotController.ChangeStoredWeapon(args.To);
                    break;
            }
        }
        
        public override void Open()
        {
            Visual.VisualRoot.SetActive(true);
        }

        public override void Close()
        {
            Visual.VisualRoot.SetActive(false);
        }
    }
}