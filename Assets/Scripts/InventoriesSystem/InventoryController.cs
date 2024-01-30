using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventory;
    private bool inventorySwitch;

    [Header("Optional Item")]
    private InventoryManager inventoryManager;
    private Transform itemContent;
    public GameObject itemOptional;
    public Transform transformViewport;
    private GameObject objInstant;

    [Header("Config")]
    [SerializeField] private Vector3 positionOptional;


    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        itemContent = inventoryManager.itemContent;
    }

    private void OnEnable()
    {
        EventManager.instance.inputEvents.onInventoryPressed += SwitchInventory;
        EventManager.instance.inputEvents.onInventoryItemOptional += OptionalItemOpen;
        EventManager.instance.inputEvents.onInventoryItemOptionalClose += OptionalItemClose;
    }
    private void OnDisable()
    {
        EventManager.instance.inputEvents.onInventoryPressed -= SwitchInventory;
        EventManager.instance.inputEvents.onInventoryItemOptional -= OptionalItemOpen;
        EventManager.instance.inputEvents.onInventoryItemOptionalClose -= OptionalItemClose;
    }

    public void SwitchInventory()
    {
        if (!inventorySwitch)
        {
            inventory.SetActive(true);
            inventorySwitch = true;
        }
        else
        {
            inventory.SetActive(false);
            inventorySwitch = false;
        }
    }

    public void ExitButton()
    {
        inventory.SetActive(false);
        inventorySwitch = false;
        DestroyOptionalItem();
    }

    private void OptionalItemOpen(string item, Transform transformItemInventory, ItemInfoSO info)
    {
        DestroyOptionalItem();
        InstantiateOptionalItem(transformItemInventory, info);

        /* foreach (Transform child in itemContent)
        {
            TMP_Text itemName = child.Find("ItemName").GetComponent<TMP_Text>();
            if (itemName.text == item)
            {
                InstantiateOptionalItem(transformItemInventory, info);
            }
        } */
    }
    private void OptionalItemClose()
    {
        DestroyOptionalItem();
    }

    private void InstantiateOptionalItem(Transform transform, ItemInfoSO info)
    {
        objInstant = Instantiate(itemOptional, transformViewport);
        objInstant.transform.position = transform.position + new Vector3(positionOptional.x, positionOptional.y, 0);
        /* switch (info.ItemStatus)
        {
            case ItemStatus.NOT_AVAILABLE:
                GameObject buttonUse = objInstant.transform.Find("ButtonUse").gameObject;
                buttonUse.SetActive(false);
                break;
        } */
    }

    private void DestroyOptionalItem()
    {
        if (objInstant == null) return;
        Destroy(objInstant);
    }
}