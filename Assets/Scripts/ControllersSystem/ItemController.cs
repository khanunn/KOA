using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ItemInfoSO itemInfoSO;
    private int amountGain = 1;

    public void OnTakeItem()
    {
        Tutorial.instance.SetTextTutorial("2.Talk to NPC with ? mark on head");
        EventManager.instance.itemEvents.AddItem(itemInfoSO);
        Debug.Log("AddItem TO Event: " + itemInfoSO);
        switch (itemInfoSO.scriptableObject)
        {
            case EquipmentInfoSO:
                Debug.Log("Take Equipment");
                break;
            default:
                EventManager.instance.pickupEvents.UpdateItem(itemInfoSO, amountGain);
                break;
        }

    }
}
