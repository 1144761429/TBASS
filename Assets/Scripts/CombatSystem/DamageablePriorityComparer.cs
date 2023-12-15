using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class DamageablePriorityComparer:Comparer<GameObject>
    {
        public override int Compare(GameObject x, GameObject y)
        {
            try
            {
                IDamageable d1 = x.GetComponent<IDamageable>();
                IDamageable d2 = y.GetComponent<IDamageable>();
                
                if (d1.Priority >= d2.Priority)
                {
                    return 1;
                }

                return -1;
            }
            catch (NullReferenceException e)
            {
                Debug.Log($"{x.name} or {y.name} has no script derived from IDamageable attached on it.");
                throw;
            }
        }
    }
}