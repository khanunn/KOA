using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

enum ItemList
{

}

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemName, int> itemAmounts = new Dictionary<ItemName, int>();
    public List<ItemInfoSO> items = new List<ItemInfoSO>();
    public GameObject inventoryItem;
    public Transform itemContent;


    /* private ItemInfoSO itemInfoSO;
    private InventoryController controller;
    private void Awake()
    {
        controller = GetComponent<InventoryController>();
    } */
    private void OnEnable()
    {
        EventManager.instance.itemEvents.onRemoveItem += RemoveItem;
        EventManager.instance.itemEvents.onAddItem += AddItem;
        //EventManager.instance.itemEvents.onListNameItem += Listname;
        EventManager.instance.pickupEvents.onUpdateItem += UpdateItemAmount;
        EventManager.instance.itemEvents.onUseItem += UseItemAmount;
    }
    private void OnDisable()
    {
        EventManager.instance.itemEvents.onAddItem -= AddItem;
        EventManager.instance.itemEvents.onRemoveItem -= RemoveItem;
        //EventManager.instance.itemEvents.onListNameItem -= Listname;
        EventManager.instance.pickupEvents.onUpdateItem -= UpdateItemAmount;
        EventManager.instance.itemEvents.onUseItem -= UseItemAmount;
    }
    private void AddItem(ItemInfoSO itemInfoSO)
    {
        /* switch (itemInfoSO.ScriptableObject)
        {
            case EquipmentInfoSO equipmentInfoSO:
                Debug.Log(equipmentInfoSO.EquipmentSlot);
                Debug.Log(equipmentInfoSO.EquipmentType);
                Debug.Log(equipmentInfoSO.EquipmentRarity);
                break;
            default:
                break;
        } */
        ItemName itemName = itemInfoSO.ItemName;
        switch (itemInfoSO.scriptableObject)
        {
            case EquipmentInfoSO:
                Debug.Log("Add Equipment On Inventory");
                items.Add(itemInfoSO);
                InstantiateItem();
                break;
            default:
                if (itemAmounts.ContainsKey(itemName)) { return; }
                else
                {
                    itemAmounts.Add(itemName, 0);
                    SetItemsFlag(itemInfoSO);
                    items.Add(itemInfoSO);
                    InstantiateItem();
                }

                break;
        }
        /* if (itemAmounts.ContainsKey(itemName)) { return; }
        else
        {
            itemAmounts.Add(itemName, 0);
        }

        if (items.Contains(itemInfoSO))
        {
            Debug.Log("ไอเทมนี้ถูกเพิ่มไปยังกระเป๋าแล้ว: " + itemInfoSO.Id);
            return;
        }
        else
        {
            SetItemsFlag(itemInfoSO);
            items.Add(itemInfoSO);
            InstantiateItem();
        } */
    }
    private void RemoveItem(ItemInfoSO itemInfoSO)
    {
        //SetItemsFlag(itemInfoSO);
        ItemName itemName = itemInfoSO.ItemName;
        items.Remove(itemInfoSO);
        itemAmounts.Remove(itemName);
    }
    private void SetItemsFlag(ItemInfoSO itemInfoSO)
    {
        ItemName itemName = itemInfoSO.ItemName;
        itemAmounts[itemName] = 0;
    }

    /* private void Listname()
    {
        Dictionary<string, Inventory> idToInventory = new Dictionary<string, Inventory>();

        foreach (ItemInfoSO item in items)
        {
            if (idToInventory.ContainsKey(item.Id))
            {
                Debug.LogWarning("Found Dupplicate item ID : " + item.Id);
                return;
            }
            else
            {
                idToInventory.Add(item.Id, new Inventory(item));
                Debug.Log("Add ID To Inventory: " + item.Id);
                //takeItemId = item.id;
                //itemInfoSO = item;
            }
        }
    } */
    private void InstantiateItem()
    {
        GameObject obj = Instantiate(inventoryItem, itemContent);
        ItemClickHandler itemClickHandler = obj.GetComponent<ItemClickHandler>();
        TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        foreach (ItemInfoSO item in items)
        {
            itemClickHandler.ItemInfoSO = item;
            itemName.text = item.DisplayName;
            itemIcon.sprite = item.Icon;
        }
    }

    private void UpdateItemText(ItemInfoSO itemInfo, int dicAmount)
    {
        foreach (Transform child in itemContent)
        {
            TMP_Text itemNameText = child.Find("ItemName").GetComponent<TMP_Text>();

            if (itemNameText.text == itemInfo.DisplayName)
            {
                TMP_Text itemAmount = child.Find("ItemAmount").GetComponent<TMP_Text>();
                itemAmount.text = dicAmount.ToString();
            }
        }
    }

    public void UpdateItemAmount(ItemInfoSO itemInfoSO, int amount)
    {
        ItemName itemName = itemInfoSO.ItemName;
        itemAmounts[itemName] += amount;
        UpdateItemText(itemInfoSO, itemAmounts[itemName]);
        ItemName itemType = itemInfoSO.ItemName;
        itemAmounts[itemType] += amount; //Can use for sell -1 and buy +1
        UpdateItemText(itemInfoSO, itemAmounts[itemType]);

        if (itemAmounts[itemType] <= 0)
        {
            foreach (Transform child in itemContent)
            {
                if (child.GetComponent<ItemClickHandler>().ItemInfoSO.ItemName == itemInfoSO.ItemName)
                {
                    Destroy(child.gameObject);
                    RemoveItem(itemInfoSO);
                }
            }
        }
    }

    private void UseItemAmount(GameObject objInventory, ItemInfoSO itemInfoSO, int amount)
    {
        ItemName itemName = itemInfoSO.ItemName;
        switch (itemInfoSO.scriptableObject)
        {
            case EquipmentInfoSO equip:
                /* Debug.Log(equip.EquipmentSlot);
                Debug.Log(equip.EquipmentType);
                Debug.Log(equip.EquipmentRarity); */
                EventManager.instance.equipmentEvents.AddEquip(equip, itemInfoSO);
                Destroy(objInventory);
                RemoveItem(itemInfoSO);
                break;
            default:
                itemAmounts[itemName] -= amount;
                EventManager.instance.healthEvents.HealthGained(itemInfoSO.Value);
                Debug.Log("Drink " + itemInfoSO.name + " Value: " + itemInfoSO.Value);
                if (itemAmounts[itemName] <= 0)
                {
                    Destroy(objInventory);
                    RemoveItem(itemInfoSO);
                    itemAmounts.Remove(itemName);
                }
                else
                {
                    UpdateItemText(itemInfoSO, itemAmounts[itemName]);
                }
                break;
        }
        /* EventManager.instance.healthEvents.HealthGained(itemInfoSO.Value);
        Debug.Log("Drink " + itemInfoSO.name + " Value: " + itemInfoSO.Value); */

        /* if (itemAmounts[itemName] <= 0)
        {
            Destroy(objInventory);
            RemoveItem(itemInfoSO);
            itemAmounts.Remove(itemName);
        }
        else
        {
            UpdateItemText(itemInfoSO, itemAmounts[itemName]);
        } */
    }

    public Dictionary<ItemName, int> GetPlayerItem()
    {
        return itemAmounts;
    }

}