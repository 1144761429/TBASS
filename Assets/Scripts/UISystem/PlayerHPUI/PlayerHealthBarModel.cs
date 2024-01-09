using System;
using Characters.Player.Data;

namespace UISystem
{
    public class PlayerHealthBarModel:UIModel
    {
        public event EventHandler<HealthChangeEventArgs> OnHealthChange; 
        public event EventHandler<ArmorChangeEventArgs> OnArmorChange; 
        
        private RuntimePlayerData _playerRuntimeData;

        public PlayerHealthBarModel(RuntimePlayerData runtimeData, UIController controller) : base(
            EUIType.PanelPlayerHealthBar, controller)
        {
            _playerRuntimeData = runtimeData;
        }
    }
}