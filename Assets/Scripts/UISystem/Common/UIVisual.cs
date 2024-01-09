using UnityEngine;

namespace UISystem
{
    /// <summary>
    /// The V of MVC.
    /// </summary>
    public class UIVisual : MonoBehaviour
    {
        /// <summary>
        /// The GameObject as the parent of all visuals of a UI.
        /// In other words, to show and hide a UI is equivalent to call Visual.SetActive().
        /// </summary>
        [Tooltip(
            "The GameObject as the parent of all visuals of a UI. " +
            "In other words, to show and hide a UI is equivalent to call Visual.SetActive().")]
        [field: SerializeField]
        public GameObject VisualRoot { get; private set; }
        
        public UIController Controller { get; private set; }
    }
}