using System;
using System.Collections.Generic;
using BuffSystem.Common;

namespace UISystem
{
    public class RemoveStatusEventArgs : EventArgs
    {
        public Buff StatusToRemove { get; private set; }

        /// <summary>
        /// Status to display after the removal is complete.
        /// </summary>
        public List<Buff> StatusToDisplay { get; private set; }


        /// <param name="statusToRemove"></param>
        /// <param name="statusToDisplay">Status to display after the removal is complete.</param>
        public RemoveStatusEventArgs(Buff statusToRemove, List<Buff> statusToDisplay)
        {
            StatusToRemove = statusToRemove;
            StatusToDisplay = statusToDisplay;
        }
    }
}