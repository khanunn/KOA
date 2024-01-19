using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentSlot
{
    MAINHAND, OFFHAND, HEAD, BODY, HAND, FOOT, NECK, FINGER
}
public enum EquipmentType
{
    SWORD, DAGGER, ROD, MACE, HAMMER, AXE, GREATSWORD, LONGSWORD, LONGBOW, CROSSBOW, STAFF, SPEAR, LANCE, SABER
}
public enum EquipmentRarity
{
    COMMON, RARE, EPIC, LEGENDARY, IMMORTAL
}

[CreateAssetMenu(fileName = "EquipmentInfoSO", menuName = "ScriptableObject/EquipmentInfoSO", order = 1)]
public class EquipmentInfoSO : ScriptableObject
{
    [field: Header("Genaral")]
    [field: SerializeField] public EquipmentSlot EquipmentSlot { get; private set; }
    [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
    [field: SerializeField] public EquipmentRarity EquipmentRarity { get; private set; }

    [field: Header("Property")]
    [field: SerializeField] public StatKey MainStat { get; private set; }
    [field: SerializeField] public int MainValue { get; private set; }
    [field: Space(5)]
    [field: SerializeField] public StatKey SubStat { get; private set; }
    [field: SerializeField] public int SubValue { get; private set; }



    /* private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    } */
}

