using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class DamageablePriorityComparer : Comparer<IDamageable>
    {
        public override int Compare(IDamageable x, IDamageable y)
        {
            if (x == null)
            {
                throw new NullReferenceException("The left-hand side argument is null.");
            }
            
            if (y == null)
            {
                throw new NullReferenceException("The right-hand side argument is null.");
            }
            
            if (x.TargetPriority >= y.TargetPriority)
            {
                return 1;
            }

            return -1;
        }
    }
}