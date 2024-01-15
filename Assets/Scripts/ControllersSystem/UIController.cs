using System.Net.Sockets;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text questText;
    private int patrolsKilled;
    private int patrolsKillToComplete = 5;
    private int pickupBox;
    private int pickupBoxToComplete = 9;

    QuestPoint questPoint;
    private string questId;
    private void OnEnable()
    {
        //EventManager.instance.killEvents.onMonsterKilled += 
        EventManager.instance.killEvents.onMonsterKilled += PatrolKilled;
        EventManager.instance.pickupEvents.onItemPickup += BoxPickup;
    }
    private void OnDestroy()
    {
        EventManager.instance.killEvents.onMonsterKilled -= PatrolKilled;
        EventManager.instance.pickupEvents.onItemPickup -= BoxPickup;
    }
    public void SetQuestToUI(string id)
    {
        Debug.Log("Quest ID to UI:" + id);
        questId = id;
        switch (questId)
        {
            case "KillPatrolQuest":
                questText.text = "Let's Kill My Shilluet Now";
                Tutorial.instance.SetTextTutorial("3.Combat 5 Target");
                break;
            case "PickupBoxQuest":
                questText.text = "Let's Pickup Box Now";
                Tutorial.instance.SetTextTutorial("6.Pickup 5 The Box");
                break;
            default:
                Debug.LogWarning("อุนยังไม่มีเควส");
                break;
        }
    }
    private void PatrolKilled()
    {
        patrolsKilled++;

        if (patrolsKilled < patrolsKillToComplete)
        {
            questText.text = "Kill Patrol: " + patrolsKilled + "/" + patrolsKillToComplete;
        }
        else if (patrolsKilled >= patrolsKillToComplete)
        {
            questText.text = "Mission Complete " + "Kill Patrol 5/5";
        }
    }
    private void BoxPickup()
    {
        pickupBox++;
        if (pickupBox < pickupBoxToComplete)
        {
            questText.text = "Pickup Box: " + pickupBox + "/" + pickupBoxToComplete;
        }
        else if (pickupBox >= pickupBoxToComplete)
        {
            questText.text = "Mission Complete " + "Pickup Box 9/9";
        }
    }
}