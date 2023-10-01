using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player")]
public class PlayerDataSO : ScriptableObject
{
    public float CurrentHp { get; set; }
    [field: SerializeField] public float MaxHp { get; private set; }
    [field: SerializeField] public float MinHp { get; private set; }

    public float CurrentSheild { get; set; }
    [field: SerializeField] public float MaxShield { get; private set; }
    [field: SerializeField] public float MinShield { get; private set; }

    [field: SerializeField] public float WalkSpeed { get; private set; }
    [field: SerializeField] public float SprintSpeed { get; private set; }


    public void Init()
    {
        CurrentHp = MaxHp;
        CurrentSheild = MaxShield;
    }

    public void Init(float initialHp, float initialShield)
    {
        CurrentHp = initialHp;
        CurrentSheild = initialShield;
    }
}