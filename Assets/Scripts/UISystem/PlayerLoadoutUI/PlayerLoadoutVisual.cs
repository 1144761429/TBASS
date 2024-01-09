using System;
using UISystem.InventoryUI;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;

namespace UISystem
{
    public class PlayerLoadoutVisual : UIVisual
    {
        [field: SerializeField] public PlayerLoadoutSlotController PrimarySlotController { get; private set; }
        [field: SerializeField] public PlayerLoadoutSlotController SecondarySlotController { get; private set; }
        [field: SerializeField] public PlayerLoadoutSlotController AdeptSlotController { get; private set; }

        private PlayerLoadoutController _controller => Controller as PlayerLoadoutController;

        public void UpdateSlotsPosAndSize(object sender, SwitchLoadoutSlotEventArgs args)
        {
            PrimarySlotController.UpdatePosAndSize(ELoadoutSlot.Primary, args);
            SecondarySlotController.UpdatePosAndSize(ELoadoutSlot.Secondary, args);
            AdeptSlotController.UpdatePosAndSize(ELoadoutSlot.Adept, args);
        }

        public void UpdateHierarchicalSiblingRelation(object sender, SwitchLoadoutSlotEventArgs args)
        {
            switch (args.Front)
            {
                case ELoadoutSlot.Primary:
                    AdeptSlotController.transform.SetSiblingIndex(0);
                    SecondarySlotController.transform.SetSiblingIndex(1);
                    break;
                case ELoadoutSlot.Secondary:
                    PrimarySlotController.transform.SetSiblingIndex(0);
                    AdeptSlotController.transform.SetSiblingIndex(1);
                    break;
                case ELoadoutSlot.Adept:
                    PrimarySlotController.transform.SetSiblingIndex(0);
                    SecondarySlotController.transform.SetSiblingIndex(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}