using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public EItemType Type { get; private set; }
    [field: SerializeField] public EItemRarity Rarity { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    public int Amount;

    public virtual ItemSO Clone()
    {
        ItemSO clone = (ItemSO)ScriptableObject.CreateInstance(typeof(ItemSO).ToString());
        clone.Name = (string)this.Name.Clone();
        clone.Type = Type;
        clone.Rarity = Rarity;
        clone.Icon = Icon;

        return clone;
    }
}