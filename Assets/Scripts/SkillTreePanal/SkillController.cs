using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [Header("Manager Setting")]
    [SerializeField] PlayerSkill playerSkill;
    [SerializeField] LevelManager levelManager;

    [Header("Class Setting")]
    [SerializeField] SkillInfoSO[] SkillList;

    [Header("UI Setting")]
    [SerializeField] GameObject[] Skillbutton;    
    [SerializeField] Text SkillPointUpgrade;

    [Header("Skill Point Config")]    
    public int SkillPoint = 0;
    public int SkillObtain = 4;
    [Header("UI Config")]
    public bool isEnable = true;
    [SerializeField] GameObject SkillPanalUI;

    Dictionary<SkillInfoSO, Button> SkillDict = new Dictionary<SkillInfoSO, Button>();
    // Start is called before the first frame update
    void Start()
    {     
        int i = 0;
        foreach (var item in SkillList)
        {
            SkillDict.Add(item, Skillbutton[i].GetComponent<Button>());
            i++;
        }

        SkillPanalUI = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (SkillPoint > 0)
        {
            foreach (SkillInfoSO Skillinfo in SkillDict.Keys)
            {
                if (Skillinfo.RequirementLevel <= levelManager.level)
                {                    
                    SkillDict[Skillinfo].interactable = true;
                }                    
            }
        }
        else
        {
            foreach (SkillInfoSO Skillinfo in SkillDict.Keys)
            {
                SkillDict[Skillinfo].interactable = false;
            }
        }

        //Support ESC to close panal
        if (gameObject.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) this.gameObject.SetActive(false);
        }        

        SkillPointUpgrade.text = "Skill Point Avaible: " + SkillPoint.ToString();
    }

    public void UpdateSkillLevel(SkillInfoSO skill)
    {
        /*foreach (GameObject SkillLv in Skillbutton)
        {
            if(skill.name == SkillLv.gameObject.name) SkillLv.transform.GetChild(1).GetComponent<Text>().text = skill.SkillLevel.ToString() + " / 5";
        }*/
        Skillbutton[skill.SkillId - 1].transform.GetChild(1).GetComponent<Text>().text = skill.SkillLevel.ToString() + " / 5";
    }

    public void UpgradeSkill(SkillInfoSO skill)
    {
        if(SkillPoint > 0)
        {
            SkillObtain += 1;
            foreach (SkillInfoSO Skillinfo in SkillDict.Keys)
            {
                if (Skillinfo.SkillId == skill.SkillId)
                {
                    Skillinfo.UpgradeSkill();
                    
                    SkillPoint -= 1;
                }
            }

            if (SkillObtain < 6) //If already have skill it will return and prevent adding redundance skill
            {
                foreach (SkillInfoSO ObtainSkill in playerSkill.SkillData)
                {
                    if (ObtainSkill.SkillId == skill.SkillId) return;
                }

                //ChangeSize of  skillCooldowns, SkillMaxSetCD and CurrentSkill in PlayerSkill and put new item into arrays
                float[] Temp_skillCooldowns = new float[playerSkill.skillCooldowns.Length + 1];
                float[] Temp_SkillMaxSetCD = new float[playerSkill.SkillMaxSetCD.Length + 1];
                int[] Temp_CurrentSkill = new int[playerSkill.SkillMaxSetCD.Length + 1];
                float[] Temp_MaxCooldown = new float[playerSkill.MaxCooldown.Length + 1];

                Temp_CurrentSkill[Temp_CurrentSkill.Length - 1] = skill.SkillId;

                playerSkill.skillCooldowns.CopyTo(Temp_skillCooldowns, 0);
                playerSkill.SkillMaxSetCD.CopyTo(Temp_SkillMaxSetCD, 0);
                playerSkill.CurrentSkill.CopyTo(Temp_CurrentSkill, 0);
                playerSkill.MaxCooldown.CopyTo(Temp_MaxCooldown, 0);

                playerSkill.skillCooldowns = Temp_skillCooldowns;
                playerSkill.SkillMaxSetCD = Temp_SkillMaxSetCD;
                playerSkill.CurrentSkill = Temp_CurrentSkill;
                playerSkill.MaxCooldown = Temp_MaxCooldown;


            }
        }        
    }

    public void SwitchInventory()
    {
        if (isEnable)
        {
            if (SkillPanalUI.active == false)
            {
                SkillPanalUI.SetActive(true);
                
            }
            else
            {
                SkillPanalUI.SetActive(false);               
            }
        }
        else
        {
            SkillPanalUI.SetActive(false);            
        }
    }
}
