using BuffSystem.Common;

namespace UISystem
{
    public class PlayerStatusSlotController : UIController
    {
        public override EUIType Type => EUIType.Component;
        
        private PlayerStatusSlotModel _model => Model as PlayerStatusSlotModel;
        private PlayerStatusSlotVisual _visual => Visual as PlayerStatusSlotVisual;

        private void Awake()
        {
            Model = new PlayerStatusSlotModel(this);

            _model.OnUpdateData += _visual.UpdateVisual;
        }

        private void Update()
        {
            // TODO: add a if check: if model.status is not null
            _visual.UpdateRemainingTime(_model.Status);
        }

        public void SetStatus(Buff status)
        {
            _model.UpdateData(status);
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