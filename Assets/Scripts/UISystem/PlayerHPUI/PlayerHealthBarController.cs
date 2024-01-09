
namespace UISystem
{
    public class PlayerHealthBarController : UIController
    {
        public override EUIType Type => EUIType.PanelPlayerHealthBar;
        
        private PlayerHealthBarVisual _visual => Visual as PlayerHealthBarVisual;
        
        private void Awake()
        {
            PlayerStats.Instance.OnHealthChange += _visual.UpdateHealthVisual;
            PlayerStats.Instance.OnArmorChange += _visual.UpdateArmorVisual;
        }

        public override void Open()
        {
            Visual.VisualRoot.SetActive(true);
        }

        public override void Close()
        {
            Visual.VisualRoot.SetActive(false);
        }
    }
}