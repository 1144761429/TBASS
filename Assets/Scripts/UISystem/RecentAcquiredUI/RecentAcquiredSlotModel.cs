using System;

namespace UISystem
{
    public class RecentAcquiredSlotModel : UIModel
    {
        public event EventHandler<CollecetItemEventArgs> OnSetStoredItem;
        
        public WrappedItem StoredItem { get; private set; }
        
        public RecentAcquiredSlotModel(UIController controller) : base(EUIType.Component, controller)
        {
        }

        public void SetStoredItem(WrappedItem item)
        {
            StoredItem = item;
            CollecetItemEventArgs args = new CollecetItemEventArgs(item);
            OnSetStoredItem?.Invoke(this,args);
        }

        public void Clear()
        {
            StoredItem = null;
        }
    }
}