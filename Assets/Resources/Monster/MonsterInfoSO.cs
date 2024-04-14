using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MonsterName
{
    SkeletonZombie, RockOgre, MutantCharger
}
public enum MonsterRace
{
    Undead,Beast
}


public class ItemDropInfo
{
    public ItemInfoSO item;
    [Range(0, 1)]
    public float dropRate;
}


[CreateAssetMenu(fileName = "MonsterInfoSO", menuName = "ScriptableObject/MonsterInfoSO", order = 1)]
public class MonsterInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }

    [field: Header("General")]
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Mana { get; private set; }
    [field: SerializeField] public int AggroTime { get; private set; } //using for reset chasing player                                                                      
    [field: SerializeField] public int Evade { get; private set; } //using for calculate Evade
    [field: SerializeField] public int PhysicalDefend { get; private set; } //using for calculate PAtk - PDef
    [field: SerializeField] public int MagicDefend { get; private set; } //using for calculate MAtk - MDef
    [field: SerializeField] public int Accuracy { get; private set; } //using for calculate Acc
    [field: SerializeField] public int CritRate { get; private set; }
    [field: SerializeField] public float CritDMG { get; private set; }
    [field: SerializeField] public MonsterName MonsterName { get; private set; }
    [field: SerializeField] public MonsterRace MonsterRace { get; private set; }

    [field: Header("Inflict BadEffect Setting")]
    [field: SerializeField] public StatusInfoSO OnHitEffect { get; private set; }
    [field: SerializeField] public int OnHitEffectChance { get; private set; }

    [field: Header("Spacial Status Setting")]
    [field: SerializeField] public StatusInfoSO SelfBuffStatus { get; private set; }
    [field: SerializeField] public int SelfBuffStatusChance { get; private set; }
    [field: SerializeField] public int SelfBuffStatusInterval { get; private set; }

    [field: Header("Rewards")]

    [field: SerializeField] public int Experience { get; private set; }
    //[field: SerializeField] public int Gold { get; private set; }
    [field: Range(0, 100)][field: SerializeField, Min(0)] public int GoldMin { get; private set; }
    [field: Range(0, 100)][field: SerializeField, Min(0)] public int GoldMax { get; private set; }

    [field: Space(20)]

    [SerializeField] List<ItemInfoSO> items = new List<ItemInfoSO>();
    [field: Range(0, 1)]
    [SerializeField] List<float> dropRate = new List<float>();
    public List<ItemInfoSO> Items { get => items; set => items = value; }
    public List<float> DropRate { get => dropRate; set => dropRate = value; }

    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

}