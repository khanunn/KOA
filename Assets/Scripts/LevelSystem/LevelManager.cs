using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    [field: SerializeField] public int level;
    [SerializeField] private int experience;
    [SerializeField] private int requireExperience;

    [Header("Config")]
    [SerializeField] private LevelInfoSO levelInfoSO;
    [SerializeField] private TMP_Text textExperience;
    [SerializeField] private TMP_Text textLevelCharacterInterface;
    [SerializeField] private TMP_Text textLevelEquipment;
    [SerializeField] private Image experienceBar;

    public ClassInfoSO classInfoSO;
    [HideInInspector] public StatContainer statContainer;

    private void OnEnable()
    {
        EventManager.instance.playerEvents.onExperienceGained += ExperienceUp;
    }
    private void OnDisable()
    {
        EventManager.instance.playerEvents.onExperienceGained -= ExperienceUp;
    }

    private void Awake()
    {
        CalculateRequireExp();
    }
    private void LevelUp()
    {
        level++;
        CalculateRequireExp();
        EventManager.instance.playerEvents.PlayerLevelChange(level);
        EventManager.instance.statEvents.LevelUpStat();
    }
    private void ExperienceUp(int value)
    {
        experience += value;
        UpdateUI();
        if (experience >= requireExperience)
        {
            while (experience >= requireExperience)
            {
                experience -= requireExperience;
                LevelUp();
            }
        }
    }
    private void CalculateRequireExp()
    {
        requireExperience = levelInfoSO.GetRequiredExp(level);
        UpdateUI();
    }
    private void UpdateUI()
    {
        experienceBar.fillAmount = ((float)experience / (float)requireExperience);
        textLevelCharacterInterface.text = level.ToString();
        textLevelEquipment.text = level.ToString();
        textExperience.text = experience + "/" + requireExperience;
    }
}