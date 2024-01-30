using TMPro;
using UnityEngine;

public class ItemOptionalClickHandler : MonoBehaviour
{
    private GameObject itemObject;
    private ItemInfoSO itemInfoSO;

    private void OnEnable()
    {
        EventManager.instance.inputEvents.onItemOptionalDrop += GetItemFromClick;
    }
    private void OnDisable()
    {
        EventManager.instance.inputEvents.onItemOptionalDrop -= GetItemFromClick;
    }

    private void GetItemFromClick(GameObject obj, ItemInfoSO itemInfo)
    {
        itemObject = obj;
        itemInfoSO = itemInfo;
    }

    public void DropItem()
    {
        Destroy(itemObject);
        EventManager.instance.itemEvents.RemoveItem(itemInfoSO);
        Destroy(this.gameObject);
    }

    public void UseItem()
    {
        switch (itemInfoSO.ItemStatus)
        {
            case ItemStatus.CAN_USE:
                int itemGain = 1;
                EventManager.instance.itemEvents.ReduceItem(itemObject, itemInfoSO, itemGain);
                break;
        }
        Destroy(this.gameObject);
    }
}
