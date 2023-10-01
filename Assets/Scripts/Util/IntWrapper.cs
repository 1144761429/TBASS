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

    // public static IntWrapper operator +(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return new IntWrapper(left.Value + right.Value);
    // }
    //
    // public static IntWrapper operator +(IntWrapper left, int right)
    // {
    //     CheckNull(left);
    //
    //     return new IntWrapper(left.Value + right);
    // }
    //
    // public static IntWrapper operator -(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return new IntWrapper(left.Value - right.Value);
    // }
    //
    // public static IntWrapper operator *(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return new IntWrapper(left.Value * right.Value);
    // }
    //
    // public static IntWrapper operator /(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return new IntWrapper(left.Value / right.Value);
    // }
    //
    // public static IntWrapper operator %(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return new IntWrapper(left.Value % right.Value);
    // }
    //
    // public static bool operator ==(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return left.Value == right.Value;
    // }
    //
    // public static bool operator !=(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return !(left == right);
    // }
    //
    // public static bool operator <(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return left.Value < right.Value;
    // }
    //
    // public static bool operator >(IntWrapper left, IntWrapper right)
    // {
    //     CheckNull(left);
    //     CheckNull(right);
    //
    //     return left.Value > right.Value;
    // }
    //
    // public static bool operator <(IntWrapper left, int right)
    // {
    //     CheckNull(left);
    //
    //     return left.Value < right;
    // }
    //
    // public static bool operator >(IntWrapper left, int right)
    // {
    //     CheckNull(left);
    //
    //     return left.Value > right;
    // }
    //
    // private static void CheckNull(IntWrapper intWrapper)
    // {
    //     if (intWrapper is null)
    //     {
    //         throw new ArgumentNullException(nameof(intWrapper));
    //     }
    // }
}