using System;
using UnityEngine;
using UnityTimer;

namespace AbilitySystem
{
    public abstract class Ability
    {
        public event Func<bool> CastRestrictions;
        public event Action BeforeCast;
        public event Action OnCast;
        public event Action AfterCast;
        
        public IAbilityCaster Caster { get; set; }

        public float Cooldown { get; protected set; }
        public bool CooldownPassed { get; protected set; }
        public Timer CooldownTimer{ get; protected set; }

        protected Ability(IAbilityCaster caster)
        {
            Debug.Log("CTOR called");
            Caster = caster;
            
            CooldownPassed = true;
            
            CastRestrictions += () => CooldownPassed;
        }
        
        public bool Cast()
        {
            if (!FuncBoolUtil.Evaluate(CastRestrictions))
            {
                return false;
            }
            
            CooldownPassed = false;
            CooldownTimer = Timer.Register(Cooldown, () =>
            {
                CooldownPassed = true;
            });
            
            BeforeCast?.Invoke();
            OnCast?.Invoke();
            AfterCast?.Invoke();

            return true;
        }
    }
}