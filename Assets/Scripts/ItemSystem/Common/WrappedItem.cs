using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BuffSystem.Common;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class WrappedItem : IDisplayable
{
    public static event Action OnIncreaseAmount;
    
    public int Amount { get; private set; }
    
    public ItemData StaticData { get; private set; }
    public string Name => StaticData.Name;
    public int ID => StaticData.ID;
    public EItemType Type => StaticData.Type;
    public int Priority => 0;
    
    
    public WrappedItem(string name, int id, int amount)
    {
        StaticData = DatabaseUtil.Instance.GetItemDataConsumableSupply(name, id);
        Amount = amount;
    }

    public bool IncreaseAmount(int amount)
    {
        Amount += amount;
        return true;
    }

    public bool DecreaseAmount(int amount)
    {
        if (Amount - amount < 0)
        {
            Amount = 0;
            return true;
        }

        Amount -= amount;
        return true;
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public WrappedItem Clone()
    {
        WrappedItem clone = new WrappedItem(Name, ID, Amount);

        return clone;
    }
    
    public override string ToString()
    {
        return $"Item Name: {Name} "
               + $"ID: {ID} "
               + $"Amount: {Amount} ";
    }

    public override bool Equals([NotNull] object obj)
    {
        if (obj is not WrappedItem other)
        {
            Debug.Log("not item data");
            return false;
        }

        return StaticData.ID == other.ID && StaticData.Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name.GetHashCode(), ID.GetHashCode());
    }
}