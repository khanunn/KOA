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

    private Stat constitution, dexterity, strength, wisdom, intelligent, lucky,
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
        constitution = statManager.GetStat(StatKey.Constitution);
        dexterity = statManager.GetStat(StatKey.Dexterity);
        strength = statManager.GetStat(StatKey.Strength);
        wisdom = statManager.GetStat(StatKey.Wisdom);
        intelligent = statManager.GetStat(StatKey.Intelligent);
        lucky = statManager.GetStat(StatKey.Lucky);
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
                if (equip.MainStat == StatKey.Constitution)
                {
                    constitution.statValue += equip.MainValue;
                    Debug.Log("Constitution: " + constitution.statValue);
                }
                if (equip.MainStat == StatKey.Dexterity)
                {
                    dexterity.statValue += equip.MainValue;
                    Debug.Log("Dexterity: " + dexterity.statValue);
                }
                if (equip.MainStat == StatKey.Strength)
                {
                    strength.statValue += equip.MainValue;
                    Debug.Log("Strength: " + strength.statValue);
                }
                if (equip.MainStat == StatKey.Wisdom)
                {
                    wisdom.statValue += equip.MainValue;
                    Debug.Log("Wisdom: " + wisdom.statValue);
                }
                if (equip.MainStat == StatKey.Intelligent)
                {
                    intelligent.statValue += equip.MainValue;
                    Debug.Log("Intelligent: " + intelligent.statValue);
                }
                if (equip.MainStat == StatKey.Lucky)
                {
                    lucky.statValue += equip.MainValue;
                    Debug.Log("Lucky: " + lucky.statValue);
                }
                if (equip.MainStat == StatKey.v_hp_max)
                {
                    v_hp_max.statValue += equip.MainValue;
                    Debug.Log("v_hp_max: " + v_hp_max.statValue);
                }
                if (equip.MainStat == StatKey.v_mp_max)
                {
                    v_mp_max.statValue += equip.MainValue;
                    Debug.Log("v_mp_max: " + v_mp_max.statValue);
                }
                if (equip.MainStat == StatKey.v_hp_recovery)
                {
                    v_hp_recovery.statValue += equip.MainValue;
                    Debug.Log("v_hp_recovery: " + v_hp_recovery.statValue);
                }
                if (equip.MainStat == StatKey.v_mp_recovery)
                {
                    v_mp_recovery.statValue += equip.MainValue;
                    Debug.Log("v_mp_recovery: " + v_mp_recovery.statValue);
                }
                if (equip.MainStat == StatKey.v_patk)
                {
                    v_patk.statValue += equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.MainStat == StatKey.v_matk)
                {
                    v_matk.statValue += equip.MainValue;
                    Debug.Log("v_matk: " + v_matk.statValue);
                }
                if (equip.MainStat == StatKey.v_pdef)
                {
                    v_pdef.statValue += equip.MainValue;
                    Debug.Log("v_pdef: " + v_pdef.statValue);
                }
                if (equip.MainStat == StatKey.v_mdef)
                {
                    v_mdef.statValue += equip.MainValue;
                    Debug.Log("v_mdef: " + v_mdef.statValue);
                }
                if (equip.MainStat == StatKey.v_acc)
                {
                    v_acc.statValue += equip.MainValue;
                    Debug.Log("v_acc: " + v_acc.statValue);
                }
                if (equip.MainStat == StatKey.v_evade)
                {
                    v_evade.statValue += equip.MainValue;
                    Debug.Log("v_evade: " + v_evade.statValue);
                }
                if (equip.MainStat == StatKey.v_crit_change)
                {
                    v_crit_change.statValue += equip.MainValue;
                    Debug.Log("v_crit_change: " + v_crit_change.statValue);
                }
                if (equip.MainStat == StatKey.v_crit_dam)
                {
                    v_crit_dam.statValue += equip.MainValue;
                    Debug.Log("v_crit_dam: " + v_crit_dam.statValue);
                }
                if (equip.MainStat == StatKey.v_pdam)
                {
                    v_pdam.statValue += equip.MainValue;
                    Debug.Log("v_pdam: " + v_pdam.statValue);
                }
                if (equip.MainStat == StatKey.v_mdam)
                {
                    v_mdam.statValue += equip.MainValue;
                    Debug.Log("v_mdam: " + v_mdam.statValue);
                }

                if (equip.SubStat == StatKey.Constitution)
                {
                    constitution.statValue += equip.MainValue;
                    Debug.Log("Constitution: " + constitution.statValue);
                }
                if (equip.SubStat == StatKey.Dexterity)
                {
                    dexterity.statValue += equip.MainValue;
                    Debug.Log("Dexterity: " + dexterity.statValue);
                }
                if (equip.SubStat == StatKey.Strength)
                {
                    strength.statValue += equip.MainValue;
                    Debug.Log("Strength: " + strength.statValue);
                }
                if (equip.SubStat == StatKey.Wisdom)
                {
                    wisdom.statValue += equip.MainValue;
                    Debug.Log("Wisdom: " + wisdom.statValue);
                }
                if (equip.SubStat == StatKey.Intelligent)
                {
                    intelligent.statValue += equip.MainValue;
                    Debug.Log("Intelligent: " + intelligent.statValue);
                }
                if (equip.SubStat == StatKey.Lucky)
                {
                    lucky.statValue += equip.MainValue;
                    Debug.Log("Lucky: " + lucky.statValue);
                }
                if (equip.SubStat == StatKey.v_hp_max)
                {
                    v_hp_max.statValue += equip.MainValue;
                    Debug.Log("v_hp_max: " + v_hp_max.statValue);
                }
                if (equip.SubStat == StatKey.v_mp_max)
                {
                    v_mp_max.statValue += equip.MainValue;
                    Debug.Log("v_mp_max: " + v_mp_max.statValue);
                }
                if (equip.SubStat == StatKey.v_hp_recovery)
                {
                    v_hp_recovery.statValue += equip.MainValue;
                    Debug.Log("v_hp_recovery: " + v_hp_recovery.statValue);
                }
                if (equip.SubStat == StatKey.v_mp_recovery)
                {
                    v_mp_recovery.statValue += equip.MainValue;
                    Debug.Log("v_mp_recovery: " + v_mp_recovery.statValue);
                }
                if (equip.SubStat == StatKey.v_patk)
                {
                    v_patk.statValue += equip.MainValue;
                    Debug.Log("patk: " + v_patk.statValue);
                }
                if (equip.SubStat == StatKey.v_matk)
                {
                    v_matk.statValue += equip.MainValue;
                    Debug.Log("v_matk: " + v_matk.statValue);
                }
                if (equip.SubStat == StatKey.v_pdef)
                {
                    v_pdef.statValue += equip.MainValue;
                    Debug.Log("v_pdef: " + v_pdef.statValue);
                }
                if (equip.SubStat == StatKey.v_mdef)
                {
                    v_mdef.statValue += equip.MainValue;
                    Debug.Log("v_mdef: " + v_mdef.statValue);
                }
                if (equip.SubStat == StatKey.v_acc)
                {
                    v_acc.statValue += equip.MainValue;
                    Debug.Log("v_acc: " + v_acc.statValue);
                }
                if (equip.SubStat == StatKey.v_evade)
                {
                    v_evade.statValue += equip.MainValue;
                    Debug.Log("v_evade: " + v_evade.statValue);
                }
                if (equip.SubStat == StatKey.v_crit_change)
                {
                    v_crit_change.statValue += equip.MainValue;
                    Debug.Log("v_crit_change: " + v_crit_change.statValue);
                }
                if (equip.SubStat == StatKey.v_crit_dam)
                {
                    v_crit_dam.statValue += equip.MainValue;
                    Debug.Log("v_crit_dam: " + v_crit_dam.statValue);
                }
                if (equip.SubStat == StatKey.v_pdam)
                {
                    v_pdam.statValue += equip.MainValue;
                    Debug.Log("v_pdam: " + v_pdam.statValue);
                }
                if (equip.SubStat == StatKey.v_mdam)
                {
                    v_mdam.statValue += equip.MainValue;
                    Debug.Log("v_mdam: " + v_mdam.statValue);
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
                    constitution.statValue -= equip.MainValue;
                    Debug.Log("Constitution: " + constitution.statValue);
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
                    constitution.statValue -= equip.MainValue;
                    Debug.Log("Constitution: " + constitution.statValue);
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