using System;
using System.Reflection;

[Serializable]
public class WrappedItem<T> where T : ItemData
{
    public T Data;
    public IntWrapper Amount;

    public string Name
    {
        get
        {
            if (Data == null)
            {
                throw new Exception("Data is not assigned.");
            }
            return Data.Name;
        }
    }
    public int ID
    {
        get
        {
            if (Data == null)
            {
                throw new Exception("Data is not assigned.");
            }
            return Data.ID;
        }
    }
    public EItemType Type
    {
        get
        {
            if (Data == null)
            {
                throw new Exception("Data is not assigned.");
            }
            return Data.Type;
        }
    }

    public WrappedItem(T data, IntWrapper amount)
    {
        Data = data;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"Contained Item: {Data.ToString()}"
        + $"Amount: {Amount.Value}";
    }

    public override bool Equals(object obj)
    {
        PropertyInfo propInfo = obj.GetType().GetProperty("ID");
        if (propInfo != null)
        {
            object propertyValue = propInfo.GetValue(obj);
            int i = (int)propertyValue;
            return ID == i;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Data);
    }
}

[Serializable]
public class WrappedItem : WrappedItem<ItemData>
{
    public WrappedItem(ItemData data, IntWrapper amount) : base(data, amount)
    {
    }
}