using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponModule : MonoBehaviour
    {
        protected Weapon _weapon;
        protected ItemDataEquipmentWeapon _data;
        protected RuntimeItemDataEquipmentWeapon _runtimeData;
        //protected int InitializePriority;

        protected virtual void Awake()
        {
            _weapon = GetComponent<Weapon>();
            _data = _weapon.Data;
            _runtimeData = _weapon.RuntimeData;
        }

        /// <summary>
        /// Initialize the module.
        /// </summary>
        public virtual void Init()
        {
        }
    }
}