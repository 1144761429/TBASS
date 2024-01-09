namespace InventorySystem.Common.EventArgument
{
    // TODO: rename this to click inventory slot???
    public class SetItemToInspectEventArgs
    {
        /// <summary>
        /// The item to inspect.
        /// </summary>
        public WrappedItem ItemToInspect { get; private set; }

        public SetItemToInspectEventArgs(WrappedItem itemToInspect)
        {
            ItemToInspect = itemToInspect;
        }
    }
}