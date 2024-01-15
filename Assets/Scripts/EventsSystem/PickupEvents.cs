using System;
public class PickupEvents
{
    public event Action onItemPickup;
    public void ItemPickup()
    {
        if (onItemPickup != null)
        {
            onItemPickup();
        }
    }

    public event Action<ItemInfoSO, int> onUpdateItem;
    public void UpdateItem(ItemInfoSO type, int potion)
    {
        if (onUpdateItem != null)
        {
            onUpdateItem(type, potion);
        }
    }
}