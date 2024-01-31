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


    private async void GenerateItem()
    {
        var index = 0;

        //using for deActive unusing panal
        for (var i = SellableObject.Length; i < BaseItem.Length; i++)
        {
            BaseItem[i].SetActive(false);
        }

        foreach (var item in SellableObject)
        {
            //Debug.Log(item.name);
            //temp GameObject in BaseItem
            GameObject Icon = BaseItem[index].transform.GetChild(0).gameObject;
            GameObject ItemName = BaseItem[index].transform.GetChild(1).gameObject;
            GameObject Price = BaseItem[index].transform.GetChild(2).gameObject;
            GameObject BuyButton = BaseItem[index].transform.GetChild(3).gameObject;
            GameObject SellButton = BaseItem[index].transform.GetChild(4).gameObject;

            //Debug.Log(BuyButton);
            //Load Sprite
            var op = Addressables.LoadAssetAsync<Sprite>($"Assets/Icons/icon/Potion/{index}.png");
            var prefab = await op.Task;

            //BaseIcon[index] = prefab;

            //Set Parameter
            Icon.GetComponent<Image>().sprite = prefab;
            ItemName.GetComponent<TextMeshProUGUI>().text = item.DisplayName;
            Price.GetComponent<TextMeshProUGUI>().text = "" + item.Price.ToString();


            var ID = index; //Solve Index outbound Bug 
            BuyButton.GetComponent<Button>().onClick.AddListener(() => BuyItem(ID));
            SellButton.GetComponent<Button>().onClick.AddListener(() => SellItem(ID));

            index++; //Shift to another            
        }
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
            inventoryManager.UpdateItemAmount(SellableObject[ItemID], 1);
            Debug.Log(SellableObject[ItemID]);

        }
        else UnBuyAblePanal.SetActive(true);
    }

    public void SellItem(int ItemID)
    {
        //Clone Player item in inventory from Inventory Manager
        Dictionary<ItemName, int> ClonePlayerItem = inventoryManager.GetPlayerItem();

        foreach (ItemName key in new List<ItemName>(ClonePlayerItem.Keys))
        {
            //Check if the player has this item or not &
            if (key == SellableObject[ItemID].ItemName)
            {
                //Check if itemAmount need to more than 0       
                if (ClonePlayerItem[key] > 0)
                {
                    Debug.Log("Transaction: " + (PlayerMoney.gold + SellableObject[ItemID].Price));
                    Debug.Log("Sell: " + SellableObject[ItemID]);

                    //Calculate Money in CurrencyManager
                    PlayerMoney.gold += SellableObject[ItemID].Price;
                    inventoryManager.UpdateItemAmount(SellableObject[ItemID], -1);
                    BuyAblePanal.SetActive(true);
                }
                else
                {
                    UnBuyAblePanal.SetActive(true);
                    break;
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
