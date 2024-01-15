using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBoxQuestStep : QuestStep
{
    [SerializeField] private int pickupBox;
    [SerializeField] private int pickupBoxToComplete;

    private void OnEnable()
    {
        EventManager.instance.pickupEvents.onItemPickup += BoxPickup;
    }
    private void OnDisable()
    {
        EventManager.instance.pickupEvents.onItemPickup -= BoxPickup;
    }
    private void BoxPickup()
    {
        if (pickupBox < pickupBoxToComplete)
        {
            pickupBox++;
        }

        if (pickupBox >= pickupBoxToComplete)
        {
            Tutorial.instance.SetTextTutorial("7.Talk to NPC with" + " Check mark " + "on head");
            FinishQuestStep();
        }
    }
}
