using System;

namespace UISystem
{
    public class CollecetItemEventArgs : EventArgs
    {
        public WrappedItem Item { get; private set; }

        public CollecetItemEventArgs(WrappedItem item)
        {
            Item = item;
        }
    }
}