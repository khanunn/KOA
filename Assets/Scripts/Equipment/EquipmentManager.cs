using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<EquipmentInfoSO> equips = new List<EquipmentInfoSO>();
    public GameObject equipmentItem;
    public Transform equipmentContent;
    private void OnEnable()
    {
        EventManager.instance.equipmentEvents.onAddEquip += AddEquip;
        EventManager.instance.equipmentEvents.onRemoveEquip += RemoveEquip;
        EventManager.instance.equipmentEvents.onListEquip += ListEquip;
    }
    private void OnDisable()
    {
        EventManager.instance.equipmentEvents.onAddEquip -= AddEquip;
        EventManager.instance.equipmentEvents.onRemoveEquip -= RemoveEquip;
        EventManager.instance.equipmentEvents.onListEquip -= ListEquip;
    }
    private void AddEquip(EquipmentInfoSO equipmentInfo)
    {
        EquipmentSlot equipmentSlot = equipmentInfo.EquipmentSlot;
        equips.Add(equipmentInfo);
        //InstantiateItem();
    }
    private void RemoveEquip(EquipmentInfoSO equipment)
    {

    }
    private void ListEquip(EquipmentInfoSO equipment)
    {

    }
    /* private void InstantiateItem()
    {
        GameObject obj = Instantiate(equipmentItem, equipmentContent);
        ItemClickHandler itemClickHandler = obj.GetComponent<ItemClickHandler>();
        TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        foreach (EquipmentInfoSO equip in equips)
        {
            itemClickHandler.ItemInfoSO = item;
            itemName.text = equip.name;
            itemIcon.sprite = item.Icon;
        }
    } */
}