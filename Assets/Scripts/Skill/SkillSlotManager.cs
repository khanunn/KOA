using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class SkillSlotManager : MonoBehaviour
{
    public GameObject[] SkillSlot = new GameObject[6];
    [SerializeField] PlayerSkill PlayerSkill;
    [SerializeField] int CurrentSlot = 0;

    public void SettingCoolDown() //Call this everytime when player need to resetskill on skillslot
    {
        var Index = 0;
        foreach (var item in SkillSlot)
        {
            GameObject SkillText = item.transform.GetChild(1).gameObject;

            if (Index < PlayerSkill.CurrentSkill.Length) //to prevent index out bound
            {
                SkillText.GetComponent<TextMeshProUGUI>().text = PlayerSkill.MaxCooldown[Index].ToString();
            }
            else SkillText.GetComponent<TextMeshProUGUI>().text = "";

            Index++;
        }
    }

    public void SettingSlot() //Call this everytime when player need to resetskill on skillslot
    {
        var Index = 0;
        //For Debug and Test 0: yellow 1: Blue 2: LightBlue 3: green 4: Red

        foreach (var item in SkillSlot)
        {
            GameObject SkillImg = item.transform.GetChild(0).gameObject;

            if (Index < PlayerSkill.CurrentSkill.Length)
            {
                SkillImg.GetComponent<Button>().interactable = true;
            }
            else SkillImg.GetComponent<Button>().interactable = false;

            Index++;
        }
    }

    public async Task SettingIconAsync() //Call this everytime when player need to resetskill on skillslot
    {
        var Index = 0;
        //For Debug and Test 0: yellow 1: Blue 2: LightBlue 3: green 4: Red
        int[] tempSKill = PlayerSkill.CurrentSkill;
        for (int i = 0; i < tempSKill.Length; i++)
        {
            tempSKill[i] = PlayerSkill.CurrentSkill[i];            
        }
        foreach (var item in SkillSlot)
        {
            GameObject SkillImg = item.transform.GetChild(0).gameObject;

            if (Index < PlayerSkill.CurrentSkill.Length)
            {
                var op = Addressables.LoadAssetAsync<Sprite>($"Assets/SkillIcon/Inusing/{tempSKill[Index]}.png"); //Load VFX to memory as Prefab
                Sprite prefab = await op.Task;                
                SkillImg.GetComponent<Image>().sprite = prefab;
            }
            else SkillImg.GetComponent<Button>().interactable = false;

            Index++;
        }
    }


    public void UnActiveSlot(int id)
    {
        GameObject SkillImg = SkillSlot[id].transform.GetChild(0).gameObject;
        SkillImg.GetComponent<Button>().interactable = false;
    }

    public void ActiveSlot(int id)
    {
        GameObject SkillImg = SkillSlot[id].transform.GetChild(0).gameObject;
        GameObject SkillText = SkillSlot[id].transform.GetChild(1).gameObject;
        //Debug.Log("skill text" + SkillText);
        SkillText.GetComponent<TextMeshProUGUI>().text = PlayerSkill.MaxCooldown[id].ToString();
        SkillImg.GetComponent<Button>().interactable = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        SkillSlot = new GameObject[6];

        CurrentSlot = PlayerSkill.CurrentSkill.Length;
        //Set Skill Slot to Arraylist
        for (int i = 0; i < this.transform.childCount; i++)
        {
            SkillSlot[i] = transform.GetChild(i).gameObject;
        } 

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentSlot != PlayerSkill.CurrentSkill.Length)
        {
            SettingSlot();
            SettingCoolDown();
            SettingIconAsync();
            CurrentSlot = PlayerSkill.CurrentSkill.Length;
        }

        for (int i = 0; i < PlayerSkill.skillCooldowns.Length; i++)
        {
            GameObject SkillText = SkillSlot[i].transform.GetChild(1).gameObject;
            float temp = (float)Math.Round(PlayerSkill.skillCooldowns[i], 1);
            SkillText.GetComponent<TextMeshProUGUI>().text = temp.ToString();
        }
    }
}
