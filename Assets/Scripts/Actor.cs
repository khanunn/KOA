using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth { get; private set; }

    private void OnEnable()
    {
        EventManager.instance.healthEvents.onHealthGained += UpHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
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

}
