using System;

namespace UISystem
{
    public class RecentAcquiredSlotController : UIController
    {
        public override EUIType Type => EUIType.Component;
        
        private RecentAcquiredSlotModel _model => Model as RecentAcquiredSlotModel;
        private RecentAcquiredSlotVisual _visual => Visual as RecentAcquiredSlotVisual;

        private void Awake()
        {
            _model.OnSetStoredItem += _visual.UpdateVisual;
        }

        public void SetItem(WrappedItem item)
        {
            _model.SetStoredItem(item);
        }

        public void Clear()
        {
            _model.Clear();
        }
        
        public override void Open()
        {
            throw new System.NotImplementedException();
        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }
    }
}