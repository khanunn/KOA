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

    private Stat Constitution, Dexterity, Strength, Wisdom, Intelligent, Lucky,
    v_hp_max, v_mp_max, v_hp_recovery, v_mp_recovery, v_patk, v_matk, v_pdef, v_mdef, v_acc, v_evade, v_crit_change, v_crit_dam, v_pdam, v_mdam;
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
        v_patk = statManager.GetStat(StatKey.v_patk);
        v_pdef = statManager.GetStat(StatKey.v_pdef);
        Constitution = statManager.GetStat(StatKey.Constitution);
        Dexterity = statManager.GetStat(StatKey.Dexterity);
        Strength = statManager.GetStat(StatKey.Strength);
        Wisdom = statManager.GetStat(StatKey.Wisdom);
        Intelligent = statManager.GetStat(StatKey.Intelligent);
        Lucky = statManager.GetStat(StatKey.Lucky);
        v_hp_max = statManager.GetStat(StatKey.v_hp_max);
        v_mp_max = statManager.GetStat(StatKey.v_mp_max);
        v_hp_recovery = statManager.GetStat(StatKey.v_hp_recovery);
        v_mp_recovery = statManager.GetStat(StatKey.v_mp_recovery);
        v_matk = statManager.GetStat(StatKey.v_matk);
        v_mdef = statManager.GetStat(StatKey.v_mdef);
        v_acc = statManager.GetStat(StatKey.v_acc);
        v_evade = statManager.GetStat(StatKey.v_evade);
        v_crit_change = statManager.GetStat(StatKey.v_crit_change);
        v_crit_dam = statManager.GetStat(StatKey.v_crit_dam);
        v_pdam = statManager.GetStat(StatKey.v_pdam);
        v_mdam = statManager.GetStat(StatKey.v_mdam);
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
                    v_patk.statValue += equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.MainStat == StatKey.Constitution)
                {
                    Constitution.statValue += equip.MainValue;
                    Debug.Log("Constitution: " + Constitution.statValue);
                }
                if (equip.MainStat == StatKey.v_pdef)
                {
                    v_pdef.statValue += equip.MainValue;
                    Debug.Log("pdef: " + v_pdef.statValue);
                }

                if (equip.SubStat == StatKey.v_patk)
                {
                    v_patk.statValue += equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.SubStat == StatKey.Constitution)
                {
                    Constitution.statValue += equip.MainValue;
                    Debug.Log("Constitution: " + Constitution.statValue);
                }
                if (equip.SubStat == StatKey.v_pdef)
                {
                    v_pdef.statValue += equip.MainValue;
                    Debug.Log("pdef: " + v_pdef.statValue);
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
                    v_patk.statValue -= equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.MainStat == StatKey.Constitution)
                {
                    Constitution.statValue -= equip.MainValue;
                    Debug.Log("Constitution: " + Constitution.statValue);
                }
                if (equip.MainStat == StatKey.v_pdef)
                {
                    v_pdef.statValue -= equip.MainValue;
                    Debug.Log("pdef: " + v_pdef.statValue);
                }

                if (equip.SubStat == StatKey.v_patk)
                {
                    v_patk.statValue -= equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.SubStat == StatKey.Constitution)
                {
                    Constitution.statValue -= equip.MainValue;
                    Debug.Log("Constitution: " + Constitution.statValue);
                }
                if (equip.SubStat == StatKey.v_pdef)
                {
                    v_pdef.statValue -= equip.MainValue;
                    Debug.Log("pdef: " + v_pdef.statValue);
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