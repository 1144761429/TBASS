namespace UISystem
{
    public class PlayerStatusBarController : UIController
    {
        public override EUIType Type => EUIType.PanelPlayerBuff;
        
        public int MaxDisplayAmount => _visual.MaxDisplayAmount;
        
        private PlayerStatusBarModel _model => Model as PlayerStatusBarModel;
        private PlayerStatusBarVisual _visual => Visual as PlayerStatusBarVisual;
        
        
        private void Awake()
        {
            Model = new PlayerStatusBarModel(this, MaxDisplayAmount);
            
            _model.OnAddStatus += _visual.UpdateStatusToDisplay;
            _model.OnRemoveStatus += _visual.UpdateStatusToDisplay;
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