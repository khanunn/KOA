using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    //private TMP_Text itemName;
    private Transform itemTransform;
    public ItemInfoSO ItemInfoSO;

    private void Awake()
    {
        //
        //itemName = this.gameObject.transform.Find("ItemName").GetComponent<TMP_Text>();
        itemTransform = this.gameObject.transform;
    }
    public void OpenOptionalItem()
    {
        string item = itemName.text;
        EventManager.instance.inputEvents.InventoryItemOptional(item, itemTransform, ItemInfoSO);
        EventManager.instance.inputEvents.ItemOptionalDrop(this.gameObject, ItemInfoSO);
        Debug.Log("item: " + item + " OpenOptionOpened");
    }

    public void CloseOptionalItem()
    {
        EventManager.instance.inputEvents.InventoryItemOptionalClose();
    }
}
