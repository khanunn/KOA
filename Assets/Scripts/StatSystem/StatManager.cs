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
        if (statContainer == null)
        {
            statContainer = new StatContainer(classInfoSO);
        }

        //for solving noob stat on the start game
        SetStartStat();
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

    private void SetStartStat()
    {
        const int maxIncrease = 10;
        DirectAddingStat(statContainer.Constitution, classInfoSO.Constitution, 3);
        DirectAddingStat(statContainer.Dexterity, classInfoSO.Dexterity, maxIncrease);
        DirectAddingStat(statContainer.Strength, classInfoSO.Strength, maxIncrease);
        DirectAddingStat(statContainer.Wisdom, classInfoSO.Wisdom, maxIncrease);
        DirectAddingStat(statContainer.Intelligent, classInfoSO.Intelligent, maxIncrease);
        DirectAddingStat(statContainer.Lucky, classInfoSO.Lucky, maxIncrease);
        DirectAddingStat(statContainer.v_hp_max, classInfoSO.v_hp_max, maxIncrease);
        DirectAddingStat(statContainer.v_mp_max, classInfoSO.v_mp_max, maxIncrease);
        DirectAddingStat(statContainer.v_patk, classInfoSO.v_patk, maxIncrease);

        DirectAddingStat(statContainer.v_evade, classInfoSO.v_evade, 0);
        DirectAddingStat(statContainer.v_acc, classInfoSO.v_acc, 0);

    }
    private void LevelUpStat(int level)
    {
        const int maxIncrease = 5;
        IncreaseStatRandomly(statContainer.Constitution, classInfoSO.Constitution, maxIncrease);
        IncreaseStatRandomly(statContainer.Dexterity, classInfoSO.Dexterity, maxIncrease);
        IncreaseStatRandomly(statContainer.Strength, classInfoSO.Strength, maxIncrease);
        IncreaseStatRandomly(statContainer.Wisdom, classInfoSO.Wisdom, maxIncrease);
        IncreaseStatRandomly(statContainer.Intelligent, classInfoSO.Intelligent, maxIncrease);
        IncreaseStatRandomly(statContainer.Lucky, classInfoSO.Lucky, maxIncrease);

        IncreaseStatRandomly(statContainer.v_hp_max, classInfoSO.v_hp_max, maxIncrease);
        IncreaseStatRandomly(statContainer.v_mp_max, classInfoSO.v_mp_max, maxIncrease);
        IncreaseStatRandomly(statContainer.v_patk, classInfoSO.v_patk, maxIncrease);

        DirectAddingStat(statContainer.v_pdef, classInfoSO.v_pdef, 2);
        DirectAddingStat(statContainer.v_evade, classInfoSO.v_evade, statContainer.Dexterity.statValue / 2);
        DirectAddingStat(statContainer.v_acc, classInfoSO.v_acc, statContainer.Dexterity.statValue / 4);
    }

    void calculatedNewStat()
    {
        
    }

    private void IncreaseStatRandomly(Stat stat, StatBase statBase, int maxIncrease)
    {
        float randomValue = Random.Range(0f, 1f);
        stat.statValue += Mathf.RoundToInt(statBase.BaseStatModifier.Evaluate(randomValue) * maxIncrease);
    }

    private void DirectAddingStat(Stat stat, StatBase statBase, int StatIncrease)
    {        
        stat.statValue += Mathf.RoundToInt(statBase.BaseStatModifier.Evaluate(1) * StatIncrease);
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
            case StatKey.v_pdef: return statContainer.v_pdef;
            case StatKey.v_mdef: return statContainer.v_mdef;

            case StatKey.v_acc: return statContainer.v_acc;
            case StatKey.v_evade: return statContainer.v_evade;
            case StatKey.v_crit_change: return statContainer.v_crit_change;
            case StatKey.v_crit_dam: return statContainer.v_crit_dam;
            case StatKey.v_pdam: return statContainer.v_pdam;
            case StatKey.v_mdam: return statContainer.v_mdam;

            default: return statContainer.v_hp_max;
        }
    }
}