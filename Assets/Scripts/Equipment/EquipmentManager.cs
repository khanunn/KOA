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

    private Stat patk, pdef;
    private StatManager statManager;
    private List<ItemClickHandler> itemClickHandlers = new List<ItemClickHandler>();

    private void OnEnable()
    {
        EventManager.instance.equipmentEvents.onAddEquip += AddEquip;
        EventManager.instance.equipmentEvents.onRemoveEquip += RemoveEquip;
        EventManager.instance.statEvents.onSendStatManager += StartStatus;
    }
    private void OnDisable()
    {
        EventManager.instance.equipmentEvents.onAddEquip -= AddEquip;
        EventManager.instance.equipmentEvents.onRemoveEquip -= RemoveEquip;
        EventManager.instance.statEvents.onSendStatManager -= StartStatus;
    }
    private void Start()
    {
        patk = statManager.GetStat(StatKey.v_patk);
        pdef = statManager.GetStat(StatKey.v_pdef);
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
                        HandleEquipReplacement(equip, itemInfoSO);
                        return;
                    }
                    break;
            }
        }

        equips.Add(itemInfoSO);
        OnStat(itemInfoSO);
        InstantiateEquipment(equipmentInfo.EquipmentSlot, itemInfoSO);
    }

    private void HandleEquipReplacement(ItemInfoSO existingEquip, ItemInfoSO newEquip)
    {
        EventManager.instance.itemEvents.AddItem(existingEquip);
        equips.Remove(existingEquip);
        OffStat(existingEquip);

        equips.Add(newEquip);
        OnStat(newEquip);

        foreach (var item in itemClickHandlers)
        {
            if (item.ItemInfoSO == existingEquip)
            {
                item.ItemInfoSO = newEquip;
            }
        }
    }

    private void RemoveEquip(ItemInfoSO itemInfoSO, GameObject gameObject)
    {
        equips.Remove(itemInfoSO);
        OffStat(itemInfoSO);
        Destroy(gameObject);
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
                    itemClickHandlers.Add(itemClickHandler);
                    //itemName.text = item.ItemName.ToString();
                    itemIcon.sprite = equip.Icon;
                }
            }
        }
    }
    private void OnStat(ItemInfoSO info)
    {
        ScriptableObject script = info.scriptableObject;
        switch (script)
        {
            case EquipmentInfoSO equip:
                if (equip.MainStat == StatKey.v_patk)
                {
                    patk.statValue += equip.MainValue;
                    Debug.Log("patk: " + patk.statValue);
                }
                break;
        }
        EventManager.instance.statEvents.ShowStat();
    }
    private void OffStat(ItemInfoSO info)
    {
        ScriptableObject script = info.scriptableObject;
        switch (script)
        {
            case EquipmentInfoSO equip:
                if (equip.MainStat == StatKey.v_patk)
                {
                    patk.statValue -= equip.MainValue;
                    Debug.Log("patk: " + patk.statValue);
                }
                break;
        }
        EventManager.instance.statEvents.ShowStat();
    }
    private void StartStatus(StatManager myStat)
    {
        statManager = myStat;
    }
}