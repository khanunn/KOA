using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int MaxMana { get; private set; }
    public int CurrentHealth { get; private set; }

    private StatManager statManager;
    private Stat con;
    private Stat v_hp_max;
    private Stat v_mp_max;

    private Interactable interactable;

    private void OnEnable()
    {
        EventManager.instance.healthEvents.onHealthGained += UpHealth;
        EventManager.instance.statEvents.onSendStatManager += StartStatus;
        EventManager.instance.statEvents.onLevelUpStat += UpdateStatus;
    }
    private void OnDisable()
    {
        EventManager.instance.healthEvents.onHealthGained -= UpHealth;
        EventManager.instance.statEvents.onSendStatManager -= StartStatus;
        EventManager.instance.statEvents.onLevelUpStat -= UpdateStatus;
    }

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        /* maxHealth = stat.statValue;
        currentHealth = maxHealth; */
    }
    private void Start()
    {
        Debug.Log("Start Actor");
        if (interactable.interactionType == InteractableType.ENEMY)
        {
            //PatrolController patrolController = GetComponent<PatrolController>();

            CurrentHealth = interactable.myPatrol.monsterInfoSO.Health;
            //currentHealth = maxHealth;
        }
    }
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
    }

    public void OnDeath()
    {
        Invoke(nameof(OnDestroy), 2f);
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void DamageOnHealthBar()
    {
        EventManager.instance.healthEvents.HealthChange(CurrentHealth);
    }

    private void UpHealth(int health)
    {
        switch (interactable.interactionType)
        {
            case InteractableType.PLAYER:
                CurrentHealth += health;
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
                break;
        }
    }

    private void StartStatus(StatManager myStat)
    {
        statManager = myStat;
        con = statManager.GetStat(StatKey.Constitution);
        v_hp_max = statManager.GetStat(StatKey.v_hp_max);
        v_mp_max = statManager.GetStat(StatKey.v_mp_max);
        switch (interactable.interactionType)
        {
            case InteractableType.PLAYER:
                MaxHealth = con.statValue * v_hp_max.statValue;
                CurrentHealth = MaxHealth;
                break;
            default:
                CurrentHealth = interactable.myPatrol.monsterInfoSO.Health;
                break;
        }
    }
    private void UpdateStatus()
    {
        switch (interactable.interactionType)
        {
            case InteractableType.PLAYER:
                MaxHealth = con.statValue * v_hp_max.statValue;
                CurrentHealth = MaxHealth;
                EventManager.instance.healthEvents.HealthMaxChange(MaxHealth);
                break;
            default: return;
        }
    }
}
