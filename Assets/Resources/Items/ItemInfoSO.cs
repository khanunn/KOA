using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemName
{
    POTION_RED, POTION_GREEN, POTION_BLUE,
    MAT_CHARCOAL, MAT_IRON, MAT_RUBY,
    BONE,
    SWORD

}
public enum ItemStatus
{
    CAN_USE, NOT_AVAILABLE
}

[CreateAssetMenu(fileName = "ItemInfoSO", menuName = "ScriptableObject/ItemInfoSO", order = 1)]
public class ItemInfoSO : ScriptableObject
{
    [field: SerializeField] public string Id { get; protected set; }

    [field: Header("Genaral")]
    [field: SerializeField] public string displayName { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public ItemName ItemName { get; private set; }
    [field: SerializeField] public ItemStatus ItemStatus { get; private set; }
    [field: SerializeField] public ScriptableObject ScriptableObject { get; private set; }

    private void OnValidate()
    {
        //Debug.Log("OnValidate");
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

