using System.Collections.Generic;
using System.Linq;
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
        foreach (ItemInfoSO equip in equips.ToList())
        {
            switch (equip.scriptableObject)
            {
                case EquipmentInfoSO isInsertedEquip:
                    if (isInsertedEquip.EquipmentSlot == equipmentInfo.EquipmentSlot)
                    {
                        EventManager.instance.itemEvents.AddItem(equip);
                        equips.Remove(equip);
                        equips.Add(itemInfoSO);
                        return;
                        /* if (equips.Contains(itemInfoSO))
                        {
                            //equip = itemInfoSO;
                            equip.scriptableObject = equipmentInfo;
                            itemInfoSO.scriptableObject = isInsertedEquip;
                            EventManager.instance.itemEvents.AddItem(itemInfoSO);
                            Debug.Log("Swap Change");
                            return;
                        }
                        else
                        {
                            EventManager.instance.itemEvents.AddItem(equip);
                            equips.Remove(equip);
                            equips.Add(itemInfoSO);
                            return;
                        } */
                    }

                    break;
                default:
                    break;
            }
        }

        equips.Add(itemInfoSO);
        InstantiateEquipment(equipmentInfo.EquipmentSlot, itemInfoSO);
    }
    private void RemoveEquip(ItemInfoSO itemInfoSO, GameObject gameObject)
    {
        equips.Remove(itemInfoSO);
        Destroy(gameObject);
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