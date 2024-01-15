using System;
using UnityEngine;

public class ItemEvents
{
    public event Action<ItemInfoSO> onAddItem;
    public void AddItem(ItemInfoSO itemInfoSO)
    {
        if (onAddItem != null)
        {
            onAddItem(itemInfoSO);
        }
    }

    public event Action<ItemInfoSO> onRemoveItem;
    public void RemoveItem(ItemInfoSO itemInfoSO)
    {
        if (onRemoveItem != null)
        {
            onRemoveItem(itemInfoSO);
        }
    }

    public event Action onListNameItem;
    public void ListNameItem()
    {
        if (onListNameItem != null)
        {
            onListNameItem();
        }
    }

    public event Action<GameObject, ItemInfoSO, int> onUseItem;
    public void ReduceItem(GameObject objInventory, ItemInfoSO item, int amount)
    {
        if (onUseItem != null)
        {
            onUseItem(objInventory, item, amount);
        }
    }

}