using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TestTwo : MonoBehaviour
{
    public int IntOne;
    public List<int> IntList;

    public List<AttackBehavior> AttackBehaviors;

    private void Awake()
    {
        AttackBehaviors = new List<AttackBehavior>();
        AttackBehaviors.Add(new AttackBehavior());
    }
}