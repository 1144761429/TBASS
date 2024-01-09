using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public interface IAbilityCaster
    {
        public Transform CasterTransform { get; }
        public bool CannotCast { get; set; }
        public List<Ability> Abilities { get; }
    }
}