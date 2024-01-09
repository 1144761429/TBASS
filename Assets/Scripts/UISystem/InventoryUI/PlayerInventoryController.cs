using System.Collections.Generic;
using InventorySystem;
using InventorySystem.Common;
using UnityEngine;

namespace UISystem.InventoryUI
{
    /// <summary>
    /// The C of MVC of player inventory.
    /// </summary>
    public class PlayerInventoryController : UIController
    {
        public static PlayerInventoryController Instance { get; private set; }

        public override EUIType Type => EUIType.PanelPlayerInventory;

        /// <summary>
        /// The visual of Player inventory.
        /// </summary>
        [field: SerializeField] public List<InventorySlotController> EquipmentSlotControllers { get; private set; }
        [field: SerializeField] public List<InventorySlotController> MaterialSlotsControllers { get; private set; }
        [field: SerializeField] public List<InventorySlotController> ConsumablesSlotControllers { get; private set; }
        [field: SerializeField] public InventoryInspectionWindowController InspectionWindowController { get; private set; }

        private PlayerInventoryModel _model => Model as PlayerInventoryModel;
        private PlayerInventoryVisual _visual => Visual as PlayerInventoryVisual;

        private void Awake()
        {
            // Initialize singleton.
            
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
            
            // Initialize model of inventory.
            
            List<InventorySlotModel> equipmentSlots = new List<InventorySlotModel>();
            foreach (var controller in EquipmentSlotControllers)
            {
                equipmentSlots.Add(controller.Model as InventorySlotModel);
            }
            
            List<InventorySlotModel> materialSlots = new List<InventorySlotModel>();
            foreach (var controller in MaterialSlotsControllers)
            {
                materialSlots.Add(controller.Model as InventorySlotModel);
            }
            
            List<InventorySlotModel> consumableSlots= new List<InventorySlotModel>();
            foreach (var controller in ConsumablesSlotControllers)
            {
                consumableSlots.Add(controller.Model as InventorySlotModel);
            }

            Model = new PlayerInventoryModel(this, equipmentSlots, materialSlots, consumableSlots);

            if (CanClose && ShouldCloseAfterInstantiate)
            {
                Close();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _visual.Switch();
            }
        }

        public bool AddItem(WrappedItem item)
        {
            return _model.AddItem(item);
        }

        public void RemoveItem(WrappedItem item, bool removeAll)
        {
            _model.RemoveItem(item, removeAll);
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