using System;
using UnityEngine;

namespace UISystem
{
    /// <summary>
    /// The C of MVC.
    /// </summary>
    public abstract class UIController : MonoBehaviour
    {
        // TODO: Add events.

        [field: SerializeField] public UIModel Model { get; protected set; }
        [field: SerializeField] public UIVisual Visual { get; protected set; }
        public abstract EUIType Type { get; }

        /// <summary>
        /// If this UI can be closed.
        /// </summary>
        public bool CanClose;

        public bool ShouldCloseAfterInstantiate;
        
        /// <summary>
        /// If this UI can multiple instances in the scene.
        /// </summary>
        public bool CanHaveMultiple;

        public abstract void Open();
        public abstract void Close();
    }
}