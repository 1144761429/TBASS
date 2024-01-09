using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerCustom
{
    public class Fail : Action
    {
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Failure;
        }
    }
}