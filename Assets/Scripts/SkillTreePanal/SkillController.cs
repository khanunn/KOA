using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Button[] Skillbutton;
    [SerializeField] Text SkillPointUpgrade;

    [Header("Skill Point Config")]    
    public int SkillPoint = 0;



    Dictionary<SkillInfoSO, Button> SkillDict = new Dictionary<SkillInfoSO, Button>();
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var item in SkillList)
        {
            SkillDict.Add(item, Skillbutton[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SkillPoint > 0)
        {
            foreach (SkillInfoSO Skillinfo in SkillDict.Keys)
            {
                if (Skillinfo.RequirementLevel <= levelManager.level) SkillDict[Skillinfo].interactable = true;
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

    public void UpgradeSkill(SkillInfoSO skill)
    {
        if(SkillPoint > 0)
        {
            foreach (SkillInfoSO Skillinfo in SkillDict.Keys)
            {
                if (Skillinfo.SkillId == skill.SkillId)
                {
                    Skillinfo.UpgradeSkill();
                    SkillPoint -= 1;
                }
            }
        }        
    }
}
