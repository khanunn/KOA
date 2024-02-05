using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EventManager : MonoBehaviour
{
    public static EventManager instance { get; private set; }
    public KillEvents killEvents;
    public PlayerEvents playerEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public PickupEvents pickupEvents;
    public ItemEvents itemEvents;
    public HealthEvents healthEvents;
    public CurrencyEvents currencyEvents;
    public StatEvents statEvents;
    public DialogueEvents dialogueEvents;
    public EquipmentEvents equipmentEvents;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            Debug.LogError("Found Event Manager > 1 in scene");
            return;
        }
        instance = this;
        //DontDestroyOnLoad(this.gameObject);

        killEvents = new KillEvents();
        playerEvents = new PlayerEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        pickupEvents = new PickupEvents();
        itemEvents = new ItemEvents();
        healthEvents = new HealthEvents();
        currencyEvents = new CurrencyEvents();
        statEvents = new StatEvents();
        dialogueEvents = new DialogueEvents();
        equipmentEvents = new EquipmentEvents();
        //Debug.Log("EventManagers Success");
    }
}