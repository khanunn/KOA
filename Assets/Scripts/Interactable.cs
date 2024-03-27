using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    PLAYER,
    ENEMY,
    ITEM_QUEST,
    NPC,
    ITEM_INVENTORY,
    SHOP
}
public class Interactable : MonoBehaviour
{
    public Actor myActor { get; private set; }
    public PatrolController myPatrol { get; private set; }
    public PlayerController myPlayer { get; private set; }
    public QuestPoint myQuestPoint { get; private set; }
    public ItemController myItem { get; private set; }
    public PlayerSkill myPlayerSkill { get; private set; }

    public StatusManager myStatus { get; private set; }

    public InteractableType interactionType;
    private void Awake()
    {
        switch (interactionType)
        {
            case InteractableType.ENEMY:
                myActor = GetComponent<Actor>();
                myPatrol = GetComponent<PatrolController>();
                myPlayer = GetComponent<PlayerController>();
                break;
            case InteractableType.PLAYER:
                myActor = GetComponent<Actor>();
                myPatrol = GetComponent<PatrolController>();
                myPlayer = GetComponent<PlayerController>();
                myPlayerSkill = GetComponent<PlayerSkill>();
                myStatus = GetComponent<StatusManager>();
                break;
            case InteractableType.NPC:
                myQuestPoint = GetComponent<QuestPoint>();
                myPlayer = GetComponent<PlayerController>();
                break;
            case InteractableType.ITEM_INVENTORY:
                myItem = GetComponent<ItemController>();
                break;
            default:
                break;
        }
    }

    public void InteracItem()
    {
        Destroy(gameObject);
    }
}
