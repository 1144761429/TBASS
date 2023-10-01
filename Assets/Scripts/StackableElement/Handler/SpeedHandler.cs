using System;

namespace StackableElement
{
    public class SpeedHandler<TIDEnumType> : StackableElementHandler<TIDEnumType, float> where TIDEnumType : Enum
    {
        public float CalculateValue()
        {
            float speed = 0;
            foreach (var stackableElement in Dict.Values)
            {
                speed += stackableElement.Value * stackableElement.Stack.Value;
            }

            return speed;
        }
    }
}