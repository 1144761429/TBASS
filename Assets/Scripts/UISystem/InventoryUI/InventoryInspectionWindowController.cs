using System;
using InventorySystem;
using InventorySystem.Common.EventArgument;
using UnityEngine;

namespace UISystem.InventoryUI
{
    public class InventoryInspectionWindowController : UIController
    {
        public override EUIType Type => EUIType.Component;

        private InventoryInspectionWindowModel _model => Model as InventoryInspectionWindowModel;
        private InventoryInspectionWindowVisual _visual => Visual as InventoryInspectionWindowVisual;

        private void Awake()
        {
            Model = new InventoryInspectionWindowModel(this);

            _model.OnSetItemToInspect += RegisterButtonEvent;
            _model.OnSetItemToInspect += _visual.UpdateVisual;
        }

        public void SetInspectedItem(WrappedItem item)
        {
            _model.SetInspectedItem(item);
        }

        private void RegisterButtonEvent(WrappedItem item)
        {
            _visual.LeftButton.onClick.RemoveAllListeners();
            _visual.LeftButton.onClick.AddListener(OnClickRemoveButton);
        }

        // TODO: Refactor
        private void OnClickRemoveButton()
        {
            WrappedItem itemToRemove =
                new WrappedItem(_model.CurrentItem.Name, _model.CurrentItem.ID, 1);

            PlayerInventoryController.Instance.RemoveItem(itemToRemove, false);

            // If the item after remove is null, then clear the info in the inspection window.
            if (_model.CurrentItem.Amount == 0)
            {
                _model.Clear();
                _visual.UpdateVisual(_model.CurrentItem);
                _visual.HideButtons();
            }
            else
            {

                _visual.UpdateVisual(_model.CurrentItem);
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