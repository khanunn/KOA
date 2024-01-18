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
    [field: SerializeField] public string id { get; private set; }

    [Header("Genaral")]
    public string displayName;
    public int value;
    //public Sprite icon;
    public EquipmentSlot equipmentSlot;
    public EquipmentType equipmentType;
    public EquipmentRarity equipmentRarity;



    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        /* var icon = Resources.LoadAll<Sprite>("Items/icons/");
        //Resources.UnloadAsset(icon); */
#endif
    }
}

