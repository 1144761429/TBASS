using System;

[Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public EItemType Type;
    public EItemRarity Rarity;
    public string SpritePath;
    
    public override string ToString()
    {
        return $"ID: {ID}\n"
        + $"Name: {Name}\n"
        + $"Type: {Type}\n";
    }

    public override bool Equals(object obj)
    {
        if (!(obj is ItemData))
        {
            return false;
        }

        return ((ItemData)obj).ID == ID;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ID);
    }
}
