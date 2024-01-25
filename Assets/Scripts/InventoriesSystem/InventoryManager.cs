using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemName, int> itemAmounts = new Dictionary<ItemName, int>();
    public List<ItemInfoSO> items = new List<ItemInfoSO>();
    public GameObject inventoryItem;
    public Transform itemContent;
    private ItemInfoSO itemInfoSO;
    private InventoryController controller;
    private void Awake()
    {
        controller = GetComponent<InventoryController>();
    }
    private void OnEnable()
    {
        EventManager.instance.itemEvents.onRemoveItem += RemoveItem;
        EventManager.instance.itemEvents.onAddItem += AddItem;
        EventManager.instance.itemEvents.onListNameItem += Listname;
        EventManager.instance.pickupEvents.onUpdateItem += UpdateItemAmount;
        EventManager.instance.itemEvents.onUseItem += UseItemAmount;
    }
    private void OnDisable()
    {
        EventManager.instance.itemEvents.onAddItem -= AddItem;
        EventManager.instance.itemEvents.onRemoveItem -= RemoveItem;
        EventManager.instance.itemEvents.onListNameItem -= Listname;
        EventManager.instance.pickupEvents.onUpdateItem -= UpdateItemAmount;
        EventManager.instance.itemEvents.onUseItem -= UseItemAmount;
    }
    private void AddItem(ItemInfoSO itemInfoSO)
    {
        ItemName itemType = itemInfoSO.itemType;
        if (itemAmounts.ContainsKey(itemType)) { return; }
        else
        {
            itemAmounts.Add(itemType, 0);
        }

        if (items.Contains(itemInfoSO))
        {
            Debug.Log("ไอเทมนี้ถูกเพิ่มไปยังกระเป๋าแล้ว: " + itemInfoSO.id);
            return;
        }
        else
        {
            SetItemsFlag(itemInfoSO);
            items.Add(itemInfoSO);
            InstantiateItem();
        }
    }
    private void RemoveItem(ItemInfoSO itemInfoSO)
    {
        //SetItemsFlag(itemInfoSO);
        ItemName itemType = itemInfoSO.itemType;
        items.Remove(itemInfoSO);
        itemAmounts.Remove(itemType);
    }
    private void SetItemsFlag(ItemInfoSO itemInfoSO)
    {
        ItemName itemType = itemInfoSO.itemType;
        itemAmounts[itemType] = 0;
    }

    private void Listname()
    {
        Dictionary<string, Inventory> idToInventory = new Dictionary<string, Inventory>();

        foreach (ItemInfoSO item in items)
        {
            if (idToInventory.ContainsKey(item.id))
            {
                Debug.LogWarning("Found Dupplicate item ID : " + item.id);
                return;
            }
            else
            {
                idToInventory.Add(item.id, new Inventory(item));
                Debug.Log("Add ID To Inventory: " + item.id);
                //takeItemId = item.id;
                itemInfoSO = item;
            }
        }
    }
    private void InstantiateItem()
    {
        GameObject obj = Instantiate(inventoryItem, itemContent);
        ItemClickHandler itemClickHandler = obj.GetComponent<ItemClickHandler>();
        TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        foreach (ItemInfoSO item in items)
        {
            itemClickHandler.ItemInfoSO = item;
            itemName.text = item.name;
            itemIcon.sprite = item.icon;
        }
    }

    private void UpdateItemText(ItemInfoSO itemName, int amount)
    {
        foreach (Transform child in itemContent)
        {
            TMP_Text itemNameText = child.Find("ItemName").GetComponent<TMP_Text>();

            if (itemNameText.text == itemName.id)
            {
                TMP_Text itemAmount = child.Find("ItemAmount").GetComponent<TMP_Text>();
                itemAmount.text = amount.ToString();
            }
        }
    }

    public void UpdateItemAmount(ItemInfoSO itemInfoSO, int amount)
    {
        ItemName itemType = itemInfoSO.itemType;
        itemAmounts[itemType] += amount; //Can use for sell -1 and buy +1
        UpdateItemText(itemInfoSO, itemAmounts[itemType]);

        if (itemAmounts[itemType] <= 0)
        {
            foreach (Transform child in itemContent)
            {
                if (child.GetComponent<ItemClickHandler>().ItemInfoSO.itemType == itemInfoSO.itemType)
                {
                    Destroy(child.gameObject);
                    RemoveItem(itemInfoSO);
                }
            }
        }       
    }

    private void UseItemAmount(GameObject objInventory, ItemInfoSO itemInfoSO, int amount)
    {
        ItemName itemType = itemInfoSO.itemType;

        itemAmounts[itemType] -= amount;
        EventManager.instance.healthEvents.HealthGained(itemInfoSO.value);
        Debug.Log("Drink " + itemInfoSO.name + " Value: " + itemInfoSO.value);

        if (itemAmounts[itemType] <= 0)
        {
            Destroy(objInventory);
            RemoveItem(itemInfoSO);
            itemAmounts.Remove(itemType);
        }
        else
        {
            UpdateItemText(itemInfoSO, itemAmounts[itemType]);
        }
    }

    public Dictionary<ItemName, int> GetPlayerItem()
    {
        return itemAmounts;
    }

}