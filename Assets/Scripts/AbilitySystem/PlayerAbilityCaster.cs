using System;
using UnityEngine;

namespace AbilitySystem
{
    public class PlayerAbilityCaster : MonoBehaviour, IAbilityCaster
    {
        
        [field: SerializeField ]public bool CannotCast { get; set; }
        public Ability[] Abilities{ get; private set; }
        public Transform CasterTransform => transform;
        
        private void Awake()
        {
            Abilities = new Ability[2];
            SetAbilityQ(new AbilitySentryGun(this,5));
        }

        private void Update()
        {
            if (PlayerInputHandler.IsAbilityQPressedThisFrame && !CannotCast)
            {
                Abilities[0].Cast();
            }
        }

        private void SetAbilityQ(Ability ability)
        {
            Abilities[0] = ability;
        }
    }
}