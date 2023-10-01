using System;
using System.Collections.Generic;
using UnityEngine;

namespace StackableElement
{
    public abstract class StackableElementHandler<TIDEnumType, TGenericValueType> where TIDEnumType : Enum
    {
        public Action<StackableElement<TGenericValueType>> OnAddElement;
        public Action<StackableElement<TGenericValueType>> OnRemoveElement;

        protected Dictionary<TIDEnumType, StackableElement<TGenericValueType>> Dict;

        public StackableElementHandler()
        {
            Dict = new Dictionary<TIDEnumType, StackableElement<TGenericValueType>>();
        }

        public void Add(TIDEnumType enumType, StackableElement<TGenericValueType> stackableElement)
        {
            if (Contain(enumType))
            {
                Debug.Log($"Key {enumType} already exists in the StackableElementHandler");
                return;
            }
            else
            {
                Dict.Add(enumType, stackableElement);
                OnAddElement?.Invoke(stackableElement);
            }
        }

        public void Remove(TIDEnumType enumType)
        {
            if (Dict.TryGetValue(enumType, out StackableElement<TGenericValueType> value))
            {
                OnRemoveElement?.Invoke(value);
            }
            else
            {
                Debug.Log($"Key {enumType} does not exist in the StackableElementHandler");
            }
        }

        public void ResetAll()
        {
            foreach (StackableElement<TGenericValueType> stackableElement in Dict.Values)
            {
                stackableElement.ResetStack();
            }
        }

        public void ResetStackableElement(TIDEnumType enumType)
        {
            Dict[enumType].ResetStack();
        }

        public void Clear()
        {
            Dict.Clear();
        }

        public bool Contain(TIDEnumType enumType)
        {
            return Dict.ContainsKey(enumType);
        }

        public StackableElement<TGenericValueType> Get(TIDEnumType enumType)
        {
            return Dict[enumType];
        }

        public void DebugContainedStackableElement()
        {
            string str = "";

            foreach (var kyPair in Dict)
            {
                str += $"{kyPair.Key.ToString()} ----- {kyPair.Value.ToString()}\n";
            }

            Debug.Log(str);
        }
    }
}