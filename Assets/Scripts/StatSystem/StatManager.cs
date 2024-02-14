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
    private void Start()
    {
        //Debug.Log("Start StatManager");
        EventManager.instance.statEvents.SendStatManager(this);
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
    private void LevelUpStat(int level)
    {
        const int maxIncrease = 10;
        IncreaseStatRandomly(statContainer.Constitution, classInfoSO.Constitution, maxIncrease);
        IncreaseStatRandomly(statContainer.Dexterity, classInfoSO.Dexterity, maxIncrease);
        IncreaseStatRandomly(statContainer.Strength, classInfoSO.Strength, maxIncrease);
        IncreaseStatRandomly(statContainer.Wisdom, classInfoSO.Wisdom, maxIncrease);
        IncreaseStatRandomly(statContainer.Intelligent, classInfoSO.Intelligent, maxIncrease);
        IncreaseStatRandomly(statContainer.Lucky, classInfoSO.Lucky, maxIncrease);

        IncreaseStatRandomly(statContainer.v_hp_max, classInfoSO.v_hp_max, maxIncrease);
        IncreaseStatRandomly(statContainer.v_mp_max, classInfoSO.v_mp_max, maxIncrease);
        IncreaseStatRandomly(statContainer.v_patk, classInfoSO.v_patk, maxIncrease);

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
            case StatKey.v_hp_max: return statContainer.v_hp_max;
            case StatKey.v_mp_max: return statContainer.v_mp_max;
            case StatKey.v_hp_recovery: return statContainer.v_hp_recovery;
            case StatKey.v_mp_recovery: return statContainer.v_mp_recovery;
            case StatKey.v_patk: return statContainer.v_patk;
            case StatKey.v_matk: return statContainer.v_matk;
            default: return statContainer.v_hp_max;
        }
    }
}