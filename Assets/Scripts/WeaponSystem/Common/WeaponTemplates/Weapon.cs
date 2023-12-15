using System.Collections.Generic;
using UnityEngine;
using WeaponSystem.Modification;

namespace WeaponSystem
{
    /// <summary>
    /// A class that represents a weapon.
    /// </summary>
    ///
    /// <example>
    /// A gun, a sword, a shield, etc.
    /// </example>
    public abstract class Weapon : MonoBehaviour
    {
        /// <summary>
        /// The ID of a weapon in the database.
        /// </summary>
        public int ID => StaticData.ID;

        public ELoadoutWeaponType LoadoutWeaponType { get; set; }

        /// <summary>
        /// The gameObject this weapon belongs to.
        /// </summary>
        [field: SerializeField]
        public GameObject
            Wielder { get; private set; } //TODO: make this member variable get its value automatically in Awake()

        /// <summary>
        /// The loadout this weapon belongs to.
        /// </summary>
        public Loadout Loadout { get; set; }

        /// <summary>
        /// The static data of this weapon
        /// </summary>
        public ItemDataEquipmentWeapon StaticData { get; set; }

        /// <summary>
        /// The runtime / dynamic data of this weapon. This data changes as the game is running.
        /// </summary>
        public RuntimeItemDataEquipmentWeapon RuntimeData { get;  set; }

        /// <summary>
        /// A transform of where instantiated bullet instance should be stored.
        /// </summary>
        [field: SerializeField]
        public Transform BulletParent { get; private set; } //TODO: change to damaging entity parent

        /// <summary>
        /// An offset representing where the bullet should spawn when shooting in local space of the weapon.
        /// </summary>
        public Transform BulletSpawnPos { get; private set; } //TODO: change to damaging entity spawn

        // /// <summary>
        // /// A list that contains all the modules a weapon have.
        // /// </summary>
        // public Dictionary<EWeaponModule, WeaponModule> Modules { get; private set; }

        public WeaponEventBundle Events { get; private set; }
        public ModuleDependencyHandler DependencyHandler { get; protected set; }
        public Dictionary<EWeaponModule, WeaponModule> Modules { get; protected set; }
        public Dictionary<EWeaponModificationType, WeaponModification> Modifications { get; protected set; }

        /// <summary>
        /// True if the weapon has loaded all the module and initialized them. Otherwise, false.
        /// </summary>
        private bool _initialized;
        
        protected virtual void InternalInit()
        {
            Modules = new Dictionary<EWeaponModule, WeaponModule>();
            Modifications = new Dictionary<EWeaponModificationType, WeaponModification>();
            
            foreach (var modification in GetComponents<WeaponModification>())
            {
                Modifications.Add(modification.ModificationType, modification);
            }
            
            BulletSpawnPos = GameObject.FindWithTag("PlayerBulletSpawn").transform;
            Events = new WeaponEventBundle();
        }

        /// <summary>
        /// Initialize modules, modifications, event bundle, and assign the BulletSpawnPos GameObject.
        /// </summary>
        public void Init()
        {
            InternalInit();

            foreach (var modification in Modifications.Values)
            {
                modification.Init(this,DependencyHandler);
            }
        }

        /// <summary>
        /// <para>
        /// 1. Set the bullet spawn position to where it supposed to be according to the static data.
        /// </para>
        /// </summary>
        public void Setup()
        {
            BulletSpawnPos.localPosition =
                new Vector2(StaticData.BulletSpawnOffset[0], StaticData.BulletSpawnOffset[1]);

            // Loadout.CurrenWeaponAnimator.runtimeAnimatorController =
            //     Resources.Load<AnimatorOverrideController>(StaticData.AnimatorOverrideControllerPath);
        }
    }
}
