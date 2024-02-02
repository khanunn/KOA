using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPatrolQuestStep : QuestStep
{
    [SerializeField] private int patrolsKilled;
    [SerializeField] private int patrolsKillToComplete;
    //public UIController uIController;

    private void OnEnable()
    {
        EventManager.instance.killEvents.onMonsterKilled += PatrolKilled;
        Debug.Log("OnEnable from KillPatrolQuestStep");
    }
    private void OnDisable()
    {
        EventManager.instance.killEvents.onMonsterKilled -= PatrolKilled;
    }
    private void PatrolKilled()
    {
        if (questStepCurrent < questStepToComplete)
        {
            questStepCurrent++;
        }

        if (questStepCurrent >= questStepToComplete)
        {
            FinishQuestStep();
        }
    }
    /* private void PatrolKilled()
    {
        if (patrolsKilled < patrolsKillToComplete)
        {
            patrolsKilled++;
            //uIController.SetAmountToUI(patrolsKilled, patrolsKillToComplete);
        }

        if (patrolsKilled >= patrolsKillToComplete)
        {
            Tutorial.instance.SetTextTutorial("4.Talk to NPC with" + " Check mark " + "on head");
            FinishQuestStep();
        }
    } */
}
