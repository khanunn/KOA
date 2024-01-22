using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public List<ItemInfoSO> equips = new List<ItemInfoSO>();
    public GameObject equipmentItem;
    public Transform equipmentContent;
    public List<GameObject> equipmentContents = new List<GameObject>();
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
    private void AddEquip(EquipmentInfoSO equipmentInfo, ItemInfoSO itemInfoSO)
    {
        EquipmentSlot equipmentSlot = equipmentInfo.EquipmentSlot;
        equips.Add(itemInfoSO);
        InstantiateEquipment(equipmentInfo.EquipmentSlot, itemInfoSO);
    }
    private void RemoveEquip(ItemInfoSO itemInfoSO)
    {
        equips.Remove(itemInfoSO);
    }
    private void ListEquip(EquipmentInfoSO equipment)
    {

    }
    private void InstantiateEquipment(EquipmentSlot equipmentSlot, ItemInfoSO itemInfo)
    {
        foreach (GameObject contentObject in equipmentContents)
        {
            EquipmentContent content = contentObject.GetComponent<EquipmentContent>();
            Transform contentTransform = contentObject.GetComponent<Transform>();
            //Debug.Log("Slot: " + content.EquipmentSlot + "Transform: " + contentTransform);
            if (content.EquipmentSlot == equipmentSlot)
            {
                GameObject obj = Instantiate(equipmentItem, contentTransform);
                ItemClickHandler itemClickHandler = obj.GetComponent<ItemClickHandler>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                foreach (ItemInfoSO equip in equips)
                {
                    itemClickHandler.ItemInfoSO = equip;
                    //itemName.text = item.ItemName.ToString();
                    itemIcon.sprite = equip.Icon;
                }
            }
        }
    }
}