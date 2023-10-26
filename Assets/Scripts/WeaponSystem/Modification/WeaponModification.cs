using UnityEngine;

namespace WeaponSystem.Modification
{
    public abstract class WeaponModification : MonoBehaviour
    {
        public abstract EWeaponModificationType ModificationType { get; }
        
        protected Weapon Weapon;
        protected ModuleDependencyHandler DependencyHandler;

        public virtual void Init(Weapon weapon, ModuleDependencyHandler dependencyHandler)
        {
            Weapon = weapon;
            DependencyHandler = dependencyHandler;
        }
    }
}