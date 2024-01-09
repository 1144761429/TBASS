using System;
using InventorySystem.Common;
using InventorySystem.Common.EventArgument;
using UISystem;
using UISystem.InventoryUI;
using UnityEngine;

namespace InventorySystem
{
    /// <summary>
    /// C of MVC.
    /// </summary>
    [DefaultExecutionOrder(-1)] 
    /*
     * This script will be executed at -1 time because in Awake(), it needs to declare a new inventory slot.
     * If the InventoryController get executed at the same time,
     * then when the InventoryController trys to get the slot of this controller, a null will be retrieved.
     */
    public class InventorySlotController : UIController
    {
        public override EUIType Type => EUIType.Component;
        
        /// <summary>
        /// The inventory controller that controls this controller.
        /// </summary>
        private PlayerInventoryController _inventoryController; // TODO: generalize this to InventoryController

        private InventorySlotModel _model => Model as InventorySlotModel;
        private InventorySlotVisual _visual => Visual as InventorySlotVisual;
        
        private void Awake()
        {
            Model = new InventorySlotModel(this);
            Visual = GetComponent<InventorySlotVisual>();
            ((InventorySlotVisual)Visual).Button.interactable = false;

            _inventoryController = GetComponentInParent<PlayerInventoryController>();
            
            _model.OnStoreItem += _visual.UpdateVisual;
            _model.OnStoreItem += EnableButtonInteractivity;
            
            _model.OnRemoveItem += (args)=>
            {
                if(args.ItemAfterRemoval == null)
                {
                    DisableButtonInteractivity(args);
                }
            };
            _model.OnRemoveItem += _visual.UpdateVisual;
        }

        /// <summary>
        /// Update the visual of an inventory slot after adding item to it.
        /// </summary>
        public void UpdateVisual(AddItemToInventoryEventArgs args)
        {
            if (_visual == null)
            {
                throw new Exception("Cannot cast");
            }
            
            _visual.UpdateVisual(args);
        }
        
        /// <summary>
        /// Update the visual of an inventory slot after removing item from it.
        /// </summary>
        public void UpdateVisual(RemoveItemFromInventoryEventArgs args)
        {
            _visual.UpdateVisual(args);
        }

        // TODO: this method should not be here, but in other class
        public void SetDisplayItem()
        {
            PlayerInventoryController.Instance.InspectionWindowController.SetInspectedItem(_model.StoredItem);
        }

        private void EnableButtonInteractivity(AddItemToInventoryEventArgs args)
        {
            _visual.Button.interactable = true;
        }
        
        private void DisableButtonInteractivity(RemoveItemFromInventoryEventArgs args)
        {
            _visual.Button.interactable = false;
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }
    }
}