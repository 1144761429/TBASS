using System;
using System.Collections.Generic;
using BuffSystem.Common;

namespace UISystem
{
    public class AddStatusEventArgs : EventArgs
    {
        public Buff StatusToAdd { get; private set; }

        /// <summary>
        /// Status to display after the addition is complete.
        /// </summary>
        public List<Buff> StatusToDisplay { get; private set; }


        /// <param name="statusToAdd"></param>
        /// <param name="statusToDisplay">Status to display after the addition is complete.</param>
        public AddStatusEventArgs(Buff statusToAdd, List<Buff> statusToDisplay)
        {
            StatusToAdd = statusToAdd;
            StatusToDisplay = statusToDisplay;
        }
    }
}