using System;
using UnityEngine;
using UnityTimer;

namespace AbilitySystem
{
    public enum EAbilityName
    {
        None = 0,
        SentryGun = 1
    }

    public abstract class Ability : ScriptableObject
    {
        public event Func<bool> CastRestrictions;
        public event Action BeforeCast;
        public event Action OnCast;
        public event Action AfterCast;
        
        public IAbilityCaster Caster { get; set; }

        public float Cooldown { get; protected set; }
        public bool CooldownPassed { get; protected set; }
        public Timer CooldownTimer{ get; protected set; }

        /// <summary>
        /// A method that initialize the ability.
        /// </summary>
        /// <param name="caster">The caster of this ability.</param>
        public void Init(IAbilityCaster caster)
        {
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