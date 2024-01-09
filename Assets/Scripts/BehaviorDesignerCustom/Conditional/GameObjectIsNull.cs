using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerCustom
{
    [TaskCategory("Unity/GameObject")]
    public class GameObjectIsNull : Conditional
    {
        [Tooltip("The GameObject to check is is not null.")]
        public SharedGameObject gameObject;

        public override TaskStatus OnUpdate()
        {
            if (gameObject.Value == null)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            gameObject = null;
        }
    }
}