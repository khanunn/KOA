using System;

public class EquipmentEvents
{
    public event Action<EquipmentInfoSO, ItemInfoSO> onAddEquip;
    public void AddEquip(EquipmentInfoSO equip, ItemInfoSO item)
    {
        if (onAddEquip != null)
        {
            onAddEquip(equip, item);
        }
    }
    public event Action<ItemInfoSO> onRemoveEquip;
    public void RemoveEquip(ItemInfoSO item)
    {
        if (onRemoveEquip != null)
        {
            onRemoveEquip(item);
        }
    }

    public event Action<EquipmentInfoSO> onListEquip;
    public void ListEquip(EquipmentInfoSO equip)
    {
        if (onListEquip != null)
        {
            onListEquip(equip);
        }
    }

    public event Action onDummyIPointerEnter;
    public void DummyIPointerEnter()
    {
        if (onDummyIPointerEnter != null)
        {
            onDummyIPointerEnter();
        }
    }

    public event Action onDummyIPointerExit;
    public void DummyIPointerExit()
    {
        if (onDummyIPointerExit != null)
        {
            onDummyIPointerExit();
        }
    }

    public event Action onSlotIPointerEnter;
    public void SlotIPointerEnter()
    {
        if (onSlotIPointerEnter != null)
        {
            onSlotIPointerEnter();
        }
    }

    public event Action onSlotIPointerExit;
    public void SlotIPointerExit()
    {
        if (onSlotIPointerExit != null)
        {
            onSlotIPointerExit();
        }
    }
}