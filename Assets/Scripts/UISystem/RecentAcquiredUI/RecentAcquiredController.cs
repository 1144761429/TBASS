using InventorySystem;
using UISystem.InventoryUI;

namespace UISystem
{
    public class RecentAcquiredController : UIController
    {
        public override EUIType Type => EUIType.PanelRecentAcquired;
        
        private RecentAcquiredModel _model => Model as RecentAcquiredModel;
        private RecentAcquiredVisual _visual => Visual as RecentAcquiredVisual;

        private void Awake()
        {
            Model = new RecentAcquiredModel(this, _visual.MaxDisplayAmount);

            ((PlayerInventoryModel)PlayerInventoryController.Instance.Model).ConsumableInventory.OnAddItem +=
                _model.AddItem;

            _model.OnAddItemToDisplayQueue += delegate(object _, WrappedItem item)
            {
                StartCoroutine(_visual.AddSlotToDisplay(item));
            };

            _visual.OnDisplayTimeElapsed += delegate { _model.RemoveItem(); };
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