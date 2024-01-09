using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player")]
public class PlayerDataSO : ScriptableObject
{
    [field: SerializeField] public float MaxHP { get; private set; }
    [field: SerializeField] public float MinHP { get; private set; }
    
    [field: SerializeField] public float MaxArmor { get; private set; }
    [field: SerializeField] public float MinArmor { get; private set; }

    [field: SerializeField] public float WalkSpeed { get; private set; }
    [field: SerializeField] public float SprintSpeed { get; private set; }
}