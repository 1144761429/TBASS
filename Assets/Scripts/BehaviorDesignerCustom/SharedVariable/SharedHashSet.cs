using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace BehaviorDesignerCustom
{
    [System.Serializable]
    public class SharedHashSet : SharedVariable<HashSet<GameObject>>
    {
        public static implicit operator SharedHashSet(HashSet<GameObject> value) { return new SharedHashSet { Value = value }; }
    }
}