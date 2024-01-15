using UnityEngine;
public class Inventory
{
    public ItemInfoSO info;

    public Inventory(ItemInfoSO itemInfo)
    {
        this.info = itemInfo;
        Debug.Log("this.info: " + this.info);
    }
}