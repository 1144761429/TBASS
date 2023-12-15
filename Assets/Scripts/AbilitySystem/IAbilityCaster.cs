using UnityEngine;

namespace AbilitySystem
{
    public interface IAbilityCaster
    {
        public Transform CasterTransform { get; }
        public bool CannotCast { get; set; }
        public Ability[] Abilities { get; }
    }
}