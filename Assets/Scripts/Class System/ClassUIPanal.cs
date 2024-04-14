using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassUIPanal : MonoBehaviour
{
    [Header("Manager Setting")]
    [SerializeField] LevelManager levelManager;
    [SerializeField] StatManager statManager;

    [Header("Class Setting")]
    [SerializeField] ClassInfoSO[] ClassList;
    [SerializeField] int CurrentClassTier = 1;

    [Header("UI Setting")]
    [SerializeField] Button[] Classbutton;    

    Dictionary<ClassInfoSO,Button> ClassDict = new Dictionary<ClassInfoSO,Button>();
    

    private void Start()
    {
        int i = 0;
        foreach (var item in ClassList)
        {
            ClassDict.Add(item, Classbutton[i]);
            i++;
        }            
    }

    // Update is called once per frame
    void Update()
    {               
        if (CurrentClassTier == 1)
        {
            //Using to avaible to change class when requirement are match
            foreach (ClassInfoSO Classinfo in ClassDict.Keys)
            {
                if (Classinfo.RequirementLevel <= levelManager.level) ClassDict[Classinfo].interactable = true;

            }
        }

        //Support ESC to close panal
        if (gameObject.active == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape)) this.gameObject.SetActive(false);          
        }
            
    }

    public void ChangeClassinto(ClassInfoSO Classinfo)
    {
        //Change stat by classInfo
        levelManager.classInfoSO = Classinfo;
        statManager.classInfoSO = Classinfo;
        CurrentClassTier = Classinfo.ClassTier;

        //Using to close another button
        foreach (ClassInfoSO DictClassinfo in ClassDict.Keys)
        {
            if (DictClassinfo.ClassTier == Classinfo.ClassTier && DictClassinfo.Id != Classinfo.Id) ClassDict[DictClassinfo].interactable = false;
        }
    }

}
