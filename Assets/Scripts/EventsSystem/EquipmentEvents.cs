using System;

public class EquipmentEvents
{
    public event Action<EquipmentInfoSO> onAddEquip;
    public void AddEquip(EquipmentInfoSO equip)
    {
        if (onAddEquip != null)
        {
            onAddEquip(equip);
        }
    }
    public event Action<EquipmentInfoSO> onRemoveEquip;
    public void RemoveEquip(EquipmentInfoSO equip)
    {
        if (onRemoveEquip != null)
        {
            onRemoveEquip(equip);
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
}