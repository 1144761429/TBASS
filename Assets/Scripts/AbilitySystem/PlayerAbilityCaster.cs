using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public enum EAbilitySlot
    {
        Q,
        E
    }
    
    public class PlayerAbilityCaster : MonoBehaviour, IAbilityCaster
    {
        [field: SerializeField] public bool CannotCast { get; set; }
        
        [field: SerializeField] public EAbilityName AbilityQEnum { get; private set; }
        [field: SerializeField] public EAbilityName AbilityEEnum { get; private set; }
        public Ability AbilityQ { get; private set; }
        public Ability AbilityE { get; private set; }
        public List<Ability> Abilities { get; private set; }
        
        public Transform CasterTransform => transform;

        private void Awake()
        {
            Abilities = new List<Ability>();
        }

        private void Start()
        {
            Abilities.Add(AbilityQ);
            Abilities.Add(AbilityE);

            UpdateAbilitySlot(EAbilitySlot.Q);
            UpdateAbilitySlot(EAbilitySlot.E);
        }

        private void Update()
        {
            if (PlayerInputHandler.IsAbilityQPressedThisFrame && !CannotCast)
            {
                if (AbilityQ.Cast())
                {
                    Debug.Log("Ability Q casted");
                }
            }
        }

        private void UpdateAbilitySlot(EAbilitySlot abilitySlot)
        {
            switch (abilitySlot)
            {
                case EAbilitySlot.Q:
                    InitAbility(EAbilitySlot.Q, AbilityQEnum);
                    break;
                case EAbilitySlot.E:
                    InitAbility(EAbilitySlot.E, AbilityEEnum);
                    break;
            }
        }
        
        private void InitAbility(EAbilitySlot abilitySlot, EAbilityName abilityName)
        {
            switch (abilityName)
            {
                case EAbilityName.SentryGun:
                    
                    //AbilitySentryGun abilitySentryGun = (AbilitySentryGun)ScriptableObject.CreateInstance(typeof(AbilitySentryGun));
                    AbilitySentryGun abilitySentryGun = Instantiate(AbilityCache.Instance.SentryGun);
                    abilitySentryGun.Init(this);
                    
                    switch (abilitySlot)
                    {
                        case EAbilitySlot.Q:
                            AbilityQ = abilitySentryGun;
                            break;
                        case EAbilitySlot.E:
                            AbilityE = abilitySentryGun;
                            break;
                    }
                    break;
            }
        }
        
        // private void SetAbilityQ(Ability ability)
        // {
        //     Abilities[0] = ability;
        // }
    }
}