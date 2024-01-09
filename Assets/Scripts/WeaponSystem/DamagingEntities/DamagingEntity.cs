using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace WeaponSystem.DamagingEntities
{
    public abstract class DamagingEntity : MonoBehaviour
    {
        /// <summary>
        /// The GameObject that this <c>DamagingEntity</c> belongs to.
        /// </summary>
        public GameObject Parent { get; protected set; }
        public Rigidbody2D Rb { get; protected set; }
        public Collider2D Collider { get; protected set; }
        //[field: SerializeField] public LayerMask CollidableLayer { get; private set; }

        public void Init(GameObject parent)
        {
            Parent = parent;
            
            Rb = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }
    }
}
