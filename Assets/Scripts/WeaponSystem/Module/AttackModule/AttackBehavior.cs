#if false



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttackBehaviorType
{
    None,
    Melee,
    Range,
}


[Serializable]
public abstract class AttackBehavior
{
    public abstract EAttackBehaviorType AttackBehaviorType { get; }
    public string Note;
}
#endif