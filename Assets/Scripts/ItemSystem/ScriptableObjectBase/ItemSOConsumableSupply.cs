using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemSOConsumableSupply", menuName = "Item/Consumable/Supply")]
public class ItemSOConsumableSupply : ItemSOConsumable
{
    public float HPRec;
    public float HPRecPerSec;
    public float HPRecDuration;

    public float ShieldRec;
    public float ShieldRecPerSec;
    public float ShieldRecDuration;

    public float AttackPctInc;
    public float MovementSpeedPctInc;
}
