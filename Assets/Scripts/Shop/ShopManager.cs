using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System.Xml;


public class ShopManager : MonoBehaviour
{
    [Header("Buy Settings")]
    [SerializeField] ItemInfoSO[] SellableObject;
    [SerializeField] int[] EnableToBuy;
    [SerializeField] GameObject[] BaseItem;

    [Header("Parameter Settings")]
    [SerializeField] CurrencyManager PlayerMoney;
    [SerializeField] InventoryManager inventoryManager;

    [Header("UI Settings")]
    [SerializeField] GameObject ShopPanal;
    [SerializeField] GameObject BuyAblePanal;
    [SerializeField] GameObject UnBuyAblePanal;
    [SerializeField] int PageIndex = 0;


    private async void GenerateItem(int Index)
    {
        PageIndex = Index;
        int index = Index;
        //using for deActive unusing panal
        for (var i = 8; i < BaseItem.Length; i++)
        {
            BaseItem[i].SetActive(false);
        }

        for(int i = 0; i < 7; index++, i++)
        {
            Debug.Log(index);
            //Debug.Log(item.name);
            //temp GameObject in BaseItem
            GameObject Icon = BaseItem[i].transform.GetChild(0).gameObject;
            GameObject ItemName = BaseItem[i].transform.GetChild(1).gameObject;
            GameObject Price = BaseItem[i].transform.GetChild(2).gameObject;
            GameObject BuyButton = BaseItem[i].transform.GetChild(3).gameObject;
            GameObject SellButton = BaseItem[i].transform.GetChild(4).gameObject;

            //Debug.Log(BuyButton);
            //Load Sprite
            var op = Addressables.LoadAssetAsync<Sprite>($"Assets/Icons/icon/Potion/{index}.png");
            var prefab = await op.Task;

            //BaseIcon[index] = prefab;

            Icon.GetComponent<Image>().sprite = prefab;
            ItemName.GetComponent<TextMeshProUGUI>().text = SellableObject[index].DisplayName;
            Price.GetComponent<TextMeshProUGUI>().text = "" + SellableObject[index].Price.ToString();

            Debug.Log(SellableObject[index].DisplayName);

            var ID = index; //Solve Index outbound Bug 
            BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyItem(ID));
            SellButton.GetComponent<Button>().onClick.AddListener(() => SellItem(ID));

            //index++; //Shift to another            
        }
    }

    public void NextPage()
    {
        PageIndex += 8;
        if (PageIndex <= SellableObject.Length) GenerateItem(SellableObject.Length - 8);
        else GenerateItem(PageIndex);
    }

    public void PreviousPage()
    {       
        PageIndex -= 8;
        if(PageIndex <= 0) GenerateItem(0);
        else GenerateItem(PageIndex);
    }

    public void BuyItem(int ItemID)
    {
        Debug.Log("Transaction: " + (PlayerMoney.gold - SellableObject[ItemID].Price));
        if (EnableToBuy[ItemID] > 0 && PlayerMoney.gold >= SellableObject[ItemID].Price)
        {
            BuyAblePanal.SetActive(true);
            EnableToBuy[ItemID] -= 1;

            //Calculate Money in CurrencyManager
            PlayerMoney.gold -= SellableObject[ItemID].Price;

            //Send to inventory here
            EventManager.instance.itemEvents.AddItem(SellableObject[ItemID]);
            switch (SellableObject[ItemID].scriptableObject)
            {
                case EquipmentInfoSO: return;
                default:
                    EventManager.instance.pickupEvents.UpdateItem(SellableObject[ItemID], 1);
                    break;
            }
            //EventManager.instance.pickupEvents.UpdateItem(SellableObject[ItemID], 1);
            //inventoryManager.UpdateItemAmount(SellableObject[ItemID], 1);
            Debug.Log(SellableObject[ItemID]);

        }
        else UnBuyAblePanal.SetActive(true);
    }

    public void SellItem(int ItemID)
    {
        //Clone Player item in inventory from Inventory Manager
        Dictionary<ItemName, int> CloneItemAmount = inventoryManager.GetItemAmount();
        List<ItemInfoSO> CloneItems = inventoryManager.GetItems();

        foreach (ItemName key in new List<ItemName>(CloneItemAmount.Keys))
        {
            //Check if the player has this item or not &
            if (key == SellableObject[ItemID].ItemName)
            {
                //Check if itemAmount need to more than 0       
                if (CloneItemAmount[key] > 0)
                {
                    Debug.Log("Transaction: " + (PlayerMoney.gold + SellableObject[ItemID].Price));
                    Debug.Log("Sell: " + SellableObject[ItemID]);

                    //Calculate Money in CurrencyManager
                    PlayerMoney.gold += SellableObject[ItemID].Price;
                    //inventoryManager.UpdateItemAmount(SellableObject[ItemID], -1);
                    EventManager.instance.pickupEvents.UpdateItem(SellableObject[ItemID], -1);
                    BuyAblePanal.SetActive(true);
                }
                else
                {
                    UnBuyAblePanal.SetActive(true);
                    break;
                }
            }
        }

        foreach (ItemInfoSO item in new List<ItemInfoSO>(CloneItems))
        {
            switch (item.scriptableObject)
            {
                case EquipmentInfoSO equip:
                    if (item == SellableObject[ItemID])
                    {
                        Debug.Log("Transaction: " + (PlayerMoney.gold + SellableObject[ItemID].Price));
                        Debug.Log("Sell: " + SellableObject[ItemID]);

                        //Calculate Money in CurrencyManager
                        PlayerMoney.gold += SellableObject[ItemID].Price;
                        //inventoryManager.UpdateItemAmount(SellableObject[ItemID], -1);
                        EventManager.instance.itemEvents.RemoveItem(SellableObject[ItemID]);
                        BuyAblePanal.SetActive(true);
                        return;
                    }
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateItem(0);
        PageIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
