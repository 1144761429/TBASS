using System;
using UISystem.InventoryUI;
using UnityEngine;
using WeaponSystem;

namespace UISystem
{
    public class PlayerLoadoutSlotController : UIController
    {
        public override EUIType Type => EUIType.Component;

        public ELoadoutSlot LoadoutSlotEnum => _visual.LoadoutSlotEnum;
        
        private PlayerLoadoutController _parentController;
        private Loadout _loadout;
        private PlayerLoadoutSlotVisual _visual => Visual as PlayerLoadoutSlotVisual;
        private PlayerLoadoutSlotModel _model => Model as PlayerLoadoutSlotModel;
        
        private void Awake()
        {
            _parentController = GetComponentInParent<PlayerLoadoutController>();
            _loadout = GameObject.FindWithTag("PlayerLoadout").GetComponent<Loadout>();
            if (_loadout == null)
            {
                Debug.LogError(
                    "Either cannot find a GameObject tagged with PlayerLoadout or the GameObject has no Loadout script attached on it.");
            }
            
            Visual = GetComponent<PlayerLoadoutSlotVisual>();
            
            switch (_visual.LoadoutSlotEnum)
            {
                case ELoadoutSlot.Primary:
                    Model = new PlayerLoadoutSlotModel(this, null);
                    break;
                case ELoadoutSlot.Secondary:
                    Model = new PlayerLoadoutSlotModel(this, null);
                    break;
                case ELoadoutSlot.Adept:
                    Model = new PlayerLoadoutSlotModel(this, null);
                    break;
            }
            
            _model.OnUpdateData += _visual.UpdateWeaponAmmoInfo;
            _model.OnUpdateData += _visual.UpdateWeaponIcon;
            _model.OnUpdateData += RegisterWeaponEvent;
        }
        
        public void UpdatePosAndSize(object sender, SwitchLoadoutSlotEventArgs args)
        {
            _visual.UpdatePosAndSize(args.SlotSwitchTo._visual.LoadoutSlotEnum);
        }
        
        public void ChangeStoredWeapon(Weapon weapon)
        {
            _model.UpdateData(weapon);
        }
        
        public override void Open()
        {
            throw new System.NotImplementedException();
        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        private void RegisterWeaponEvent(Weapon weapon)
        {
            weapon.RuntimeData.OnReduceAmmo += () => _visual.UpdateWeaponAmmoInfo(weapon);
            weapon.DependencyHandler.OnReload += () => _visual.UpdateWeaponAmmoInfo(weapon);
        }
    }
}