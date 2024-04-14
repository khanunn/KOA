using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    protected QuestInfoSO info;

    public int questStepCurrent;
    public int questStepToComplete;

    public void InitializeQuestStep(QuestInfoSO questInfo)
    {
        this.questId = questInfo.id;
        this.info = questInfo;
       
        Debug.Log("Quest Step ID : " + questId);
    }

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            EventManager.instance.questEvents.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }
}
