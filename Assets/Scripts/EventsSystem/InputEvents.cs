using System;
using UnityEngine;

public class InputEvents
{
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }
    public event Action onInventoryPressed;
    public void InventoryPressed()
    {
        if (onInventoryPressed != null)
        {
            onInventoryPressed();
        }
    }
    public event Action onStatPressed;
    public void StatPressed()
    {
        if (onStatPressed != null)
        {
            onStatPressed();
        }
    }
    public event Action<string, Transform, ItemInfoSO> onInventoryItemOptional;
    public void InventoryItemOptional(string item, Transform transform, ItemInfoSO info)
    {
        if (onInventoryItemOptional != null)
        {
            onInventoryItemOptional(item, transform, info);
        }
    }
    public event Action onInventoryItemOptionalClose;
    public void InventoryItemOptionalClose()
    {
        if (onInventoryItemOptionalClose != null)
        {
            onInventoryItemOptionalClose();
        }
    }

    public event Action<GameObject, ItemInfoSO> onItemOptionalDrop;
    public void ItemOptionalDrop(GameObject obj, ItemInfoSO itemInfoSO)
    {
        if (onItemOptionalDrop != null)
        {
            onItemOptionalDrop(obj, itemInfoSO);
        }
    }
}