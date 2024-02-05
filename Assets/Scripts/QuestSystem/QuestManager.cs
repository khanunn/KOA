using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, Quest> questMap;
    private int currentPlayerLevel = 1;
    private void Awake()
    {
        questMap = CreateQuestMap();

        /* Quest quest = GetQuestById("KillPatrolQuest");
        Debug.Log("Display Name : "+quest.info.displayName);
        Debug.Log("Level Requirement : "+quest.info.levelRequirements);
        Debug.Log("State : "+quest.state);
        Debug.Log(quest.CurrentStepExits()); */
    }
    private void OnEnable()
    {
        EventManager.instance.questEvents.onStartQuest += StartQuest;
        EventManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        EventManager.instance.questEvents.onFinishQuest += FinishQuest;
        EventManager.instance.playerEvents.onPlayerLevelChange += LevelUp;
        EventManager.instance.questEvents.onStartDialogue += StartDialogue;
    }
    private void OnDisable()
    {
        EventManager.instance.questEvents.onStartQuest -= StartQuest;
        EventManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        EventManager.instance.questEvents.onFinishQuest -= FinishQuest;
        EventManager.instance.playerEvents.onPlayerLevelChange -= LevelUp;
        EventManager.instance.questEvents.onStartDialogue -= StartDialogue;
    }
    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            //Debug.Log(questMap.Values);
            EventManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    public void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        EventManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirements(Quest quest)
    {
        bool meetsRequirements = true;
        if (currentPlayerLevel < quest.info.levelRequirements)
        {
            meetsRequirements = false;
        }
        foreach (QuestInfoSO requisitesQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(requisitesQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
            }
        }
        return meetsRequirements;
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.NOT_REQUIREMENT && CheckRequirements(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
    private void StartDialogue(string id)
    {
        //เริ่มไดอาล็อก
        Debug.Log("Start Dialogue: " + id);

        Quest quest = GetQuestById(id);
        quest.DialogueCurrentQuestStep();
    }
    private void StartQuest(string id)
    {
        //เริ่มเควส
        Debug.Log("Start Quest: " + id);

        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }
    private void AdvanceQuest(string id)
    {
        //ความก้าวหน้าของเควส
        Debug.Log("Advance Quest: " + id);

        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();

        if (quest.CurrentStepExits())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }
    private void FinishQuest(string id)
    {
        /* if (id == "KillPatrolQuest")
        {
            Tutorial.instance.SetTextTutorial("5.Talk to NPC with" + " ? " + "on head");
        }
        if (id == "PickupBoxQuest")
        {
            Tutorial.instance.SetTextTutorial("8.To be continue...");
        }
        //เสร็จสิ้นเควส
        Debug.Log("Finish Quest: " + id); */

        Quest quest = GetQuestById(id);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        ClaimRewards(quest);
    }

    private void ClaimRewards(Quest quest)
    {
        Debug.Log("คุณได้รับค่าประสบการณ์" + quest.info.expReward);
        int exp = quest.info.expReward;
        EventManager.instance.playerEvents.ExperienceGained(exp);
        foreach (ItemInfoSO item in quest.info.Items)
        {
            EventManager.instance.itemEvents.AddItem(item);
        }
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //โหลดข้อมูลเควสทั้งหมดใน Assets/Resources/Quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        //สร้างเควสแมพ
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            { Debug.LogWarning("พบไอดีเควสซ้ำ เมื่อสร้าง QuestMap : " + questInfo.id); }
            else
            { idToQuestMap.Add(questInfo.id, new Quest(questInfo)); }
        }
        return idToQuestMap;
    }
    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogWarning("ไม่พบไอดีในเควสแมพ :" + id);
        }
        return quest;
    }

    private void LevelUp(int level)
    {
        currentPlayerLevel = level;
        Debug.Log("Level up from QuestManager: " + currentPlayerLevel);
    }
}
