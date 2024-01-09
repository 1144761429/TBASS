using System;
using JetBrains.Annotations;

[Serializable]
public class IntWrapper
{
    public int Value;

    public IntWrapper(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}