using UnityEngine;

public class StatManager : MonoBehaviour
{
    public ClassInfoSO classInfoSO;
    public LevelManager levelManager;
    [HideInInspector] StatContainer statContainer;
    private void OnEnable()
    {
        EventManager.instance.playerEvents.onPlayerLevelChange += LevelUpStat;
    }
    private void OnDisable()
    {
        EventManager.instance.playerEvents.onPlayerLevelChange -= LevelUpStat;
    }
    private void Awake()
    {
        SetStat();
    }
    public void SetStat()
    {
        if (statContainer == null)
        {
            statContainer = new StatContainer(classInfoSO);
        }
        for (int i = 0; i < levelManager.level; i++)
        {
            LevelUpStat(levelManager.level);
        }
    }
    public void LevelUpStat(int level)
    {
        const int maxIncrease = 10;

        IncreaseStatRandomly(statContainer.Constitution, classInfoSO.Constitution, maxIncrease);
        IncreaseStatRandomly(statContainer.Dexterity, classInfoSO.Dexterity, maxIncrease);
        IncreaseStatRandomly(statContainer.Strength, classInfoSO.Strength, maxIncrease);
        IncreaseStatRandomly(statContainer.Wisdom, classInfoSO.Wisdom, maxIncrease);
        IncreaseStatRandomly(statContainer.Intelligent, classInfoSO.Intelligent, maxIncrease);
        IncreaseStatRandomly(statContainer.Lucky, classInfoSO.Lucky, maxIncrease);
    }

    private void IncreaseStatRandomly(Stat stat, StatBase statBase, int maxIncrease)
    {
        float randomValue = Random.Range(0f, 1f);
        stat.statValue += Mathf.RoundToInt(statBase.BaseStatModifier.Evaluate(randomValue) * maxIncrease);
    }

    public Stat GetStat(StatKey statName)
    {
        switch (statName)
        {
            case StatKey.Constitution: return statContainer.Constitution;
            case StatKey.Dexterity: return statContainer.Dexterity;
            case StatKey.Strength: return statContainer.Strength;
            case StatKey.Wisdom: return statContainer.Wisdom;
            case StatKey.Intelligent: return statContainer.Intelligent;
            case StatKey.Lucky: return statContainer.Lucky;
            default: return statContainer.Health;
        }
    }
}