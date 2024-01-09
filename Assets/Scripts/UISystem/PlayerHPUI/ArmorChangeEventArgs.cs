using System;

namespace UISystem
{
    public class ArmorChangeEventArgs : EventArgs
    {
        public float ArmorBeforeChange { get; private set; }
        public float ArmorAfterChange { get; private set; }
        public float MaxArmor { get; private set; }

        public ArmorChangeEventArgs(float armorBeforeChange, float armorAfterChange, float maxArmor)
        {
            ArmorBeforeChange = armorBeforeChange;
            ArmorAfterChange = armorAfterChange;
            MaxArmor = maxArmor;
        }
    }
}