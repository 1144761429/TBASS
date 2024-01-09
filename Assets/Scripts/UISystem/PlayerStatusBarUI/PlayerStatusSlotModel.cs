using System;
using BuffSystem.Common;

namespace UISystem
{
    public class PlayerStatusSlotModel : UIModel
    {
        public event Action<Buff> OnUpdateData;

        /// <summary>
        /// The buff being stored in the buff slot.
        /// </summary>
        public Buff Status { get; private set; }

        public PlayerStatusSlotModel(PlayerStatusSlotController controller) : base(EUIType.Component,
            controller)
        {
        }

        public void UpdateData(Buff status)
        {
            Status = status;

            OnUpdateData?.Invoke(status);
        }
    }
}