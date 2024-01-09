using System;
using UISystem.InventoryUI;
using WeaponSystem;

namespace UISystem
{
    public class PlayerLoadoutModel : UIModel
    {
        public event EventHandler<SwitchLoadoutSlotEventArgs> OnSetCurrentSlot; 
        
        public Loadout PlayerLoadout { get; private set; }

        public PlayerLoadoutSlotController CurrentSlot { get; private set; }
        
        public PlayerLoadoutSlotModel PrimarySlotModel { get; private set; }
        public PlayerLoadoutSlotModel SecondarySlotModel { get; private set; }
        public PlayerLoadoutSlotModel AdeptSlotModel { get; private set; }

        public PlayerLoadoutModel(
            PlayerLoadoutController controller,
            PlayerLoadoutSlotModel primarySlotModel,
            PlayerLoadoutSlotModel secondarySlotModel,
            PlayerLoadoutSlotModel adeptSlotModel,
            Loadout playerLoadout) : base(EUIType.PanelPlayerLoadout, controller)
        {
            PlayerLoadout = playerLoadout;

            PrimarySlotModel = primarySlotModel;
            SecondarySlotModel = secondarySlotModel;
            AdeptSlotModel = adeptSlotModel;
        }

        public void SetCurrentSlot(PlayerLoadoutSlotController current)
        {
            if (CurrentSlot == current)
            {
                return;
            }

            CurrentSlot = current;

            SwitchLoadoutSlotEventArgs args = new SwitchLoadoutSlotEventArgs(CurrentSlot, CurrentSlot.LoadoutSlotEnum);
            OnSetCurrentSlot?.Invoke(this, args);
        }
    }
}