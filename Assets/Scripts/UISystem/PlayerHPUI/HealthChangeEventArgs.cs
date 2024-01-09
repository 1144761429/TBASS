using System;

namespace UISystem
{
    public class HealthChangeEventArgs : EventArgs
    {
        public float HealthBeforeChange { get; private set; }
        public float HealthAfterChange { get; private set; }
        public float MaxHealth { get; private set; }

        public HealthChangeEventArgs(float healthBeforeChange, float healthAfterChange, float maxHealth)
        {
            HealthBeforeChange = healthBeforeChange;
            HealthAfterChange = healthAfterChange;
            MaxHealth = maxHealth;
        }
    }
}