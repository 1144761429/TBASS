using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    public abstract class WeaponModule
    {
        public abstract EWeaponModule ModuleType { get; }
        protected Weapon _weapon;
        protected ItemDataEquipmentWeapon _staticData;
        protected RuntimeItemDataEquipmentWeapon _runtimeData;
        protected ModuleDependencyHandler _dependencyHandler => _weapon.DependencyHandler;

        protected MonoBehaviour _mono;
        
        public WeaponModule(MonoBehaviour mono,Weapon weapon,ItemDataEquipmentWeapon staticData,RuntimeItemDataEquipmentWeapon runtimeData)
        {
            _mono = mono;
            _weapon = weapon;
            _staticData = staticData;
            _runtimeData = runtimeData;
        }

        /// <summary>
        /// Initialize the module. This is called when ALL of the modules of a weapon have been added to the GameObject.
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// This method is called every frame on a weapon.
        /// This simulates the Update() method of monobehavior.
        /// </summary>
        public virtual void ModuleUpdate()
        {
            
        }
    }
}