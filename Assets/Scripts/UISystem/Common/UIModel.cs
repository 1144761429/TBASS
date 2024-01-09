using UnityEngine;

namespace UISystem
{
    /// <summary>
    /// The M of MVC.
    /// </summary>
    public abstract class UIModel
    {
        /// <summary>
        /// The enum that represents what is this UIModel for.
        /// </summary>
        /// <example>
        /// For player's inventory, this enum would be EUIType.PlayerInventory.
        /// </example>
        public EUIType Type { get; private set; }
        
        /// <summary>
        /// The ID that distinguishes UI models among the same EUIType but different GameObjects in the game.
        /// If the ID is zero
        /// </summary>
        /// <example>
        /// For example, if there are multiple enemies of in the scene, and there is a health bar UI for each of them.
        /// Since these health bar all have the same EUIType, here comes the ID to identify each of the health bar.
        /// </example>
        public int ID { get; private set; }

        public UIController Controller { get; protected set; }
        
        private static int _count;
        
        public UIModel(EUIType uiType, UIController controller)
        {
            Type = uiType;
            ID = _count++;
            Controller = controller;
        }
    }
}