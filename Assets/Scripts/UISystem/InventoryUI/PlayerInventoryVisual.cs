using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UISystem.InventoryUI
{
    public class PlayerInventoryVisual : UIVisual
    {
        [field: SerializeField] public Button EquipmentTabButton { get; private set; }
        [field: SerializeField] public Button MaterialTabButton { get; private set; }
        [field: SerializeField] public Button ConsumableTabButton { get; private set; }

        [field: SerializeField] public GameObject EquipmentTabPanel { get; private set; }
        [field: SerializeField] public GameObject MaterialTabPanel { get; private set; }
        [field: SerializeField] public GameObject ConsumableTabPanel { get; private set; }

        [field: SerializeField] public List<InventorySlotVisual> EquipmentSlotPanels { get; private set; }
        [field: SerializeField] public List<InventorySlotVisual> MaterialTabSlotPanels { get; private set; }
        [field: SerializeField] public List<InventorySlotVisual> ConsumableTabSlotPanels { get; private set; }

        public InventoryInspectionWindowVisual inspectionWindowVisual;

        public void UpdateVisual()
        {

        }

        public void Show()
        {
            VisualRoot.SetActive(true);
        }

        public void Hide()
        {
            VisualRoot.SetActive(false);
        }

        public void Switch()
        {
            if (VisualRoot.activeInHierarchy)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}