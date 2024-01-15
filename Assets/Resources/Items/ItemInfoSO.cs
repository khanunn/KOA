using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemName
{
    POTION_RED, POTION_GREEN, POTION_BLUE,
    MAT_CHARCOAL, MAT_IRON, MAT_RUBY,
    BONE
}
public enum ItemStatus
{
    CAN_USE, NOT_AVAILABLE
}

[CreateAssetMenu(fileName = "ItemInfoSO", menuName = "ScriptableObject/ItemInfoSO", order = 1)]
public class ItemInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("Genaral")]
    public string displayName;
    public int value;
    public Sprite icon;
    public ItemName itemType;
    public ItemStatus itemStatus;


    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

