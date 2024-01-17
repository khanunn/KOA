using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxMana;
    public int currentHealth { get; private set; }

    private StatManager statManager;
    private Stat con;
    private Stat v_hp_max;
    private Stat v_mp_max;

    private Interactable interactable;

    private void OnEnable()
    {
        EventManager.instance.healthEvents.onHealthGained += UpHealth;
        EventManager.instance.statEvents.onSendStat += StartStatus;
        EventManager.instance.statEvents.onLevelUpStat += UpdateStatus;
    }
    private void OnDisable()
    {
        EventManager.instance.healthEvents.onHealthGained -= UpHealth;
        EventManager.instance.statEvents.onSendStat -= StartStatus;
        EventManager.instance.statEvents.onLevelUpStat -= UpdateStatus;
    }

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        /* maxHealth = stat.statValue;
        currentHealth = maxHealth; */
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
        EventManager.instance.healthEvents.HealthChange(currentHealth);
    }

    private void UpHealth(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
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
                maxHealth = con.statValue * v_hp_max.statValue;
                currentHealth = maxHealth;
                break;
            default:
                currentHealth = maxHealth;
                break;
        }
    }
    private void UpdateStatus()
    {
        switch (interactable.interactionType)
        {
            case InteractableType.PLAYER:
                maxHealth = con.statValue * v_hp_max.statValue;
                EventManager.instance.healthEvents.HealthMaxChange(maxHealth);
                break;
            default: return;
        }
    }
}
