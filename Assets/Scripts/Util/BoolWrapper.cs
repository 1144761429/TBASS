using System;

[Serializable]
public class BoolWrapper
{
    public bool Value;

    public BoolWrapper(bool value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}