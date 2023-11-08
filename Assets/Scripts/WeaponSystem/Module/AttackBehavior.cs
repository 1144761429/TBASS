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
public class AttackBehavior
{
    // [field: SerializeField] public EAttackBehaviorType AttackBehaviorType { get; private set; }
    // [field: SerializeField] public string Note { get; private set; }
    public EAttackBehaviorType AttackBehaviorType;
    public string Note;
}
