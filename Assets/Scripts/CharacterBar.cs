using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    [Space(20)]
    [Header("Mana")]
    [SerializeField] private int mana;
    [SerializeField] private int manaMax;
    [Header("Config")]
    [SerializeField] private TMP_Text textHp;
    [SerializeField] private Image healthBar;
    [Space(5)]
    [SerializeField] private TMP_Text textMp;
    [SerializeField] private Image manaBar;
    [Space(5)]
    [SerializeField] private GameObject player;

    private Actor myActor;

    private void Awake()
    {
        myActor = player.GetComponent<Actor>();
    }

    private void OnEnable()
    {
        EventManager.instance.healthEvents.onHealthChange += SetHealth;
        EventManager.instance.healthEvents.onHealthGained += UpHealth;
        EventManager.instance.healthEvents.onHealthMaxChange += SetMaxHealth;
    }
    private void OnDisable()
    {
        EventManager.instance.healthEvents.onHealthChange -= SetHealth;
        EventManager.instance.healthEvents.onHealthGained -= UpHealth;
        EventManager.instance.healthEvents.onHealthMaxChange -= SetMaxHealth;
    }
    private void Start()
    {
        healthMax = myActor.currentHealth;
        health = healthMax;
        UpdateUI(healthMax);
        //EventManager.instance.healthEvents.HealthChange(health);
    }

    private void SetHealth(int value)
    {
        health = value;
        UpdateUI(health);
    }
    private void UpHealth(int value)
    {
        health += value;
        if (health > healthMax)
        {
            health = healthMax;
        }
        UpdateUI(health);
    }
    private void UpdateUI(int value)
    {
        healthBar.fillAmount = ((float)health / (float)healthMax);
        textHp.text = value.ToString() + "/" + healthMax;
    }
    /* public void SetPlayerHealth(int amount)
    {
        playerCurrentHealth = playerActor.currentHealth;
        //playerCurrentHealth = amount;
        EventManager.instance.healthEvents.HealthChange(playerCurrentHealth);
        Debug.Log(playerCurrentHealth);
    } */
    private void SetMaxHealth(int value)
    {
        healthMax = value;
        UpdateUI(health);
    }
}