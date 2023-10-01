using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem
{
    public class PanelBase : MonoBehaviour
    {
        public Action OnInstantiatePanel;
        public Action OnOpenPanel;
        public Action OnClosePanel;
        public Action OnRemovePanelFromScene;

        [field: SerializeField] public EPanelType Type { get; private set; }
        [field: SerializeField] public string ID { get; private set; }

        [field: SerializeField] public bool CanClose { get; private set; }
        [field: SerializeField] public bool CanHaveMultiple { get; private set; }

        /// <summary>
        /// Initialize any required component or variable.
        /// This is the first method to be called when open a panel
        /// </summary>
        public virtual void Init()
        {
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Close a panel if possible.
        /// </summary>
        /// <returns>True if the panel is successfully closed. False if the panel failed to close.</returns>
        public virtual bool Close()
        {
            if (!CanClose)
            {
                return false;
            }

            gameObject.SetActive(false);
            return true;
        }

        public virtual void Remove()
        {
            Destroy(this);
        }
    }
}