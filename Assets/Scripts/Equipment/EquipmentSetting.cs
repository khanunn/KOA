using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSetting : MonoBehaviour
{
    [SerializeField] GameObject[] EqupimentSet; //0-head 1-body 2-Larm 3-Rarm 4-buttom  
    public int[] ItemID = {0,0,0,0,0}; //0-head 1-body 2-Larm 3-Rarm 4-buttom

    public void ChangeItemSet()
    {               
        switch(ItemID[0]) //part head 
        {
            case 1:
                GameObject Item = Resources.Load("Equipment") as GameObject;                    
                Instantiate(Item, EqupimentSet[0].transform.position, Quaternion.identity, EqupimentSet[0].transform);
                break;  
        }
       
    }


    // Update is called once per frame
    void Update()
    {       
            
    }
}
