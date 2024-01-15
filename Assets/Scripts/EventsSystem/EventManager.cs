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
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found Game Manager > 1 in scene");
        }
        instance = this;

        killEvents = new KillEvents();
        playerEvents = new PlayerEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        pickupEvents = new PickupEvents();
        itemEvents = new ItemEvents();
        healthEvents = new HealthEvents();
        currencyEvents = new CurrencyEvents();
        statEvents = new StatEvents();
        //Debug.Log("EventManagers Success");
    }
}